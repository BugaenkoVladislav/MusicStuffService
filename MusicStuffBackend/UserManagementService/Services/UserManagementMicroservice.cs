using Grpc.Core;
using Infrastructure;
using Infrastructure;
using MusicStuffBackend;
using LoginPassword = Domain.Entities.LoginPassword;
using User = MusicStuffBackend.User;

namespace UserManagmentService.Services;

public class UserManagementMicroservice(ILogger<UserManagementMicroservice> logger, UnitOfWork uow) : User.UserBase
{
    private readonly ILogger<UserManagementMicroservice> _logger = logger;
    private UnitOfWork _uow = uow;
    public override async Task<Result> RemoveUser(LogPasUser request, ServerCallContext context)
    {
        var userLoginPassword = await _uow.LoginPasswordRepository.FindEntityByAsync(x => x.Password == request.LoginPassword.Password && x.Login == request.LoginPassword.Login);
        var user = await _uow.UserRepository.FindEntityByAsync(x => x.IdLoginPassword == userLoginPassword.IdLoginPassword);
       _uow.UserRepository.RemoveEntity(user);
       await _uow.SaveAllChanges();
       return await Task.FromResult(new Result()
       {
            StatusCode = "200",
            Details = "Object Successful Removed"
       });
    }

    public override async Task<Result> ChangeLoginPassword(LogPasUser request, ServerCallContext context)
    {
        var user = await _uow.UserRepository.FindEntityByAsync(x => x.IdUser == request.IdUser);
        var loginPassword =
            await _uow.LoginPasswordRepository.FindEntityByAsync(x => x.IdLoginPassword == user.IdLoginPassword);

        loginPassword.Password = request.LoginPassword.Password;
        loginPassword.Login = request.LoginPassword.Login;
        _uow.LoginPasswordRepository.UpdateEntity(loginPassword);
        await _uow.SaveAllChanges();
        return await Task.FromResult(new Result()
        {
            StatusCode = "200",
            Details = "Object Successful Updated"
        });
    }

    public override async Task<Result> CreateNewUser(UserMessage request, ServerCallContext context)
    {
        var loginPassword = new LoginPassword()
        {
            Login = request.LogPass.Login,
            Password = request.LogPass.Password,
        };
        _uow.LoginPasswordRepository.AddEntity(loginPassword);
        await _uow.SaveAllChanges();
        _uow.UserRepository.AddEntity(new Domain.Entities.User()
        {
            Name = request.Name,
            Surname = request.Surname,
            Username = request.Username,
            LoginPassword = loginPassword,
            IdLoginPassword = (await _uow.LoginPasswordRepository.FindEntityByAsync(i => i.Password == loginPassword.Password && i.Login == loginPassword.Login)).IdLoginPassword,
            Role = await _uow.RoleRepository.FindEntityByAsync(x=>x.IdRole == 1),
            IdRole = 1
        });
        await _uow.SaveAllChanges();
        return await Task.FromResult(new Result()
        {
            StatusCode = "200",
            Details = "Success"
        });
    }

    public override async Task<UserMessage> GetUserById(Id request, ServerCallContext context)
    {
        var user = await  _uow.UserRepository.FindEntityByAsync(x => x.IdRole == request.MessageId);
        return await Task.FromResult(new UserMessage()
        {
            Name = user.Name,
            Surname = user.Surname,
            Username = user.Username,
            LogPass = new MusicStuffBackend.LoginPassword()
            {
                Login = user.LoginPassword.Login,
                Password = user.LoginPassword.Password
            }
        });
    }
}