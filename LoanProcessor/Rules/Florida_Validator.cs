using LoanProcessor.Constants;
using LoanProcessor.Models;

namespace LoanProcessor.Rules
{
	public class Florida_Validator
	{

		public virtual void validateLoan(Loan loan)
		{
			if (loan.Type == LoanTypes.Conventional || loan.Type == LoanTypes.VA)
			{
				// ******* Max Fee Check *******************************************************************************************	
				LoanTestResult Florida_Max_Fee_Test_Result = new LoanTestResult()
				{
					Name = "Florida Max Fee Test",
					Description = "Validates max fee for application, flood certification, and title search fees. Max fees for loan amounts <= to $20,000: 6%. Max fees for loan amounts > $20,000 and <= $75,000: 8%. Max fees for loan amounts > $75,000 and <= $150,000: 9%. Max fees for loan amount > $150,000: 10%.",
					Passed = false
				};

				decimal maximumFeeAllowed;
				decimal feeSum = 0.0m;
				List<String> includedFeeTypes = new List<String> { FeeTypes.Application, FeeTypes.Flood_Certification, FeeTypes.Title_Search };
				foreach (Fee fee in loan.Fees)
				{
					if (includedFeeTypes.Contains(fee.Type))
					{
						feeSum += fee.Amount;
					}
				}
				if (loan.Amount <= 20000.0m)
				{
					maximumFeeAllowed = loan.Amount * 0.06m;
				}
				else if (loan.Amount <= 75000.0m)
				{
					maximumFeeAllowed = loan.Amount * 0.08m;
				}
				else if (loan.Amount <= 150000.0m)
				{
					maximumFeeAllowed = loan.Amount * 0.09m;
				}
				else
				{
					maximumFeeAllowed = loan.Amount * 0.10m;
				}

				if (feeSum <= maximumFeeAllowed)
				{
					Florida_Max_Fee_Test_Result.Passed = true;
				}

				loan.Tests.Add(Florida_Max_Fee_Test_Result);
			}
		}
	}
}
