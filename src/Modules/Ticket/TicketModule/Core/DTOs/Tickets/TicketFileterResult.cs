using Common.Query.Filter;
using TicketModule.Data.Entities;

namespace TicketModule.Core.DTOs.Tickets;

public class TicketFilterData
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public DateTime CreationDate { get; set; }
    public TicketStatus Status { get; set; }
}
public class TicketFilterResult:BaseFilter<TicketFilterData>
{

}
public class TicketFilterParam : BaseFilterParam
{
    public Guid? UserId { get; set; }

}