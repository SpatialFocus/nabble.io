// <copyright file="ObjectCacheAdapter.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Common
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using System.Runtime.Caching;
	using System.Threading.Tasks;

	/// <summary>
	/// Provides an implementation of <see cref="ICache" /> for caching.
	/// Wraps an underlying <see cref="ObjectCache" /> instance, which performs the actual cache operations.
	/// </summary>
	public class ObjectCacheAdapter : ICache
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ObjectCacheAdapter" /> class.
		/// </summary>
		/// <param name="objectCache">The <see cref="ObjectCache" /> beeing wrapped.</param>
		public ObjectCacheAdapter(ObjectCache objectCache)
		{
			ObjectCache = objectCache;
		}

		/// <summary>
		/// Gets or sets the ObjectCache beeing wrapped.
		/// </summary>
		public ObjectCache ObjectCache { get; set; }

		/// <inheritdoc />
		public bool Contains(string key)
		{
			return ObjectCache.Contains(key);
		}

		/// <inheritdoc />
		public void Remove(string key)
		{
			ObjectCache.Remove(key);
		}

		/// <inheritdoc />
		public T Retrieve<T>(string key)
		{
			// When Retrieve is called for Tasks (e.g. async methods), wrap the actual stored Task result as Task again
			if (typeof(Task).IsAssignableFrom(typeof(T)))
			{
				MethodInfo makeGenericMethod =
					typeof(Task).GetMethod("FromResult").MakeGenericMethod(typeof(T).GetGenericArguments().Single());

				return (T)makeGenericMethod.Invoke(null, new[] { ObjectCache.GetCacheItem(key).Value });
			}

			return (T)ObjectCache.GetCacheItem(key).Value;
		}

		/// <inheritdoc />
		public void Store(string key, object data, IDictionary<string, object> parameters)
		{
			CacheItemPolicy cacheItemPolicy = null;

			// Create a CacheItemPolicy instance if Duration is set
			if (parameters.ContainsKey("Duration"))
			{
				cacheItemPolicy = new CacheItemPolicy()
				{
					AbsoluteExpiration = DateTimeOffset.Now.AddSeconds((int)parameters["Duration"])
				};
			}

			Task task = data as Task;

			if (task != null)
			{
				if (typeof(Task).IsAssignableFrom(task.GetType().GetGenericArguments().Single()))
				{
					throw new InvalidOperationException("Can't store Task<Task> instances.");
				}

				// When Store is called for Tasks (e.g. async methods), defer the storage until the Task is finished and store the actual Task result
				task.ContinueWith(
					x =>
					{
						ObjectCache.Add(key, ((dynamic)x).Result, cacheItemPolicy);
					},
					TaskContinuationOptions.OnlyOnRanToCompletion);
			}
			else
			{
				ObjectCache.Add(key, data, cacheItemPolicy);
			}
		}
	}
}