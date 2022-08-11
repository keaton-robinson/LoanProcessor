namespace LoanProcessor.Models
{
	public class LoanValidationApiDTO
	{

		public LoanValidationApiDTO(Loan loan, bool passed)
		{
			Loan = loan;
			this.allTestsPassed = passed;
		}

		public Loan Loan { get; set; }
		public bool allTestsPassed { get; set; }
	}
}
