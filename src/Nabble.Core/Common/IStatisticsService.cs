// <copyright file="IStatisticsService.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Common
{
	using System;
	using System.Threading.Tasks;

	/// <summary>
	/// </summary>
	public interface IStatisticsService
	{
		/// <summary>
		/// </summary>
		/// <param name="badgeIdentifier"></param>
		Task AddBadgeEntryIfNotExistsAsync(string badgeIdentifier);

		/// <summary>
		/// </summary>
		/// <param name="accountName"></param>
		/// <param name="projectName"></param>
		Task AddProjectEntryIfNotExistsAsync(string accountName, string projectName);

		/// <summary>
		/// </summary>
		Task AddRequestEntryAsync();

		/// <summary>
		/// </summary>
		/// <returns></returns>
		Task<int> GetTotalBadgeEntriesAsync();

		/// <summary>
		/// </summary>
		/// <returns></returns>
		Task<int> GetTotalProjectEntriesAsync();

		/// <summary>
		/// </summary>
		/// <returns></returns>
		Task<int> GetTotalRequestEntriesAsync();
	}
}