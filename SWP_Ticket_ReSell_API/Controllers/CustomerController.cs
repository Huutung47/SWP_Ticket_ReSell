using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;
using SWP_Ticket_ReSell_DAO.DTO.Customer;
using SWP_Ticket_ReSell_DAO.Models;

namespace SWP_Ticket_ReSell_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ServiceBase<Customer> _service;
        private readonly ServiceBase<Role> _serviceRole;
        private readonly ServiceBase<Package> _servicePackage;

        public CustomerController(ServiceBase<Customer> service, ServiceBase<Role> serviceRole, ServiceBase<Package> servicePackage)
        {
            _service = service;
            _serviceRole = serviceRole;
            _servicePackage = servicePackage;
        }

        [HttpGet]
        public async Task<ActionResult<IList<CustomerResponseDTO>>> GetSilverJewelries()
        {
            var entities = await _service.FindListAsync<CustomerResponseDTO>();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponseDTO>> GetSilverJewelry(string id)
        {
            var entity = await _service.FindByAsync(p => p.IdCustomer.ToString() == id);
            if (entity == null)
            {
                return Problem(detail: $"Customer id {id} cannot found", statusCode: 404);
            }
            return Ok(entity.Adapt<CustomerResponseDTO>());
        }

        [HttpPut]
        public async Task<IActionResult> PutCustomer(CustomerRequestDTO customerRequest)
        {
            var entity = await _service.FindByAsync(p => p.IdCustomer == customerRequest.IdCustomer);
            if (entity == null)
            {
                return Problem(detail: $"Customer_id {customerRequest.IdCustomer} cannot found", statusCode: 404);
            }

            if (!await _servicePackage.ExistsByAsync(p => p.IdPackage == customerRequest.IdPackage))
            {
                return Problem(detail: $"Package_id {customerRequest.IdPackage} cannot found", statusCode: 404);
            }

            customerRequest.Adapt(entity);
            await _service.UpdateAsync(entity);
            return Ok("Update customer successfull.");
        }

        [HttpPost]
        public async Task<ActionResult<CustomerResponseDTO>> PostCustomer(CustomerCreateDTO customerRequest)
        {
            if (await _service.ExistsByAsync(p => p.Email.Equals(customerRequest.Email)))
            {
                return Problem(detail: $"Email {customerRequest.Email} already exists", statusCode: 400);
            }

            if (!await _servicePackage.ExistsByAsync(p => p.IdPackage == customerRequest.IdPackage))
            {
                return Problem(detail: $"Package_id {customerRequest.IdPackage} cannot found", statusCode: 404);
            }

            var customer = new Customer();

            customerRequest.Adapt(customer); // chuyển data vào request checking regex

            await _service.CreateAsync(customer);
            return Ok("Create customer successfull.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _service.FindByAsync(p => p.IdCustomer == id);
            if (customer == null)
            {
                return Problem(detail: $"customer_id {id} cannot found", statusCode: 404);
            }

            await _service.DeleteAsync(customer);
            return Ok("Delete customer successfull.");
        }
    }
}
