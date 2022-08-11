using LoanProcessor.Constants;
using LoanProcessor.Models;
using LoanProcessor.Rules;

namespace UnitTests
{
	[TestClass]
	public class New_York_Validator_Tests
	{
		[TestMethod]
		public void New_York_Validator_Only_Runs_Tests_When_Amount_Less_Than_750000_And_Conventional_Loan()
		{
			// Arrange
			New_York_Validator new_york_validator = new New_York_Validator();
			Loan loanThatShouldRunTests = new Loan() { Amount = 750000.0m, Type = LoanTypes.Conventional };
			Loan loanWithTooLargeAmount = new Loan() { Amount = 750000.01m, Type = LoanTypes.Conventional };
			Loan loanThatIsNotConventional = new Loan() { Amount = 1.0m, Type = LoanTypes.VA };

			// Act
			new_york_validator.validateLoan(loanThatShouldRunTests);
			new_york_validator.validateLoan(loanWithTooLargeAmount);
			new_york_validator.validateLoan(loanThatIsNotConventional);

			// Assert
			Assert.IsTrue(loanThatShouldRunTests.Tests.Count > 0);
			Assert.IsTrue(loanWithTooLargeAmount.Tests.Count == 0);
			Assert.IsTrue(loanThatIsNotConventional.Tests.Count == 0);
		}

		[TestMethod]
		public void New_York_Max_APR_Primary_Occupancy_Loan_Is_6_Pct()
		{
			// Arrange
			New_York_Validator new_york_validator = new New_York_Validator();
			Loan maxAprPrimaryLoan = new Loan() { Amount = 5000.0m, Type = LoanTypes.Conventional, APR = 0.06m, PrimaryResidence = true };
			Loan tooLargeAPRPrimaryLoan = new Loan() { Amount = 5000.0m, Type = LoanTypes.Conventional, APR = 0.061m, PrimaryResidence = true };
			Loan zeroAPRPrimaryLoan = new Loan() { Amount = 5000.0m, Type = LoanTypes.Conventional, APR = 0.0m, PrimaryResidence = true };

			// Act
			new_york_validator.validateLoan(maxAprPrimaryLoan);
			new_york_validator.validateLoan(tooLargeAPRPrimaryLoan);
			new_york_validator.validateLoan(zeroAPRPrimaryLoan);

			// Assert
			Assert.IsTrue(maxAprPrimaryLoan.Tests[0].Passed);
			Assert.IsFalse(tooLargeAPRPrimaryLoan.Tests[0].Passed);
			Assert.IsTrue(zeroAPRPrimaryLoan.Tests[0].Passed);
		}

		[TestMethod]
		public void New_York_Max_APR_Secondary_Occupancy_Loan_Is_8_Pct()
		{
			// Arrange
			New_York_Validator new_york_validator = new New_York_Validator();
			Loan maxAPRSecondaryLoan = new Loan() { Amount = 5000.0m, Type = LoanTypes.Conventional, APR = 0.08m, PrimaryResidence = false };
			Loan tooLargeAPRSecondaryLoan = new Loan() { Amount = 5000.0m, Type = LoanTypes.Conventional, APR = 0.081m, PrimaryResidence = false };
			Loan zeroAPRSecondaryLoan = new Loan() { Amount = 5000.0m, Type = LoanTypes.Conventional, APR = 0.0m, PrimaryResidence = false };

			// Act
			new_york_validator.validateLoan(maxAPRSecondaryLoan);
			new_york_validator.validateLoan(tooLargeAPRSecondaryLoan);
			new_york_validator.validateLoan(zeroAPRSecondaryLoan);

			// Assert
			Assert.IsTrue(maxAPRSecondaryLoan.Tests[0].Passed);
			Assert.IsFalse(tooLargeAPRSecondaryLoan.Tests[0].Passed);
			Assert.IsTrue(zeroAPRSecondaryLoan.Tests[0].Passed);
		}
	}
}