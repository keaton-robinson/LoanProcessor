namespace LoanProcessor.Rules
{
	public class RulesEngineFactory
	{
		public RulesEngine getRulesEngine()
		{
			RulesEngine rulesEngine = new RulesEngine(
				new California_Validator(),
				new Florida_Validator(),
				new Maryland_Validator(),
				new New_York_Validator(),
				new Virginia_Validator()
				);

			return rulesEngine;
		}
	}
}
