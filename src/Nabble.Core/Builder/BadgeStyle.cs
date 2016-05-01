// <copyright file="BadgeStyle.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Builder
{
	/// <summary>
	/// An enum representing possible styles of a Badge.
	/// Corresponds to the <see href="http://shields.io/">shields.io</see> style scheme.
	/// </summary>
	public enum BadgeStyle
	{
		/// <summary>
		/// A style representing the default style.
		/// </summary>
		Default,

		/// <summary>
		/// A style representing a plastic style.
		/// </summary>
		Plastic,

		/// <summary>
		/// A style representing a flat style.
		/// </summary>
		Flat,

		/// <summary>
		/// A style representing a flat square style.
		/// </summary>
		FlatSquare,

		/// <summary>
		/// A style representing a social style.
		/// </summary>
		Social
	}
}