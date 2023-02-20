using Common.Application;
using Microsoft.EntityFrameworkCore;
using UserModule.Data;

namespace UserModule.Core.Commands.Users.Edit;

public class EditUserCommandHandler : IBaseCommandHandler<EditUserCommand>
{
    private readonly UserContext _userContext;

    public EditUserCommandHandler(UserContext userContext)
    {
        _userContext = userContext;
    }

    public async Task<OperationResult> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        var user =await _userContext.Users.FirstOrDefaultAsync(u => u.Id == request.UserId,cancellationToken);

        if (user == null)
            return OperationResult.NotFound();

        user.Name = request.Name;
        user.Family = request.Family;
        if (string.IsNullOrWhiteSpace(request.Email) == false)
            user.Email = request.Email;

        await _userContext.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
}
