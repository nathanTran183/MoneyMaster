using AutoMapper;
using MoneyMaster.Common.DTOs;
using MoneyMaster.Database.Interfaces;
using MoneyMaster.Service.Interfaces;

namespace MoneyMaster.Service.Services;

public class UserService : IUserService
{
    private readonly IMapper mapper;
    private readonly IUserRepository userRepository;

    public UserService(IMapper mapper, IUserRepository userRepository)
    {
        this.mapper = mapper;
        this.userRepository = userRepository;
    }

    public async Task<ServiceResult<IEnumerable<UserDTO>>> GetUsersAsync()
    {
        var result = new ServiceResult<IEnumerable<UserDTO>>();
        var users = await userRepository.GetUsersAsync();
        result.Value = mapper.Map<IEnumerable<UserDTO>>(users);
        return result;
    }

    public async Task<ServiceResult<UserDTO>> GetUserByIdAsync(string id)
    {
        var result = new ServiceResult<UserDTO>();
        var user = await userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            result.AddErrors($"User Id {id} is not existed.");
        }
        result.Value = mapper.Map<UserDTO>(user);
        return result;
    }

    public async Task<ServiceResult<UserDTO>> GetUserByEmailAsync(string email)
    {
        var result = new ServiceResult<UserDTO>();
        var user = await userRepository.GetUserByEmailAsync(email);
        if (user == null)
        {
            result.AddErrors($"User with email {email} is not existed.");
        }
        result.Value = mapper.Map<UserDTO>(user);
        return result;
    }

    public async Task<ServiceResult<UserDTO>> AddUserAsync(UserDTO userDTO)
    {
        var result = new ServiceResult<UserDTO>();
        if(await userRepository.IsEmailExistAsync(userDTO.Email))
        {

        }


        await userRepository.SaveUserAsync(userDTO)
    }

    public Task<ServiceResult> DeleteUserAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult> UpdateUserAsync(UserDTO userDTO)
    {
        throw new NotImplementedException();
    }

}
