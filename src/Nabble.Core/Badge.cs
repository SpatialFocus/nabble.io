// <copyright file="Badge.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core
{
	using System.IO;

	/// <summary>
	/// A class representing a Badge.
	/// </summary>
	public class Badge
	{
		/// <summary>
		/// Gets or sets the Badge content as <see cref="Stream"/>.</summary>
		public Stream Stream { get; set; }

		/// <summary>
		/// Gets or sets the Badge content type.
		/// </summary>
		public string ContentType { get; set; }
	}
}