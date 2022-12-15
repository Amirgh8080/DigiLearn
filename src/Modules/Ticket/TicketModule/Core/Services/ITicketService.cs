using AutoMapper;
using Common.Application;
using Common.Application.SecurityUtil;
using Microsoft.EntityFrameworkCore;
using TicketModule.Core.DTOs.Tickets;
using TicketModule.Data;
using TicketModule.Data.Entities;

namespace TicketModule.Core.Services;

public interface ITicketService
{
    Task<OperationResult<Guid>> CreateTicket(CreateTicketCommand command);
    Task<OperationResult> SendTicketmessage(SendTicketMessageCommand command);
    Task<OperationResult> CloseTicket(Guid ticketId);


    Task<TicketDto> GetTicket(Guid ticketId);
}
class TicketService : ITicketService
{
    #region Injections
    private readonly TicketContext _context;
    private readonly IMapper _mapper;

    public TicketService(TicketContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    #endregion
    public async Task<OperationResult> CloseTicket(Guid ticketId)
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);

        if (ticket == null)
            return OperationResult.NotFound();

        ticket.Status = TicketStatus.Closed;
        _context.Tickets.Update(ticket);
        await _context.SaveChangesAsync();

        return OperationResult.Success();
    }

    public async Task<OperationResult<Guid>> CreateTicket(CreateTicketCommand command)
    {
        var ticket = _mapper.Map<Ticket>(command);

        _context.Tickets.Add(ticket);

        await _context.SaveChangesAsync();
        return OperationResult<Guid>.Success(ticket.Id);
    }

    public async Task<TicketDto> GetTicket(Guid ticketId)
    {
        var ticket =await _context.Tickets
                            .Include(t => t.Messages)
                            .FirstOrDefaultAsync(t => t.Id == ticketId);

        return _mapper.Map<TicketDto>(ticket);
    }

    public async Task<OperationResult> SendTicketmessage(SendTicketMessageCommand command)
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == command.TicketId);
        if (ticket == null)
            return OperationResult.NotFound();

        var message = new TicketMessage()
        {
            Text = command.Text.SanitizeText(),
            TicketId = command.TicketId,
            UserId = command.UserId,
            UserFullName = command.OwnerFullName
        };

        if(ticket.UserId == command.UserId)
        {
            ticket.Status = TicketStatus.Pending;
        }
        else
        {
            ticket.Status = TicketStatus.Answered;
        }

        _context.TicketMessages.Add(message);
        _context.Tickets.Update(ticket);
        await _context.SaveChangesAsync();

        return OperationResult.Success();
    }
}
