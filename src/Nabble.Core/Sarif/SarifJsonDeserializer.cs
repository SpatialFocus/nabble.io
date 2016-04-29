// <copyright file="SarifResultJsonDeserializer.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Sarif
{
	using System.IO;
	using Nabble.Core.Common;

	/// <summary>
	/// </summary>
	public class SarifJsonDeserializer : ISarifJsonDeserializer
	{
		/// <summary>
		/// </summary>
		/// <param name="jsonDeserializer"></param>
		public SarifJsonDeserializer(IJsonDeserializer jsonDeserializer)
		{
			JsonDeserializer = jsonDeserializer;
		}

		/// <summary>
		/// </summary>
		public IJsonDeserializer JsonDeserializer { get; set; }

		/// <inheritdoc />
		public SarifResult DeserializeFromStream(Stream stream)
		{
			return JsonDeserializer.DeserializeFromStream<SarifResult>(stream);
		}
	}
}