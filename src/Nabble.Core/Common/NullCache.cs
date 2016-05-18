// <copyright file="NullCache.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Common
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// Provides an Null Object Pattern implementation of <see cref="ICache" /> for caching.
	/// </summary>
	public class NullCache : ICache
	{
		/// <inheritdoc />
		public bool Contains(string key)
		{
			return false;
		}

		/// <inheritdoc />
		public void Remove(string key)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public T Retrieve<T>(string key)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public void Store(string key, object data, IDictionary<string, object> parameters)
		{
		}
	}
}