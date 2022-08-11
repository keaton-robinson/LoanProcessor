using LoanProcessor.Constants;
using LoanProcessor.Models;

namespace LoanProcessor.Rules
{
	public class RulesEngine
	{

		California_Validator _california_validator;
		Florida_Validator _florida_validator;
		Maryland_Validator _maryland_validator;
		New_York_Validator _new_york_validator;
		Virginia_Validator _virginia_validator;

		public RulesEngine(
			California_Validator california_validator,
			Florida_Validator florida_validator,
			Maryland_Validator maryland_validator,
			New_York_Validator new_york_validator,
			Virginia_Validator virginia_validator
			)
		{
			_california_validator = california_validator;
			_florida_validator = florida_validator;
			_maryland_validator = maryland_validator;
			_new_york_validator = new_york_validator;
			_virginia_validator = virginia_validator;
		}

		public bool validateLoan(Loan loan)
		{
			bool testsPassed = true;

			switch (loan.State.ToLower())
			{
				case States.New_York:
					_new_york_validator.validateLoan(loan);
					break;
				case States.Virginia:
					_virginia_validator.validateLoan(loan);
					break;
				case States.Maryland:
					_maryland_validator.validateLoan(loan);

					break;
				case States.California:
					_california_validator.validateLoan(loan);

					break;
				case States.Florida:
					_florida_validator.validateLoan(loan);
					break;
			}

			bool passed = true;
			foreach(LoanTestResult loanTestResult in loan.Tests)
			{
				if (!(loanTestResult.Passed))
				{
					passed = false;
				}
			}
			return passed; 
		}
		
	}
}
