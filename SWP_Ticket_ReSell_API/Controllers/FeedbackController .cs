using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using SWP_Ticket_ReSell_DAO.Models;

namespace SWP_Ticket_ReSell_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly ServiceBase<Customer> _serviceCustomer;
        private readonly ServiceBase<Role> _serviceRole;
        private readonly ServiceBase<Package> _servicePackage;
        private readonly ServiceBase<Feedback> _serviceFeedback;
        private readonly ServiceBase<Boxchat> _serviceBoxchat;
        private readonly ServiceBase<Notification> _serviceNotification;
        private readonly ServiceBase<Order> _serviceOrder;
        private readonly ServiceBase<Report> _serviceReport;
        private readonly ServiceBase<Request> _serviceRequest;
        private readonly ServiceBase<Ticket> _serviceTicket;


        public FeedbackController(ServiceBase<Order> serviceOrder, ServiceBase<Customer> serviceCustomer)
        {
            _serviceOrder = serviceOrder;
            _serviceCustomer = serviceCustomer;
        }



    }
}
