using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        public DateTime LoanDate { get; set; }
        public string State { get; set; } = string.Empty;
        public int RequestLoanId { get; set; }
        public RequestLoan RequestLoan { get; set; } = new();
        public int ClientId { get; set; }
        public Client Client { get; set; } = new();
    }
}
