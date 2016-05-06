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
		public StatisticsService(IUnitOfWork unitOfWork)
		{
			UnitOfWork = unitOfWork;
		}

		/// <summary>
		/// </summary>
		public IUnitOfWork UnitOfWork { get; set; }

		/// <inheritdoc />
		public async Task AddBadgeEntryIfNotExistsAsync(string badgeIdentifier)
		{
			if (!await UnitOfWork.Set<Badge>().AnyAsync(x => x.BadgeIdentifier == badgeIdentifier))
			{
				UnitOfWork.Add(new Badge() { BadgeIdentifier = badgeIdentifier });
				await UnitOfWork.SaveAsync();
			}
		}

		/// <inheritdoc />
		public async Task AddProjectEntryIfNotExistsAsync(string accountName, string projectName)
		{
			if (!await UnitOfWork.Set<Project>().AnyAsync(x => x.AccountName == accountName && x.ProjectName == projectName))
			{
				UnitOfWork.Add(new Project() { AccountName = accountName, ProjectName = projectName });
				await UnitOfWork.SaveAsync();
			}
		}

		/// <inheritdoc />
		public async Task AddRequestEntryAsync()
		{
			UnitOfWork.Add(new Request() { });
			await UnitOfWork.SaveAsync();
		}

		/// <inheritdoc />
		public async Task<int> GetTotalBadgeEntriesAsync()
		{
			return await UnitOfWork.Set<Badge>().CountAsync();
		}

		/// <inheritdoc />
		public async Task<int> GetTotalProjectEntriesAsync()
		{
			return await UnitOfWork.Set<Project>().CountAsync();
		}

		/// <inheritdoc />
		public async Task<int> GetTotalRequestEntriesAsync()
		{
			return await UnitOfWork.Set<Request>().CountAsync();
		}
	}
}