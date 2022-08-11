using LoanProcessor.Constants;
using LoanProcessor.Models;

namespace LoanProcessor.Rules
{
    public class New_York_Validator
    {
		public virtual void validateLoan(Loan loan)
        {
            if (loan.Amount <= 750000m && loan.Type == LoanTypes.Conventional)
            {
				LoanTestResult New_York_APR_Test_Result = new LoanTestResult()
				{
					Name = "New York Max APR Test",
					Description = "Maximum APR check for New York loans. Conventional loans only. Max APR with primary occupancy: 6%. Max APR without primary occupancy: 8%.",
					Passed = false //set to false by default to be safe
				};

				if (loan.PrimaryResidence && loan.APR <= 0.06m || (!loan.PrimaryResidence && loan.APR <= 0.08m))
                {
                    New_York_APR_Test_Result.Passed = true;
                }

				loan.Tests.Add(New_York_APR_Test_Result);
			}
        }
    }
}
