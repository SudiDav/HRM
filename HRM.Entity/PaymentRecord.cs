using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HRM.Entity
{
    public class PaymentRecord
    {
        public int Id { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [MaxLength(100)]
        public string FullName { get; set; }
        public string NInsuranceNo { get; set; }   //National Insurance Number
        public DateTime PayDate { get; set; }
        public string PayMonth { get; set; }
        [ForeignKey("TaxYear")]
        public int TaxYearId { get; set; }
        public TaxYear TaxYear  { get; set; }
        public string TaxCode { get; set; }
        [Column(TypeName = "money")]
        public decimal HourlyRate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal HoursWorked { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal COntractualHours { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OverTimeHour { get; set; }
        [Column(TypeName = "decimal(18, 2)")]  // 18 represent the scale and 2 represent 2 decimal to the right eg: 10000.02
        public decimal ContractualEarnings { get; set; }
        [Column(TypeName = "money")]
        public decimal OverTimeEarings { get; set; }
        [Column(TypeName = "money")]
        public decimal Tax { get; set; }         
        [Column(TypeName = "money")]
        public decimal NIC { get; set; } //National Insurance Contribution
        [Column(TypeName = "money")]
        public decimal? UnionFee { get; set; }
        // Nullable is the same as a question mark for making a property optional or null
        public Nullable<decimal> SLC { get; set; } //Student Loan Company        
        [Column(TypeName = "money")]
        public decimal TotalEarnings { get; set; }
        [Column(TypeName = "money")]
        public decimal TotalDeduction { get; set; }
        [Column(TypeName ="money")]
        public decimal NetPayment { get; set; }
    }
}
