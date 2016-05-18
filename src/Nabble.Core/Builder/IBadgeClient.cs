// <copyright file="IBadgeClient.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Builder
{
	using System.Threading.Tasks;

	/// <summary>
	/// Represents an interface that provides a method to request Badges.
	/// </summary>
	public interface IBadgeClient
	{
		/// <summary>
		/// Requests a <see cref="Badge" /> with the given parameters as an asynchronous operation.
		/// </summary>
		/// <param name="badgeClientProperties">The <see cref="BadgeClientProperties" /> used to configurate the Badge request.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		Task<Badge> RequestBadgeAsync(BadgeClientProperties badgeClientProperties);
	}
}