using LoanProcessor.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
	public class MockRulesEngineFactory
	{
		public RulesEngine getMockRulesEngine()
		{
			RulesEngine rulesEngine = new RulesEngine(
				new Mock_California_Validator(),
				new Mock_Florida_Validator(),
				new Mock_Maryland_Validator(),
				new Mock_New_York_Validator(),
				new Mock_Virginia_Validator()
				);
			return rulesEngine;
		}
	}
}
