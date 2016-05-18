// <copyright file="IJsonDeserializer.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Common
{
	using System.IO;

	/// <summary>
	/// Represents an interface that provides a method to deserialize streams into objects.
	/// </summary>
	public interface IJsonDeserializer
	{
		/// <summary>
		/// Deserializes a JSON structure contained by the specified <see cref="Stream" /> into an instance of the specified type
		/// <typeparamref name="T" />.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> containing the JSON structure</param>
		/// <typeparam name="T"> The type of the object to deserialize.</typeparam>
		/// <returns>
		/// The instance of <typeparamref name="T" /> being deserialized.
		/// </returns>
		T DeserializeFromStream<T>(Stream stream);
	}
}