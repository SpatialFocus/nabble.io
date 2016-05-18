// <copyright file="StaticAnalyzerResultAccessor.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Builder
{
	using System.Threading.Tasks;

	/// <summary>
	/// Provides an implementation of <see cref="IAnalyzerResultAccessor" /> to access static analyzer results.
	/// </summary>
	public class StaticAnalyzerResultAccessor : IAnalyzerResultAccessor
	{
		/// <summary>
		/// Gets or sets the AnalyzerResult returned by GetAnalyzerResultAsync.
		/// </summary>
		public AnalyzerResult AnalyzerResult { get; set; }

		/// <inheritdoc />
		public Task<AnalyzerResult> GetAnalyzerResultAsync()
		{
			return Task.FromResult(AnalyzerResult);
		}
	}
}