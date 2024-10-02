﻿using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using SWP_Ticket_ReSell_DAO.DTO.Customer;
using SWP_Ticket_ReSell_DAO.DTO.Package;
using SWP_Ticket_ReSell_DAO.Models;

namespace SWP_Ticket_ReSell_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly ServiceBase<Customer> _service;
        private readonly ServiceBase<Role> _serviceRole;
        private readonly ServiceBase<Package> _servicePackage;
        public PackageController(ServiceBase<Customer> service, ServiceBase<Role> serviceRole, ServiceBase<Package> servicePackage)
        {
            _service = service;
            _serviceRole = serviceRole;
            _servicePackage = servicePackage;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IList<FeedbackRequestDTO>>> GetPackage()
        {
            var entities = await _servicePackage.FindListAsync<FeedbackRequestDTO>();
            return Ok(entities);
        }

        //Customer xài
        //[Authorize(Roles = "2")]
        [HttpPost]
        public async Task<IActionResult> PackageChoose(FeedbackRequestDTO package)
        {
            // Tìm gói Package dựa trên ID_Package mà khách hàng chọn
            var packageFind = await _servicePackage.FindByAsync(p => p.ID_Package == package.ID_Package);
            //Nếu thấy Package
            if (packageFind != null)
            {
                //so với Customer
                var packageExist = await _service.FindByAsync(c => c.ID_Package == package.ID_Package);
                if (packageExist != null)
                {
                    // Kiểm tra xem gói khách hàng đã chọn có trùng với gói hiện tại không
                    if (packageExist.ID_Package == package.ID_Package)
                    {
                        // Nếu trùng, trả về lỗi và không cho đăng ký lại
                        return BadRequest(new { message = "You have already registered for this package. Please choose a different package." });
                    }
                    // Nếu không trùng, cập nhật gói mới
                    packageExist.ID_Package = package.ID_Package;
                    await _service.UpdateAsync(packageExist);  // Cập nhật thông tin khách hàng với gói mới
                    return Ok(new { message = "Package selected successfully", package });
                }
            }
            //Nếu không thấy Package 
            return NotFound(new { message = "Package not found" });
        }
    }
}
