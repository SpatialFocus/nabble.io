// <copyright file="Project.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Data.Entities
{
	using System;

	/// <summary>
	/// An entity used to store project statistics.
	/// </summary>
	public class Project
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Project" /> class.
		/// </summary>
		public Project()
		{
			DateTime = DateTime.UtcNow;
		}

		/// <summary>
		/// Gets or sets the AccountName used to distinguish unique projects.
		/// </summary>
		public string AccountName { get; set; }

		/// <summary>
		/// Gets or sets the Created date time.
		/// </summary>
		public DateTime DateTime { get; set; }

		/// <summary>
		/// Gets or sets the Id.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the ProjectName used to distinguish unique projects.
		/// </summary>
		public string ProjectName { get; set; }
	}
}