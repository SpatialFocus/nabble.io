// <copyright file="NullStatisticsService.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Common
{
	using System;
	using System.Threading.Tasks;

	/// <summary>
	/// </summary>
	public class NullStatisticsService : IStatisticsService
	{
		public Task AddBadgeEntryIfNotExistsAsync(string badgeIdentifier)
		{
			return Task.FromResult(true);
		}

		public Task AddProjectEntryIfNotExistsAsync(string accountName, string projectName)
		{
			return Task.FromResult(true);
		}

		public Task AddRequestEntryAsync()
		{
			return Task.FromResult(true);
		}

		public Task<int> GetTotalBadgeEntriesAsync()
		{
			throw new NotImplementedException();
		}

		public Task<int> GetTotalProjectEntriesAsync()
		{
			throw new NotImplementedException();
		}

		public Task<int> GetTotalRequestEntriesAsync()
		{
			throw new NotImplementedException();
		}
	}
}