// <copyright file="IAnalyzerResultBuilder.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Builder
{
	using System.Collections.Generic;
	using Nabble.Core.Sarif;

	/// <summary>
	/// </summary>
	public interface IAnalyzerResultBuilder
	{
		/// <summary>
		/// </summary>
		/// <param name="sarifResult"></param>
		/// <returns></returns>
		AnalyzerResult AnalyzeSarifResult(SarifResult sarifResult);


		/// <summary>
		/// </summary>
		/// <param name="sarifResults"></param>
		/// <returns></returns>
		AnalyzerResult AnalyzeSarifResults(IEnumerable<SarifResult> sarifResults);
	}
}