// <copyright file="IBadgeClient.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Builder
{
	using System.Threading.Tasks;

	/// <summary>
	/// </summary>
	public interface IBadgeClient
	{
		/// <summary>
		/// </summary>
		/// <param name="badgeClientProperties"></param>
		/// <param name="analyzerResult"></param>
		/// <returns></returns>
		Task<Badge> RequestBadgeAsync(BadgeClientProperties badgeClientProperties);
	}
}