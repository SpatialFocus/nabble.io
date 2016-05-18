// <copyright file="ISarifJsonDeserializer.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Sarif
{
	using System.IO;

	/// <summary>
	/// Represents an interface that provides a method to deserialize JSON streams into SARIF results.
	/// </summary>
	public interface ISarifJsonDeserializer
	{
		/// <summary>
		/// Deserializes a JSON structure contained by the specified <see cref="Stream" /> into an instance of
		/// <see cref="SarifResult" />.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> containing the JSON structure.</param>
		/// <returns>The instance of a <see cref="SarifResult" /> being deserialized.</returns>
		SarifResult DeserializeFromStream(Stream stream);
	}
}