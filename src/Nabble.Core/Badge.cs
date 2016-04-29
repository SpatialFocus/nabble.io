// <copyright file="Badge.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core
{
	using System.IO;

	/// <summary>
	/// </summary>
	public class Badge
	{
		/// <summary>
		/// </summary>
		public Stream Stream { get; set; }

		/// <summary>
		/// </summary>
		public string ContentType { get; set; }
	}
}