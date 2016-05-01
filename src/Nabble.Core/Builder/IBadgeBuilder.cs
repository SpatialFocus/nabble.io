// <copyright file="IBadgeBuilder.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Builder
{
	using System.Threading.Tasks;

	/// <summary>
	/// Represents an interface that provides a method to build Badges.
	/// </summary>
	public interface IBadgeBuilder
	{
		/// <summary>
		/// Builds a <see cref="Badge" /> with the given parameters as an asynchronous operation.
		/// </summary>
		/// <param name="badgeBuilderProperties">The <see cref="BadgeBuilderProperties" /> used to configurate the Badge building.</param>
		/// <param name="analyzerResultAccessor">
		/// The <see cref="IAnalyzerResultAccessor" /> used to access the
		/// <see cref="AnalyzerResult" />.
		/// </param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		Task<Badge> BuildBadgeAsync(BadgeBuilderProperties badgeBuilderProperties,
			IAnalyzerResultAccessor analyzerResultAccessor);
	}
}