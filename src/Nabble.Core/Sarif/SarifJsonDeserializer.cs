// <copyright file="SarifJsonDeserializer.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Sarif
{
	using System.IO;
	using Nabble.Core.Common;

	/// <summary>
	/// Provides an implementation of <see cref="ISarifJsonDeserializer" /> to deserialize JSON streams into SARIF results.
	/// </summary>
	public class SarifJsonDeserializer : ISarifJsonDeserializer
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SarifJsonDeserializer" /> class.
		/// </summary>
		/// <param name="jsonDeserializer">The <see cref="IJsonDeserializer" /> used to deserialize JSON streams.</param>
		public SarifJsonDeserializer(IJsonDeserializer jsonDeserializer)
		{
			JsonDeserializer = jsonDeserializer;
		}

		/// <summary>
		/// Gets or sets the JsonDeserializer used to deserialize JSON streams.
		/// </summary>
		public IJsonDeserializer JsonDeserializer { get; set; }

		/// <inheritdoc />
		public SarifResult DeserializeFromStream(Stream stream)
		{
			return JsonDeserializer.DeserializeFromStream<SarifResult>(stream);
		}
	}
}