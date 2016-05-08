// <copyright file="NabbleContext.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Data
{
	using System;
	using Microsoft.Data.Entity;
	using Microsoft.Data.Entity.Infrastructure;
	using Nabble.Core.Data.Entities;

	/// <summary>
	/// The Nabble DbContext implementation.
	/// </summary>
	public class NabbleContext : DbContext
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NabbleContext"/> class.
		/// </summary>
		public NabbleContext()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NabbleContext"/> class.
		/// </summary>
		/// <param name="options">The options for this context.</param>
		public NabbleContext(DbContextOptions<NabbleContext> options)
			: base(options)
		{
		}

		/// <summary>
		/// Gets or sets the DbSet for <see cref="Badge" />.
		/// </summary>
		public DbSet<Badge> Badges { get; set; }

		/// <summary>
		/// Gets or sets the DbSet for <see cref="Project" />.
		/// </summary>
		public DbSet<Project> Projects { get; set; }

		/// <summary>
		/// Gets or sets the DbSet for <see cref="Request" />.
		/// </summary>
		public DbSet<Request> Requests { get; set; }

		/// <inheritdoc />
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlite(@"fileName=NabbleCore.db");
			}
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