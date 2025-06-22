using MoneyMaster.Common.DTOs;

namespace MoneyMaster.Service.Interfaces;

public interface IUserService
{
    public Task<ServiceResult<IEnumerable<UserDTO>>> GetUsersAsync();
    public Task<ServiceResult<UserDTO>> GetUserByEmailAsync(string email);
    public Task<ServiceResult<UserDTO>> GetUserByIdAsync(string id);
    public Task<ServiceResult<UserDTO>> AddUserAsync(UserDTO userDTO);
    public Task<ServiceResult> UpdateUserAsync(UserDTO userDTO);
    public Task<ServiceResult> DeleteUserAsync(int id);
}
