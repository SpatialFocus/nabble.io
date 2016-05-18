// <copyright file="RunLogs.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Sarif
{
	using System.Collections.Generic;

	/// <summary>
	/// A data class used in JSON serialization.
	/// </summary>
	public class RunLogs
	{
		/// <summary>
		/// Gets or sets the Results.
		/// </summary>
		public ICollection<Result> Results { get; set; }
	}
}