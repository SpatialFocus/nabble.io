// <copyright file="NabbleContext.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Data
{
	using Microsoft.Data.Entity;
	using Nabble.Core.Data.Entities;

	/// <summary>
	/// </summary>
	public class NabbleContext : DbContext
	{
		/// <summary>
		/// </summary>
		public DbSet<Badge> Badges { get; set; }

		/// <summary>
		/// </summary>
		public DbSet<Project> Projects { get; set; }

		/// <summary>
		/// </summary>
		public DbSet<Request> Requests { get; set; }

		/// <inheritdoc />
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite(@"fileName=NabbleCore.db");
		}

		/// <inheritdoc />
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Project>().HasIndex(x => new { x.AccountName, x.ProjectName }).IsUnique();
			modelBuilder.Entity<Badge>().HasIndex(x => new { x.BadgeIdentifier }).IsUnique();
		}
	}
}