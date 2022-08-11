using LoanProcessor.Constants;
using LoanProcessor.Models;

namespace LoanProcessor.Rules
{
	public class Maryland_Validator
	{
		public virtual void validateLoan(Loan loan)
		{
			if (loan.Amount <= 400000.0m)
			{

				// ******* Max APR Check *******************************************************************************************
				LoanTestResult Maryland_APR_Test_Result = new LoanTestResult()
				{
					Name = "Maryland Max APR Test",
					Description = "Max APR 4%.",
					Passed = false //set to false by default to be safe
				};

				if (loan.APR < 0.04m)
				{
					Maryland_APR_Test_Result.Passed = true;
				}

				loan.Tests.Add(Maryland_APR_Test_Result);




				// ******* Max Fee Check *******************************************************************************************	
				LoanTestResult Maryland_Max_Fee_Test_Result = new LoanTestResult()
				{
					Name = "Maryland Max Fee Test",
					Description = "For loan amounts less than or equal to $200,000, sum of application fee and credit report fee cannot exceed 4%. For loan amounts greater than $200,000, sum of application fee and credit report fee cannot exceed 6%.",
					Passed = false
				};

				decimal maximumFeeAllowed;
				decimal feeSum = 0.0m;
				List<String> includedFeeTypes = new List<String> { FeeTypes.Application, FeeTypes.Credit_Report };
				foreach (Fee fee in loan.Fees)
				{
					if (includedFeeTypes.Contains(fee.Type))
					{
						feeSum += fee.Amount;
					}
				}
				if(loan.Amount <= 200000.0m)
				{
					maximumFeeAllowed = loan.Amount * 0.04m;
				} 
				else
				{
					maximumFeeAllowed = loan.Amount * 0.06m;
				}

				if (feeSum <= maximumFeeAllowed)
				{
					Maryland_Max_Fee_Test_Result.Passed = true;
				}

				loan.Tests.Add(Maryland_Max_Fee_Test_Result);
			}
		}
	}
}
