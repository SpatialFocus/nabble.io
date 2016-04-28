// <copyright file="AnalyzerResultBuilder.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Builder
{
	using System.Collections.Generic;
	using System.Linq;
	using Nabble.Core.Sarif;

	/// <summary>
	/// </summary>
	public class AnalyzerResultBuilder : IAnalyzerResultBuilder
	{
		/// <summary>
		/// </summary>
		public ICollection<string> Rules { get; set; }

		/// <inheritdoc />
		public AnalyzerResult AnalyzeSarifResult(SarifResult sarifResult)
		{
			AnalyzerResult analyzerResult = new AnalyzerResult();

			foreach (Result result in sarifResult.RunLogs.Single().Results)
			{
				if (Rules.Any(rule => result.RuleId.StartsWith(rule)))
				{
					switch (result.Properties.Severity)
					{
						case Severity.Info:
							analyzerResult.NumberOfInfos++;
							break;

						case Severity.Warning:
							analyzerResult.NumberOfWarnings++;
							break;

						case Severity.Error:
							analyzerResult.NumberOfErrors++;
							break;
					}
				}
			}

			return analyzerResult;
		}

		/// <inheritdoc />
		public AnalyzerResult AnalyzeSarifResults(IEnumerable<SarifResult> sarifResults)
		{
			AnalyzerResult result = new AnalyzerResult();

			foreach (SarifResult sarifResult in sarifResults)
			{
				result.NumberOfInfos += AnalyzeSarifResult(sarifResult).NumberOfInfos;
				result.NumberOfWarnings += AnalyzeSarifResult(sarifResult).NumberOfWarnings;
				result.NumberOfErrors += AnalyzeSarifResult(sarifResult).NumberOfErrors;
			}

			return result;
		}
	}
}