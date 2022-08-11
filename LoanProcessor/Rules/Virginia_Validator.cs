using LoanProcessor.Constants;
using LoanProcessor.Models;

namespace LoanProcessor.Rules
{
	public class Virginia_Validator
	{
		public virtual void validateLoan(Loan loan)
		{
			// Max APR check
			LoanTestResult Virginia_APR_Test_Result = new LoanTestResult()
			{
				Name = "Virginia Max APR Test",
				Description = "Maximum APR check for Virginia loans. Max APR with primary occupancy: 5%. Max APR without primary occupancy: 8%.",
				Passed = false // explicitly set to false by default to be safe
			};

			if (loan.PrimaryResidence && loan.APR <= 0.05m || (!loan.PrimaryResidence && loan.APR <= 0.08m))
			{
				Virginia_APR_Test_Result.Passed = true;
			}

			loan.Tests.Add(Virginia_APR_Test_Result);




			//Max Fee check			
			LoanTestResult Virginia_Fee_Max_Test_Result = new LoanTestResult()
			{
				Name = "Virginia Max Fee Test",
				Description = "Sum of flood certification, processing, and settlement fees cannot exceed 7%.",
				Passed = false
			};

			decimal maximumFeeAllowed = 0.07m * loan.Amount;
			decimal feeSum = 0.0m;
			List<String> includedFeeTypes = new List<String> { FeeTypes.Flood_Certification, FeeTypes.Processing, FeeTypes.Settlement };
			foreach(Fee fee in loan.Fees)
			{
				if (includedFeeTypes.Contains(fee.Type))
				{
					feeSum += fee.Amount;
				}
			}
			
			if(feeSum <= maximumFeeAllowed)
			{
				Virginia_Fee_Max_Test_Result.Passed = true;
			}

			loan.Tests.Add(Virginia_Fee_Max_Test_Result);

		}
	}
}
