using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanProcessor.Models;
using LoanProcessor.Rules;

namespace UnitTests
{
	public class Mock_California_Validator : California_Validator
	{
		public override void validateLoan(Loan loan)
		{
			loan.Tests.Add(new LoanTestResult() { Name = "Mock_California_Test", Passed = true });
			loan.Tests.Add(new LoanTestResult() { Name = "Mock_California_Passed_Test", Passed = true });
		}
	}

	public class Mock_Florida_Validator : Florida_Validator
	{
		public override void validateLoan(Loan loan)
		{
			loan.Tests.Add(new LoanTestResult() { Name = "Mock_Florida_Test", Passed = true });
			loan.Tests.Add(new LoanTestResult() { Name = "Mock_Florida_Failed_test", Passed = false });
		}
	}
	public class Mock_Maryland_Validator : Maryland_Validator
	{
		public override void validateLoan(Loan loan)
		{
			loan.Tests.Add(new LoanTestResult() { Name = "Mock_Maryland_Test", Passed = true });
		}
	}
	public class Mock_New_York_Validator : New_York_Validator
	{
		public override void validateLoan(Loan loan)
		{
			loan.Tests.Add(new LoanTestResult() { Name = "Mock_New_York_Test", Passed = true });
		}
	}

	public class Mock_Virginia_Validator : Virginia_Validator
	{
		public override void validateLoan(Loan loan)
		{
			loan.Tests.Add(new LoanTestResult() { Name = "Mock_Virginia_Test", Passed = true });
		}
	}
}
