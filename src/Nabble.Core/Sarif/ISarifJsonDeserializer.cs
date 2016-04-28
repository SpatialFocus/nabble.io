// <copyright file="ISarifResultJsonDeserializer.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Sarif
{
	using System.IO;

	/// <summary>
	/// </summary>
	public interface ISarifJsonDeserializer
	{
		/// <summary>
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		SarifResult DeserializeFromStream(Stream stream);
	}
}