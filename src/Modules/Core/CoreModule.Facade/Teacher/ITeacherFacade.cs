using Common.Application;
using CoreModule.Application.Teacheres.AcceptRequest;
using CoreModule.Application.Teacheres.RejectRequest;
using MediatR;

namespace CoreModule.Facade.Teacher;

public interface ITeacherFacade
{
    Task<OperationResult> Register(RegisterTeacherCommand command);
    Task<OperationResult> AcceptTeacherRequest(AcceptTeacherRequestCommand command);
    Task<OperationResult> RejectTeacherRequest(RejectTeacherRequestCommand command);
}
class TeacherFacade : ITeacherFacade
{
    private readonly IMediator _mediator;
    public TeacherFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> AcceptTeacherRequest(AcceptTeacherRequestCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Register(RegisterTeacherCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RejectTeacherRequest(RejectTeacherRequestCommand command)
    {
        return await _mediator.Send(command);
    }
}
