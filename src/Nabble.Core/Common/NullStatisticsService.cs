// <copyright file="NullStatisticsService.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Common
{
	using System;
	using System.Threading.Tasks;
	using Microsoft.Data.Entity.Storage;

	/// <summary>
	/// Provides an Null Object Pattern implementation of <see cref="IStatisticsService" /> to modify and get certain badge statistics.
	/// </summary>
	public class NullStatisticsService : IStatisticsService
	{
		/// <inheritdoc/>
		public Task AddBadgeEntryIfNotExistsAsync(string badgeIdentifier)
		{
			return Task.FromResult(true);
		}

		/// <inheritdoc/>
		public Task AddProjectEntryIfNotExistsAsync(string accountName, string projectName)
		{
			return Task.FromResult(true);
		}

		/// <inheritdoc/>
		public Task AddRequestEntryAsync()
		{
			return Task.FromResult(true);
		}

		/// <inheritdoc/>
		public Task<IRelationalTransaction> BeginTransactionAsync()
		{
			return Task.FromResult<IRelationalTransaction>(new NullRelationalTransaction());
		}

		/// <inheritdoc/>
		public Task<int> GetTotalBadgeEntriesAsync()
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public Task<int> GetTotalProjectEntriesAsync()
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public Task<int> GetTotalRequestEntriesAsync()
		{
			throw new NotImplementedException();
		}
	}
}