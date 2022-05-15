using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Response
{
    public class SignInResponsDTO
    {
        public string Email { get; set; }
        public string Fullname { get; set; }
        public List<string> Role { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryTime { get; set; }
        public decimal TotalAmountInvoiced { get; set; }
        public int TotalCustomers { get; set; }
        public IEnumerable<RecentInvoiceResponse> Invoices { get; set; }
        public ImageVM ImageDetails { get; set; }
    }
}
