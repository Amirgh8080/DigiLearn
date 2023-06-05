using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.Teacher._DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.Teacher.GetById;

public record GetTeacherByIdQuery(Guid Id) : IQuery<TeacherDto?>;

class GetTeacherByIdQueryHandler : IQueryHandler<GetTeacherByIdQuery, TeacherDto?>
{
    private readonly QueryContext _context;

    public GetTeacherByIdQueryHandler(QueryContext context)
    {
        _context = context;
    }

    public async Task<TeacherDto?> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
    {
        var model =await _context.Teachers
            .Include(f=>f.User)
            .FirstOrDefaultAsync(f => f.Id == request.Id,cancellationToken);

        if(model == null)
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