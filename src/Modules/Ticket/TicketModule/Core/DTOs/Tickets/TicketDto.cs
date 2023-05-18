using Common.Query;
using TicketModule.Data.Entities;

namespace TicketModule.Core.DTOs.Tickets;

public class TicketDto : BaseDto
{
    public Guid UserId { get; set; }
    public string OwnerFullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public TicketStatus TicketStatus { get; set; }
    public List<TicketMessageDto> Messages { get; set; }
}
