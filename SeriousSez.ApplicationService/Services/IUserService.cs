using Microsoft.AspNetCore.Identity;
using SeriousSez.Domain.Entities;
using SeriousSez.Domain.Models;
using SeriousSez.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeriousSez.ApplicationService.Services
{
    public interface IUserService
    {
        Task<IdentityResult> Create(RegistrationViewModel model);
        Task<UserResponse> Update(UserUpdateViewModel model);
        Task<IdentityResult> Delete(UserResponse model);
        Task<UserResponse> Get(Guid id);
        Task<User> GetEntity(Guid id);
        Task<UserResponse> GetByUserId(Guid id);
        Task<UserResponse> GetByUserName(string userName);
        Task<UserResponse> GetByEmail(string userName);
        Task<IEnumerable<UserResponse>> GetAll();
        Task<UserSettingsResponse> GetSettings(Guid id);
        Task<UserSettingsResponse> UpdateSettings(UserSettingsUpdateViewModel model);
        List<string> GetRoles();
        Task AddRoleToUser(UserResponse user);
    }
}