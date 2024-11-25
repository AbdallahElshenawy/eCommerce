

using AutoMapper;
using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Identity;
using eCommerce.Application.Services.Interfaces.Authentication;
using eCommerce.Application.Services.Interfaces.Logging;
using eCommerce.Domain.Entities.Identity;
using eCommerce.Domain.Interfaces.Authentication;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.Application.Services.Implementaions.Authentication
{
    public class AuthenticationService
        (ITokenManagement tokenManagement,IUserManagement userManagement,
        IRoleManagement roleManagement, IAppLogger<AuthenticationService>logger
        ,IMapper mapper, IValidator<CreateUser> CreateUserValidator
        ,IValidator<LoginUser> LoginUserValidator) : IAuthenticationService
    {
        public async Task<ServiceResponse> CreateUser(CreateUser user)
        {
            var validation = await CreateUserValidator.ValidateAsync(user);
            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(e=>e.ErrorMessage).ToList();
                string ErrorsToString = string.Join(", ", errors);
                return new ServiceResponse { Message = ErrorsToString };
            }
            var mapModel = mapper.Map<AppUser>(user);
            mapModel.UserName = user.Email;

            var passwordHasher = new PasswordHasher<AppUser>();
            mapModel.PasswordHash = passwordHasher.HashPassword(mapModel, user.Password);

            var result = await userManagement.CreateUser(mapModel);
            if (!result)
            {
                return new ServiceResponse { Message = "Email might be in use or an unknown error occurred!" };
            }
            var _user = await userManagement.GetUserByEmail(user.Email);
            var users= await userManagement.GetAllUsers();
            bool assignRole = await roleManagement.AddUserToRole(_user!, users!.Count() > 1 ? "User" : "Admin");
            if(!assignRole)
            {
                int removeUserRole = await userManagement.RemoveUserByEmail(user.Email);
                if(removeUserRole <= 0)
                {
                    logger.LogError(new Exception($"User with email {_user!.Email} failed to remove as a role assiging issue"),"User can not be assign to a role");
                    return new ServiceResponse { Message = "Error occurred in creat account " };
                }
            }
            return new ServiceResponse { Message ="Account created",Success = true};

        }

        public async Task<LoginResponse> LoginUser(LoginUser user)
        {
            var validation = await LoginUserValidator.ValidateAsync(user);
            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(e => e.ErrorMessage).ToList();
                string ErrorsToString = string.Join(", ", errors);
                return new LoginResponse { Message = ErrorsToString };
            }
            var mapModel = mapper.Map<AppUser>(user);
            mapModel.PasswordHash = user.Password;
            bool loginResult = await userManagement.LoginUser(mapModel);
            if (!loginResult) return new LoginResponse(Message: "Email not found");

            var _user = await userManagement.GetUserByEmail(user.Email);
            var claims = await userManagement.GetUserClaims(_user!.Email!);

            string jwtToken = tokenManagement.GenerateToken(claims);
            string refreshToken = tokenManagement.GetRefreshToken();
            int saveToken = 0;
            bool userTokenCheck= await tokenManagement.ValidateRefreshToken(refreshToken);
            if (userTokenCheck)
            saveToken =  await tokenManagement.UpdateRefreshToken(_user.Id, refreshToken);
            else
                saveToken = await tokenManagement.AddRefreshToken(_user.Id, refreshToken);
            return saveToken <= 0? new LoginResponse (Message :"Internal error occurred while authenticating"):
                new LoginResponse (Success:true,Token:jwtToken,RefreshToken:refreshToken);
        }

        public async Task<LoginResponse> ReviveToken(string refreshToken)
        {
            bool validateToken = await tokenManagement.ValidateRefreshToken(refreshToken);
            if (!validateToken) return new LoginResponse(Message: "Invalid token");
            string userId = await tokenManagement.GetUserIdByRefreshToken(refreshToken);
            var user = await userManagement.GetUserById(userId);
            var claims = await userManagement.GetUserClaims(user.Email!);
            string newJwtToken = tokenManagement.GenerateToken(claims);
            string newRefreshToken = tokenManagement.GetRefreshToken();
            await tokenManagement.UpdateRefreshToken(userId, newRefreshToken);
            return new LoginResponse (Success:true,Token:newJwtToken,RefreshToken:newRefreshToken);
        }
    }
}
