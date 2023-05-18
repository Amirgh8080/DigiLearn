using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TicketModule.Core.DTOs.Tickets;
using TicketModule.Core.Services;

namespace DigiLearn.Web.Pages.Profile.Tickets
{
    public class ShowModel : BaseRazor
    {
        private readonly ITicketService _ticketService;

        public ShowModel(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public TicketDto Ticket { get; set; }


        [BindProperty]
        [Display(Name = "متن پیام")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Text { get; set; }

        public async Task<IActionResult> OnGet(Guid ticketId)
        {
            var ticket = await _ticketService.GetTicket(ticketId);
            if (ticket == null || ticket.UserId != User.GetUserId())
                return RedirectToPage("Index");

            Ticket = ticket;

            return Page();
        }
    }
}
