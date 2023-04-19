using Common.Application;
using Common.Application.SecurityUtil;
using Microsoft.EntityFrameworkCore;
using UserModule.Data;

namespace UserModule.Core.Commands.Users.ChangePassword;

public class ChangeUserPasswordCommandHandler : IBaseCommandHandler<ChangeUserPasswordCommand>
{
    private readonly UserContext _userContext;

    public ChangeUserPasswordCommandHandler(UserContext userContext)
    {
        _userContext = userContext;
    }

    public async Task<OperationResult> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userContext.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);

        if (user == null)
            return OperationResult.NotFound();

        if(Sha256Hasher.IsCompare(user.Password,request.CurrentPassword) == true)
        {
            string hashPassword = Sha256Hasher.Hash(request.NewPassword);
            user.Password = hashPassword.SanitizeText();
            _userContext.Update(user);
            await _userContext.SaveChangesAsync();
            return OperationResult.Success();
        }
        return OperationResult.Error("کلمه عبور نامعتبر است");
    }
}
