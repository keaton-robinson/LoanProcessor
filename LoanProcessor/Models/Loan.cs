using System.Collections;

namespace LoanProcessor.Models
{
	public class Loan
	{
		public decimal Amount { get; set; }
		public decimal APR { get; set; }
		public string Type { get; set; }
		public string State { get; set; }
		public bool PrimaryResidence { get; set; }
		public List<Fee> Fees { get; set; }
		public List<LoanTestResult> Tests {get;set;}

		public Loan()
		{
			Fees = new List<Fee>();
			Tests = new List<LoanTestResult>();
			
		}
	}
}
