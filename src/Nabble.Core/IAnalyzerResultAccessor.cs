// <copyright file="IAnalyzerResultAccessor.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core
{
	using System.Threading.Tasks;
	using Nabble.Core.Builder;

	/// <summary>
	/// </summary>
	public interface IAnalyzerResultAccessor
	{
		/// <summary>
		/// </summary>
		/// <returns></returns>
		Task<AnalyzerResult> GetAnalyzerResultAsync();
	}
}