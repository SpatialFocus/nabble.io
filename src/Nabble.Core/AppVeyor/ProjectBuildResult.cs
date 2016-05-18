// <copyright file="ProjectBuildResult.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.AppVeyor
{
	/// <summary>
	/// A data class used in JSON serialization.
	/// </summary>
	public class ProjectBuildResult
	{
		/// <summary>
		/// Gets or sets the Build.</summary>
		public Build Build { get; set; }
	}
}