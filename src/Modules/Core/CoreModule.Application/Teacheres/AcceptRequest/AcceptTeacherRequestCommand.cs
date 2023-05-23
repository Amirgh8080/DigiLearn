using Common.Application;

namespace CoreModule.Application.Teacheres.AcceptRequest;

public record AcceptTeacherRequestCommand(Guid TeacherId) : IBaseCommand;

