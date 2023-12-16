using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime LimitDate { get; set; }
        public double Amount { get; set; }
        public bool LatePayment { get; set; }
        public int LoanId { get; set; }
        public Loan Loan { get; set; } = new();
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = new();
    }
}
