// <copyright file="IStatisticsService.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Common
{
	using System;
	using System.Threading.Tasks;

	/// <summary>
	/// Represents an interface that provides methods to modify and get certain badge statistics.
	/// </summary>
	public interface IStatisticsService
	{
		/// <summary>
		/// Adds an entry to the badge statistics, if none exists.
		/// </summary>
		/// <param name="badgeIdentifier">The badge identifier used to distinguish unique badges.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		Task AddBadgeEntryIfNotExistsAsync(string badgeIdentifier);

		/// <summary>
		/// Adds an entry to the project statistics, if none exists.
		/// </summary>
		/// <param name="accountName">The account name used to distinguish unique projects.</param>
		/// <param name="projectName">The project name used to distinguish unique projects.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		Task AddProjectEntryIfNotExistsAsync(string accountName, string projectName);

		/// <summary>
		/// Adds an entry to the request statistics.
		/// </summary>
		/// <returns>The task object representing the asynchronous operation.</returns>
		Task AddRequestEntryAsync();

		/// <summary>
		/// Creates a new transaction scope.
		/// </summary>
		/// <returns>The task object representing the asynchronous operation.</returns>
		Task<IDisposable> BeginTransactionAsync();

		/// <summary>
		/// Gets the total number of badge entries.
		/// </summary>
		/// <returns>
		/// The task object representing the asynchronous operation.
		/// The task result contains the number of total badge entries.
		/// </returns>
		Task<int> GetTotalBadgeEntriesAsync();

		/// <summary>
		/// Gets the total number of project entries.
		/// </summary>
		/// <returns>
		/// The task object representing the asynchronous operation.
		/// The task result contains the number of total project entries.
		/// </returns>
		Task<int> GetTotalProjectEntriesAsync();

		/// <summary>
		/// Gets the total number of request entries.
		/// </summary>
		/// <returns>
		/// The task object representing the asynchronous operation.
		/// The task result contains the number of total request entries.
		/// </returns>
		Task<int> GetTotalRequestEntriesAsync();
	}
}