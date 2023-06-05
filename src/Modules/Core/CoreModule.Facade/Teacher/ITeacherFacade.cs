using Common.Application;
using CoreModule.Application.Teacheres.AcceptRequest;
using CoreModule.Application.Teacheres.RejectRequest;
using CoreModule.Query.Teacher._DTOs;
using CoreModule.Query.Teacher.GetById;
using CoreModule.Query.Teacher.GetByUserId;
using CoreModule.Query.Teacher.GetList;
using MediatR;

namespace CoreModule.Facade.Teacher;

public interface ITeacherFacade
{
    Task<OperationResult> Register(RegisterTeacherCommand command);
    Task<OperationResult> AcceptTeacherRequest(AcceptTeacherRequestCommand command);
    Task<OperationResult> RejectTeacherRequest(RejectTeacherRequestCommand command);


    Task<TeacherDto?> GetById(Guid id);
    Task<TeacherDto?> GetByUserId(Guid userId);
    Task<List<TeacherDto>> GetList();
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

    public async Task<TeacherDto?> GetById(Guid id)
    {
        return await _mediator.Send(new GetTeacherByIdQuery(id));
    }

    public async Task<TeacherDto?> GetByUserId(Guid userId)
    {
        return await _mediator.Send(new GetTeacherByUserIdQuery(userId));
    }

    public async Task<List<TeacherDto>> GetList()
    {
        return await _mediator.Send(new GetTeacherListQuery());
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
