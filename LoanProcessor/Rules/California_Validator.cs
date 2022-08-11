using LoanProcessor.Constants;
using LoanProcessor.Models;

namespace LoanProcessor.Rules
{
	public class California_Validator
	{

		public virtual void validateLoan(Loan loan)
		{
			if (loan.Amount <= 600000.0m)
			{

				// ******* Max APR Check *******************************************************************************************
				LoanTestResult California_APR_Test_Result = new LoanTestResult()
				{
					Name = "California Max APR Test",
					Description = "Conventional and FHA Loans: Max with primary occupancy: 5%. Max without primary occupancy: 4%. All VA loans: Maximum APR: 3%",
					Passed = false //set to false by default to be safe
				};

				if(loan.Type == LoanTypes.Conventional || loan.Type == LoanTypes.FHA)
				{
					if(loan.PrimaryResidence && loan.APR <= 0.05m || (!loan.PrimaryResidence && loan.APR <= 0.04m))
					{
						California_APR_Test_Result.Passed = true;
					}
				}
				else if( loan.Type == LoanTypes.VA && loan.APR <= 0.03m )
				{
					California_APR_Test_Result.Passed = true;
				}

				loan.Tests.Add(California_APR_Test_Result);




				// ******* Max Fee Check *******************************************************************************************	
				LoanTestResult California_Max_Fee_Test_Result = new LoanTestResult()
				{
					Name = "California Max Fee Test",
					Description = "Validates max fee for application and settlement fees. Max fees for loan amounts <= to $50,000: 3%. Max fees for loan amounts > $50,000 and <= $150,000: 4%. Max fees for loan amounts > $150,000: 5%.",
					Passed = false
				};

				decimal maximumFeeAllowed;
				decimal feeSum = 0.0m;
				List<String> includedFeeTypes = new List<String> { FeeTypes.Application, FeeTypes.Settlement};
				foreach (Fee fee in loan.Fees)
				{
					if (includedFeeTypes.Contains(fee.Type))
					{
						feeSum += fee.Amount;
					}
				}
				if (loan.Amount <= 50000.0m)
				{
					maximumFeeAllowed = loan.Amount * 0.03m;
				}
				else if( loan.Amount <= 150000.0m)
				{
					maximumFeeAllowed = loan.Amount * 0.04m;
				} 
				else
				{
					maximumFeeAllowed = loan.Amount * 0.05m;
				}

				if (feeSum <= maximumFeeAllowed)
				{
					California_Max_Fee_Test_Result.Passed = true;
				}

				loan.Tests.Add(California_Max_Fee_Test_Result);
			}
		}
	}
}
