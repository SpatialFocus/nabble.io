// <copyright file="Result.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Sarif
{
	/// <summary>
	/// A data class used in JSON serialization.
	/// </summary>
	public class Result
	{
		/// <summary>
		/// Gets or sets the Properties.</summary>
		public Properties Properties { get; set; }

		/// <summary>
		/// Gets or sets the RuleId.</summary>
		public string RuleId { get; set; }
	}
}