// <copyright file="BadgeClientProperties.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Builder
{
	/// <summary>
	/// A class providing configuration for the <see cref="Badge" /> creation of <see cref="IBadgeClient" /> instances.
	/// </summary>
	public class BadgeClientProperties
	{
		/// <summary>
		/// Gets or sets the Badge Color.
		/// </summary>
		public BadgeColor Color { get; set; }

		/// <summary>
		/// Gets or sets the Badge Format (svg, png, jpg, gif, json).
		/// </summary>
		public string Format { get; set; }

		/// <summary>
		/// Gets or sets the Badge Label.
		/// </summary>
		public string Label { get; set; }

		/// <summary>
		/// Gets or sets the Badge Status.
		/// </summary>
		public string Status { get; set; }

		/// <summary>
		/// Gets or sets the Badge Style.
		/// </summary>
		public BadgeStyle Style { get; set; }
	}
}