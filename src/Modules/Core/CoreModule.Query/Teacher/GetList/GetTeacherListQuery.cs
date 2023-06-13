using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.Teacher._DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.Teacher.GetList;

public record GetTeacherListQuery() : IQuery<List<TeacherDto>>;

class GetTeacherListQueryHandler : IQueryHandler<GetTeacherListQuery, List<TeacherDto>>
{
    private readonly QueryContext _context;

    public GetTeacherListQueryHandler(QueryContext context)
    {
        _context = context;
    }

    public async Task<List<TeacherDto>> Handle(GetTeacherListQuery request, CancellationToken cancellationToken)
    {
        return await _context.Teachers.Include(c => c.User).Select(e => new TeacherDto
        {
            Id = e.Id,
            UserName = e.UserName,
            Status = e.Status,
            CreationDate = e.CreationDate,
            CvFileName = e.CvFileName,
            User = new Query._DTOs.CoreModuleUserDto()
            {
                Id = e.User.Id,
                Name = e.User.Name,
                Family = e.User.Family,
                Email = e.User.Email,
                Avatar = e.User.Avatar,
                CreationDate = e.User.CreationDate,
                PhoneNumber = e.User.PhoneNumber
            }
        }).ToListAsync();


    }
}
