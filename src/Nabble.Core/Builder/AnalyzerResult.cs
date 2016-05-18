// <copyright file="AnalyzerResult.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Builder
{
	/// <summary>
	/// A class containing the number of results within a SARIF log, grouped by their severity.
	/// </summary>
	public class AnalyzerResult
	{
		/// <summary>
		/// Gets or sets the NumberOfErrors.
		/// </summary>
		public int NumberOfErrors { get; set; }

		/// <summary>
		/// Gets or sets the NumberOfInfos.
		/// </summary>
		public int NumberOfInfos { get; set; }

		/// <summary>
		/// Gets or sets the NumberOfWarnings.
		/// </summary>
		public int NumberOfWarnings { get; set; }
	}
}