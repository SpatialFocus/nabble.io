// <copyright file="IAnalyzerResultBuilder.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Builder
{
	using System.Collections.Generic;
	using Nabble.Core.Sarif;

	/// <summary>
	/// Represents an interface that provides methods to analyze SARIF results.
	/// </summary>
	public interface IAnalyzerResultBuilder
	{
		/// <summary>
		/// Analyzes a <see cref="SarifResult" /> and returns an <see cref="AnalyzerResult" />.
		/// </summary>
		/// <param name="sarifResult">The SARIF result being analyzed.</param>
		/// <returns>The analyzed result.</returns>
		AnalyzerResult AnalyzeSarifResult(SarifResult sarifResult);

		/// <summary>
		/// Analyzes an <see cref="IEnumerable{SarifResult}">IEnumerable&lt;SarifResult&gt;</see> and returns an
		/// <see cref="AnalyzerResult" />.
		/// </summary>
		/// <param name="sarifResults">The SARIF results being analyzed.</param>
		/// <returns>The analyzed result.</returns>
		AnalyzerResult AnalyzeSarifResults(IEnumerable<SarifResult> sarifResults);
	}
}