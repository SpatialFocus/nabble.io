// <copyright file="Badge.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Data.Entities
{
	using System;

	/// <summary>
	/// An entity used to store badge statistics.
	/// </summary>
	public class Badge
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Badge" /> class.
		/// </summary>
		public Badge()
		{
			Created = DateTime.UtcNow;
		}

		/// <summary>
		/// Gets or sets the BadgeIdentifier used to distinguish unique badges.
		/// </summary>
		public string BadgeIdentifier { get; set; }

		/// <summary>
		/// Gets or sets the Created date time.
		/// </summary>
		public DateTime Created { get; set; }

		/// <summary>
		/// Gets or sets the Id.
		/// </summary>
		public int Id { get; set; }
	}
}