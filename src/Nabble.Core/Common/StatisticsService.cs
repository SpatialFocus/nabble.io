// <copyright file="StatisticsService.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Common
{
	using System.Threading.Tasks;
	using Microsoft.Data.Entity;
	using Nabble.Core.Data;
	using Nabble.Core.Data.Entities;

	/// <summary>
	/// </summary>
	public class StatisticsService : IStatisticsService
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="StatisticsService" /> class.
		/// </summary>
		/// <param name="context"></param>
		public StatisticsService(NabbleContext context)
		{
			Context = context;
		}

		/// <summary>
		/// </summary>
		public NabbleContext Context { get; set; }

		/// <inheritdoc />
		public async Task AddBadgeEntryIfNotExistsAsync(string badgeIdentifier)
		{
			if (!await Context.Badges.AnyAsync(x => x.BadgeIdentifier == badgeIdentifier))
			{
				Context.Badges.Add(new Badge() { BadgeIdentifier = badgeIdentifier });
				await Context.SaveChangesAsync();
			}
		}

		/// <inheritdoc />
		public async Task AddProjectEntryIfNotExistsAsync(string accountName, string projectName)
		{
			if (!await Context.Projects.AnyAsync(x => x.AccountName == accountName && x.ProjectName == projectName))
			{
				Context.Projects.Add(new Project() { AccountName = accountName, ProjectName = projectName });
				await Context.SaveChangesAsync();
			}
		}

		/// <inheritdoc />
		public async Task AddRequestEntryAsync()
		{
			Context.Requests.Add(new Request() { });
			await Context.SaveChangesAsync();
		}

		/// <inheritdoc />
		public async Task<int> GetTotalBadgeEntriesAsync()
		{
			return await Context.Badges.CountAsync();
		}

		/// <inheritdoc />
		public async Task<int> GetTotalProjectEntriesAsync()
		{
			return await Context.Projects.CountAsync();
		}

		/// <inheritdoc />
		public async Task<int> GetTotalRequestEntriesAsync()
		{
			return await Context.Requests.CountAsync();
		}
	}
}