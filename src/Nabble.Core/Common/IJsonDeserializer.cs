// <copyright file="IJsonDeserializer.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Common
{
	using System.IO;

	/// <summary>
	/// </summary>
	public interface IJsonDeserializer
	{
		/// <summary>
		/// </summary>
		/// <param name="stream"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		T DeserializeFromStream<T>(Stream stream);
	}
}