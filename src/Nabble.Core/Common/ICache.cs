// <copyright file="ICache.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Common
{
	using System.Collections.Generic;

	/// <summary>
	/// Represents an interface that provides methods for caching.
	/// </summary>
	public interface ICache
	{
		/// <summary>
		/// Checks whether the cache item already exists in the cache.
		/// </summary>
		/// <param name="key">A unique identifier for the cache item.</param>
		/// <returns>true if the cache contains a cache item with the same key value as <paramref name="key" />; otherwise, false.</returns>
		bool Contains(string key);

		/// <summary>
		/// Removes the cache item for the given key from the cache.
		/// </summary>
		/// <param name="key">A unique identifier for the cache item.</param>
		void Remove(string key);

		/// <summary>
		/// Gets the specified cache item from the cache as an instance of type <typeparamref name="T" />.
		/// </summary>
		/// <param name="key">A unique identifier for the cache item.</param>
		/// <typeparam name="T">The type of the object to retrieve.</typeparam>
		/// <returns>The cache item that is identified by <paramref name="key"/>.</returns>
		T Retrieve<T>(string key);

		/// <summary>
		/// Stores the specified cache item for the given key.
		/// </summary>
		/// <param name="key">A unique identifier for the cache item.</param>
		/// <param name="data">The cache item which will be cached.</param>
		/// <param name="parameters">Additional (optional) parameters used for fine-grained cache control.</param>
		void Store(string key, object data, IDictionary<string, object> parameters);
	}
}