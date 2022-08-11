using LoanProcessor.Models;
using LoanProcessor.Rules;

namespace UnitTests
{
	[TestClass]
	public class RulesEngineTest
	{
		[TestMethod]
		public void Rules_Engine_Selects_New_York_When_State_Is_New_York()
		{
			// Arrange
			string expectedTestName = "Mock_New_York_Test";
			RulesEngine rulesEngine = new MockRulesEngineFactory().getMockRulesEngine();
			Loan newYorkLoan = new Loan() { State = "new york" };

			// Act
			rulesEngine.validateLoan(newYorkLoan);

			// Assert
			string actualTestName = newYorkLoan.Tests[0].Name;
			Assert.AreEqual(expectedTestName, actualTestName);
		}

		[TestMethod]
		public void Rules_Engine_Selects_Florida_When_State_Is_Florida()
		{
			// Arrange
			string expectedTestName = "Mock_Florida_Test";
			RulesEngine rulesEngine = new MockRulesEngineFactory().getMockRulesEngine();
			Loan floridaLoan = new Loan() { State = "florida" };

			// Act
			rulesEngine.validateLoan(floridaLoan);

			// Assert
			string actualTestName = floridaLoan.Tests[0].Name;
			Assert.AreEqual(expectedTestName, actualTestName);
		}

		[TestMethod]
		public void Rules_Engine_Returns_True_When_All_Tests_True()
		{
			RulesEngine rulesEngine = new MockRulesEngineFactory().getMockRulesEngine();
			Loan loanWithPassingTests = new Loan() { State = "california" };


			bool result = rulesEngine.validateLoan(loanWithPassingTests);

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void Rules_Engine_Returns_False_When_Any_Test_False()
		{
			RulesEngine rulesEngine = new MockRulesEngineFactory().getMockRulesEngine();
			Loan loanWithFailingTests = new Loan() { State = "florida" };

			bool result = rulesEngine.validateLoan(loanWithFailingTests);

			Assert.IsFalse(result);

		}
	}
}