using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RequestLoan
    {
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public string PaymentFrequency { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public int Description { get; set; }
        public DateTime RespomseDate { get; set; }
        public string? LaboralCertificate { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; } = new();
    }
}
