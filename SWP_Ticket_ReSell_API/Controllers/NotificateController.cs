using Castle.Core.Resource;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using SWP_Ticket_ReSell_DAO.DTO.Notificate;
using SWP_Ticket_ReSell_DAO.DTO.Ticket;
using SWP_Ticket_ReSell_DAO.Models;

namespace SWP_Ticket_ReSell_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificateController : ControllerBase
    {
        private readonly ServiceBase<Ticket> _serviceTicket;
        private readonly ServiceBase<Notification> _serviceNotification;
        private readonly GenericRepository<Notification> _notificationRepository;

        public NotificateController(ServiceBase<Ticket> serviceTicket, ServiceBase<Notification> serviceNotification, GenericRepository<Notification> notificationRepository)
        {
            _serviceTicket = serviceTicket;
            _serviceNotification = serviceNotification;
            _notificationRepository = notificationRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IList<NotificateDTO>>> GetNotificate()
        {
            var entities = await _serviceNotification.FindListAsync<NotificateDTO>();
            return Ok(entities);
        }
        //[HttpGet("ticket/{ticketId}")]
        //public async Task<ActionResult<IEnumerable<NotificateDTO>>> GetNotificationsByTicket(int ticketId)
        //{
        //    var notifications = await _notificationRepository.GetNotificationsByTicketIdAsync(ticketId);
        //    if (notifications == null || !notifications.Any())
        //    {
        //        return NotFound();
        //    }

        //    return Ok(notifications.Select(n => new NotificateDTO
        //    {
        //        Title = n.Title,
        //        Event = n.Event,
        //        Organization_day = n.Organization_day,
        //        Organizing_time = n.Organizing_time,
        //    }));
        //}

        [HttpGet("{id}")]
            public async Task<ActionResult<NotificateDTO>> GetNotificate(string id)
            {
            var entity = await _serviceNotification.FindByAsync(p => p.ID_Notification.ToString() == id);
            if (entity == null)
                {
                    return Problem(detail: $"Notificate id {id} cannot found", statusCode: 404);
                }
                return Ok(entity.Adapt<NotificateDTO>());
            }

        [HttpPut]
        public async Task<IActionResult> PutNotificate(NotificateDTO notificate)
        {
            var entity = await _serviceNotification.FindByAsync(p => p.ID_Notification == notificate.ID_Notification);
            if (entity == null)
            {
                return Problem(detail: $"Notificate_id {notificate.ID_Notification} cannot found", statusCode: 404);
            }
            notificate.Adapt(entity);
            await _serviceNotification.UpdateAsync(entity);
            return Ok("Update notificate successfull.");
        }

        [HttpPost]
        public async Task<ActionResult<TicketResponseDTO>> PostNotificate(NotificateDTO notificate)
        {
            var notificates = new Notification()
            {
                Title = notificate.Title,
                Event = notificate.Event,
                Organization_day = notificate.Organization_day,
                Organizing_time = notificate.Organizing_time,
                ID_Ticket = notificate.ID_Ticket,
            };
            notificate.Adapt(notificates); 
            await _serviceNotification.CreateAsync(notificates);
            return Ok("Create notificate successfull.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotificate(int id)
        {
            var notificate = await _serviceNotification.FindByAsync(p => p.ID_Notification == id);
            if (notificate == null)
            {
                return Problem(detail: $"Notificate_id {id} cannot found", statusCode: 404);
            }

            await _serviceNotification.DeleteAsync(notificate);
            return Ok("Delete ticket successfull.");
        }
    }
}
