// <copyright file="IAnalyzerResultAccessor.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core
{
	using System.Threading.Tasks;
	using Nabble.Core.Builder;

	/// <summary>
	/// Represents an interface that provides a method to access analyzer results.
	/// </summary>
	public interface IAnalyzerResultAccessor
	{
		/// <summary>
		/// Accesses the <see cref="AnalyzerResult" /> as an asynchronous operation.
		/// </summary>
		/// <returns>The task object representing the asynchronous operation.</returns>
		Task<AnalyzerResult> GetAnalyzerResultAsync();
	}
}