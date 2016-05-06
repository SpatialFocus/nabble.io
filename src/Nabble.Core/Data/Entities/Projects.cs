// <copyright file="Projects.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Data.Entities
{
	using System;

	/// <summary>
	/// </summary>
	public class Project
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Project"/> class.
		/// </summary>
		public Project()
		{
			DateTime = DateTime.UtcNow;
		}

		/// <summary>
		/// </summary>
		public string AccountName { get; set; }

		/// <summary>
		/// </summary>
		public DateTime DateTime { get; set; }

		/// <summary>
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// </summary>
		public string ProjectName { get; set; }
	}
}