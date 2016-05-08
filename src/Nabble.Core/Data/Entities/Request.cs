// <copyright file="Request.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Data.Entities
{
	using System;

	/// <summary>
	/// An entity used to store request statistics.
	/// </summary>
	public class Request
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Request" /> class.
		/// </summary>
		public Request()
		{
			DateTime = DateTime.UtcNow;
		}

		/// <summary>
		/// Gets or sets the Created date time.
		/// </summary>
		public DateTime DateTime { get; set; }

		/// <summary>
		/// Gets or sets the Id.
		/// </summary>
		public int Id { get; set; }
	}
}