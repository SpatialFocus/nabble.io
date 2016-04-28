// <copyright file="JsonDeserializer.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Common
{
	using System.IO;
	using Newtonsoft.Json;

	/// <summary>
	/// </summary>
	public class JsonDeserializer : IJsonDeserializer
	{
		/// <inheritdoc/>
		public T DeserializeFromStream<T>(Stream stream)
		{
			JsonSerializer serializer = new JsonSerializer();

			using (StreamReader streamReader = new StreamReader(stream))
			{
				using (JsonReader jsonReader = new JsonTextReader(streamReader))
				{
					return serializer.Deserialize<T>(jsonReader);
				}
			}
		}
	}
}