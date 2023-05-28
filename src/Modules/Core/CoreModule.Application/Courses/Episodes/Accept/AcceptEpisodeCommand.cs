using Common.Application;
using CoreModule.Domain.Course.Models;

namespace CoreModule.Application.Courses.Episodes.Accept;

public record AcceptEpisodeCommand(Guid EpisodeId,Guid CourseId) :IBaseCommand;

