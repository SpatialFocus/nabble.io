// <copyright file="Build.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.AppVeyor
{
	using System.Collections.Generic;

	/// <summary>
	/// A data class used in JSON serialization.
	/// </summary>
	public class Build
	{
		/// <summary>
		/// Gets or sets the Jobs.</summary>
		public ICollection<Jobs> Jobs { get; set; }
	}
}