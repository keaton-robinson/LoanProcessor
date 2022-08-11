using LoanProcessor.Models;
using LoanProcessor.Rules;

namespace LoanProcessor
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			var app = builder.Build();

			RulesEngine rulesEngine = new RulesEngineFactory().getRulesEngine();

			/**
				Assume this service is hosted at domain/LoanTest  This endpoint doesn't create loans. It just validates them.
				It only exposes a post endpoint which takes in loan parameters and returns pass/fail info about whether the loan passes loan tests
				I suppose to be truly restful...it could create a record of the fact that a user submitted a loan for validation
				so that the http post could actually create a record (to conform to REST)
			*/

			app.MapPost("/", (Loan loan) =>
			{
				bool passed = rulesEngine.validateLoan(loan);
				LoanValidationApiDTO apiCallResult = new LoanValidationApiDTO(loan, passed);
				return Results.Ok(apiCallResult);
			});

			app.Run();
		}
	}
}