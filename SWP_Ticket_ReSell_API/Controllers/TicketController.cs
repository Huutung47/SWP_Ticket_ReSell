using Mapster;
using Microsoft.AspNetCore.Mvc;
using Repository;
using SWP_Ticket_ReSell_DAO.DTO.Ticket;
using SWP_Ticket_ReSell_DAO.Models;
//using SWP_Ticket_ReSell_Repository;

namespace SWP_Ticket_ReSell_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ServiceBase<Ticket> _service;
        private readonly ServiceBase<Role> _serviceRole;
        private readonly ServiceBase<Package> _servicePackage;
        private readonly GenericRepository<Ticket> _ticketRepository;
        public TicketController(ServiceBase<Ticket> service, ServiceBase<Role> serviceRole, ServiceBase<Package> servicePackage, GenericRepository<Ticket> ticketRepository)
        {
            _service = service;
            _serviceRole = serviceRole;
            _servicePackage = servicePackage;
            _ticketRepository = ticketRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IList<TicketResponseDTO>>> GetCustomer()
        {
            var entities = await _service.FindListAsync<TicketResponseDTO>();
            return Ok(entities);
        }
       
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketResponseDTO>> GetTicket(string id)
        {
            var entity = await _service.FindByAsync(p => p.ID_Ticket.ToString() == id);
            if (entity == null)
            {
                return Problem(detail: $"Ticket id {id} cannot found", statusCode: 404);
            }
            return Ok(entity.Adapt<TicketResponseDTO>());
        }

        [HttpGet("seller/{sellerId}")]
        public async Task<ActionResult<IEnumerable<TicketResponseDTO>>> GetTicketsBySellerId(int sellerId)
        {
            var tickets = await _ticketRepository.GetByIdCustomer(sellerId);
            if (tickets == null || !tickets.Any())
            {
                return NotFound();
            }

            return Ok(tickets.Select(t => new TicketResponseDTO
            {
                ID_Ticket = t.ID_Ticket,
                ID_Customer = t.ID_Customer,
                Price = t.Price,
                Ticket_category = t.Ticket_category,
                Ticket_type = t.Ticket_type,
                Buyer = t.Buyer,
                Quantity = t.Quantity,
                Ticket_History = t.Ticket_History,
                Status = t.Status,
                Show_Name = t.Show_Name,
                Description = t.Description,
            }));
        }

        [HttpPut]
        public async Task<IActionResult> PutTicket(TicketResponseDTO ticketRequest)
        {
            var entity = await _service.FindByAsync(p => p.ID_Customer == ticketRequest.ID_Customer);
            if (entity == null)
            {
                return Problem(detail: $"Customer_id {ticketRequest.ID_Customer} cannot found", statusCode: 404);
            }

            if (!await _servicePackage.ExistsByAsync(p => p.ID_Package == ticketRequest.ID_Ticket))
            {
                return Problem(detail: $"Ticket_id {ticketRequest.ID_Ticket} cannot found", statusCode: 404);
            }

            ticketRequest.Adapt(entity);
            await _service.UpdateAsync(entity);
            return Ok("Update ticket successfull.");
        }

        [HttpPost]
        public async Task<ActionResult<TicketResponseDTO>> PostTicket(TicketCreateDTO ticketRequest)
        {
            //Validation

            var ticket = new Ticket()
            {
                ID_Customer = ticketRequest.ID_Customer,
                Buyer = "",
                Ticket_History = DateTime.Now,
                Status = "Available"
            };
            ticketRequest.Adapt(ticket); // chuyển data vào request checking regex
            await _service.CreateAsync(ticket);
            return Ok("Create ticket successfull.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var ticket = await _service.FindByAsync(p => p.ID_Ticket == id);
            if (ticket == null)
            {
                return Problem(detail: $"ticket_id {id} cannot found", statusCode: 404);
            }

            await _service.DeleteAsync(ticket);
            return Ok("Delete ticket successfull.");
        }
    }
}
