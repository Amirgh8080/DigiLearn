
using Common.Application;

namespace CoreModule.Application.Courses.Sections.AddSection;

public record AddCourseSectionCommand(Guid CourseId,string Title,int DisplayOrder):IBaseCommand;
