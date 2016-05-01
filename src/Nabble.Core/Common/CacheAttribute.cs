// <copyright file="CacheAttribute.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Common
{
	using System;

	/// <summary>
	/// An attribute to mark methods and properties for caching.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property, Inherited = false)]
	public sealed class CacheAttribute : Attribute
	{
		/// <summary>
		/// Gets or sets the Duration in Seconds until the cache item should be invalidated.
		/// </summary>
		public int Duration { get; set; }
	}
}