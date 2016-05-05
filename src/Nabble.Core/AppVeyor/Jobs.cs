// <copyright file="Jobs.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.AppVeyor
{
	using System;

	/// <summary>
	/// A data class used in JSON serialization.
	/// </summary>
	public class Jobs
	{
		/// <summary>
		/// Gets or sets the Finished DateTime.
		/// </summary>
		public DateTime? Finished { get; set; }

		/// <summary>
		/// Gets or sets the JobId.
		/// </summary>
		public string JobId { get; set; }
	}
}