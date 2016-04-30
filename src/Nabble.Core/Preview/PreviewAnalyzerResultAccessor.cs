namespace Nabble.Core.Preview
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Nabble.Core.Builder;
	using Nabble.Core.Sarif;

	/// <summary>
	/// </summary>
	public class PreviewAnalyzerResultAccessor : IAnalyzerResultAccessor
	{
		public PreviewSettings PreviewSettings { get; set; }

		/// <summary>
		/// </summary>
		/// <returns></returns>
		public Task<AnalyzerResult> GetAnalyzerResultAsync()
		{
			AnalyzerResultBuilder analyzerResultBuilder = new AnalyzerResultBuilder { Rules = new[] { string.Empty } };

			SarifResult sarifResult = CreateSarifResult();

			return Task.Run(() => analyzerResultBuilder.AnalyzeSarifResult(sarifResult));
		}

		private SarifResult CreateSarifResult()
		{
			if (PreviewSettings.Inaccessible)
			{
				throw new ArgumentException("Simulate an exception in badge creation.");
			}

			SarifResult sarifResult = new SarifResult { RunLogs = new[] { new RunLogs() } };

			List<Result> results = new List<Result>();

			for (int i = 0; i < PreviewSettings.NumErrors; i++)
			{
				results.Add(new Result() { RuleId = "XX" + i, Properties = new Properties() { Severity = Severity.Error } });
			}

			for (int i = 0; i < PreviewSettings.NumWarnings; i++)
			{
				results.Add(new Result() { RuleId = "YY" + i, Properties = new Properties() { Severity = Severity.Warning } });
			}

			for (int i = 0; i < PreviewSettings.NumInfos; i++)
			{
				results.Add(new Result() { RuleId = "ZZ" + i, Properties = new Properties() { Severity = Severity.Info } });
			}

			sarifResult.RunLogs.Single().Results = results.ToArray();

			return sarifResult;
		}
	}
}