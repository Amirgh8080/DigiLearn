using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.Teacher._DTOs;
using CoreModule.Query.Teacher.GetById;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.Teacher.GetByUserId;

public record GetTeacherByUserIdQuery(Guid UserId) : IQuery<TeacherDto?>;

class GetTeacherByUserIdQueryHandler : IQueryHandler<GetTeacherByUserIdQuery, TeacherDto?>
{
    private readonly QueryContext _context;

    public GetTeacherByUserIdQueryHandler(QueryContext context)
    {
        _context = context;
    }

    public async Task<TeacherDto?> Handle(GetTeacherByUserIdQuery request, CancellationToken cancellationToken)
    {
        var model = await _context.Teachers
            .Include(f => f.User)
            .FirstOrDefaultAsync(f => f.UserId == request.UserId, cancellationToken);

        if (model == null)
        {
            return null;
        }
        return new TeacherDto()
        {
            Id = model.Id,
            CreationDate = model.CreationDate,
            CvFileName = model.CvFileName,
            Status = model.Status,
            UserName = model.UserName,
            User = new Query._DTOs.CoreModuleUserDto()
            {
                Avatar = model.User.Avatar,
                CreationDate = model.User.CreationDate,
                Id = model.User.Id,
                Email = model.User.Email,
                Name = model.User.Name,
                Family = model.User.Family,
                PhoneNumber = model.User.PhoneNumber
            }
        };
    }
}

