namespace Nabble.Core.Test
{
	using System.Collections.Generic;
	using System.Linq;
	using Nabble.Core.Builder;
	using Nabble.Core.Sarif;
	using Xunit;

	public class AnalyzerResultBuilderTests
	{
		[Fact]
		public void AnalyzeSarifResultMinimalConfigBuildsAnalyzerResultCorrectly()
		{
			IAnalyzerResultBuilder analyzerResultBuilder = new AnalyzerResultBuilder()
			{
				Rules = new List<string>() { string.Empty }
			};

			SarifResult sarifResult = CreateSarifResult();

			AnalyzerResult analyzerResult = analyzerResultBuilder.AnalyzeSarifResult(sarifResult);

			Assert.True(analyzerResult.NumberOfInfos == 3);
			Assert.True(analyzerResult.NumberOfWarnings == 5);
			Assert.True(analyzerResult.NumberOfErrors == 7);
		}

		[Fact]
		public void AnalyzeSarifResultMissingRulePrefixesBuildsAnalyzerResultCorrectly()
		{
			IAnalyzerResultBuilder analyzerResultBuilder = new AnalyzerResultBuilder() { Rules = new List<string>() { "AA" } };

			SarifResult sarifResult = CreateSarifResult();

			AnalyzerResult analyzerResult = analyzerResultBuilder.AnalyzeSarifResult(sarifResult);

			Assert.True(analyzerResult.NumberOfInfos == 0);
			Assert.True(analyzerResult.NumberOfWarnings == 0);
			Assert.True(analyzerResult.NumberOfErrors == 0);
		}

		[Fact]
		public void AnalyzeSarifResultOneRulePrefixBuildsAnalyzerResultCorrectly()
		{
			IAnalyzerResultBuilder analyzerResultBuilder = new AnalyzerResultBuilder() { Rules = new List<string>() { "XY" } };

			SarifResult sarifResult = CreateSarifResult();

			AnalyzerResult analyzerResult = analyzerResultBuilder.AnalyzeSarifResult(sarifResult);

			Assert.True(analyzerResult.NumberOfInfos == 3);
			Assert.True(analyzerResult.NumberOfWarnings == 4);
			Assert.True(analyzerResult.NumberOfErrors == 4);
		}

		[Fact]
		public void AnalyzeSarifResultsMinimalConfigBuildsAnalyzerResultCorrectly()
		{
			IAnalyzerResultBuilder analyzerResultBuilder = new AnalyzerResultBuilder()
			{
				Rules = new List<string>() { string.Empty }
			};

			IEnumerable<SarifResult> sarifResults = new[] { CreateSarifResult(), CreateSarifResult() };

			AnalyzerResult analyzerResult = analyzerResultBuilder.AnalyzeSarifResults(sarifResults);

			Assert.True(analyzerResult.NumberOfInfos == 6);
			Assert.True(analyzerResult.NumberOfWarnings == 10);
			Assert.True(analyzerResult.NumberOfErrors == 14);
		}

		[Fact]
		public void AnalyzeSarifResultSuppressedResultBuildsAnalyzerResultCorrectly()
		{
			IAnalyzerResultBuilder analyzerResultBuilder = new AnalyzerResultBuilder()
			{
				Rules = new List<string>() { string.Empty }
			};

			SarifResult sarifResult = CreateSarifResultSuppressed();

			AnalyzerResult analyzerResult = analyzerResultBuilder.AnalyzeSarifResult(sarifResult);

			Assert.True(analyzerResult.NumberOfInfos == 0);
			Assert.True(analyzerResult.NumberOfWarnings == 0);
			Assert.True(analyzerResult.NumberOfErrors == 0);
		}

		[Fact]
		public void AnalyzeSarifResultTwoRulePrefixesBuildsAnalyzerResultCorrectly()
		{
			IAnalyzerResultBuilder analyzerResultBuilder = new AnalyzerResultBuilder()
			{
				Rules = new List<string>() { "XY", "XZ" }
			};

			SarifResult sarifResult = CreateSarifResult();

			AnalyzerResult analyzerResult = analyzerResultBuilder.AnalyzeSarifResult(sarifResult);

			Assert.True(analyzerResult.NumberOfInfos == 3);
			Assert.True(analyzerResult.NumberOfWarnings == 5);
			Assert.True(analyzerResult.NumberOfErrors == 6);
		}

		private static SarifResult CreateSarifResult()
		{
			SarifResult sarifResult = new SarifResult { RunLogs = new[] { new RunLogs() } };

			sarifResult.RunLogs.Single().Results =
				(new List<Result>()
				{
					new Result() { RuleId = "XY1001", Properties = new Properties() { Severity = Severity.Info } },
					new Result() { RuleId = "XY1001", Properties = new Properties() { Severity = Severity.Info } },
					new Result() { RuleId = "XY1002", Properties = new Properties() { Severity = Severity.Info } },
					new Result() { RuleId = "XY1001", Properties = new Properties() { Severity = Severity.Warning } },
					new Result() { RuleId = "XY1001", Properties = new Properties() { Severity = Severity.Warning } },
					new Result() { RuleId = "XY1002", Properties = new Properties() { Severity = Severity.Warning } },
					new Result() { RuleId = "XY1002", Properties = new Properties() { Severity = Severity.Warning } },
					new Result() { RuleId = "XZ1001", Properties = new Properties() { Severity = Severity.Warning } },
					new Result() { RuleId = "XY1001", Properties = new Properties() { Severity = Severity.Error } },
					new Result() { RuleId = "XY1001", Properties = new Properties() { Severity = Severity.Error } },
					new Result() { RuleId = "XY1002", Properties = new Properties() { Severity = Severity.Error } },
					new Result() { RuleId = "XY1002", Properties = new Properties() { Severity = Severity.Error } },
					new Result() { RuleId = "XZ1001", Properties = new Properties() { Severity = Severity.Error } },
					new Result() { RuleId = "XZ1002", Properties = new Properties() { Severity = Severity.Error } },
					new Result() { RuleId = "XX1002", Properties = new Properties() { Severity = Severity.Error } },
				}).ToArray();

			return sarifResult;
		}

		private static SarifResult CreateSarifResultSuppressed()
		{
			SarifResult sarifResult = CreateSarifResult();

			foreach (Result result in sarifResult.RunLogs.Single().Results)
			{
				result.IsSuppressedInSource = true;
			}

			return sarifResult;
		}
	}
}