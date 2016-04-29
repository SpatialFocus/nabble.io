// <copyright file="IBadgeBuilder.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Builder
{
	using System.Threading.Tasks;

	/// <summary>
	/// </summary>
	public interface IBadgeBuilder
	{
		/// <summary>
		/// </summary>
		/// <param name="badgeBuilderProperties"></param>
		/// <param name="analyzerResultAccessor"></param>
		/// <returns></returns>
		Task<Badge> BuildBadgeAsync(BadgeBuilderProperties badgeBuilderProperties,
			IAnalyzerResultAccessor analyzerResultAccessor);
	}
}