using SWP_Ticket_ReSell_DAO.DTO.Package;
using SWP_Ticket_ReSell_DAO.DTO.Role;
using SWP_Ticket_ReSell_DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP_Ticket_ReSell_DAO.DTO.Customer
{
    public class CustomerResponseDTO
    {
        public int IdCustomer { get; set; }

        public string? Name { get; set; }

        public string? Contact { get; set; }

        public string? Email { get; set; }

        public decimal? AverageFeedback { get; set; }

        public int? IdRole { get; set; } // bỏ  ID này nếu thấy thừa

        public int? IdPackage { get; set; } // bỏ ID này nếu thấy thừa

        public DateTime? PackageExpirationDate { get; set; }

        public virtual PackageDTO? IdPackageNavigation { get; set; }

        public virtual RoleDTO? IdRoleNavigation { get; set; }
    }
}
