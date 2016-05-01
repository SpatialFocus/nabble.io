// <copyright file="Severity.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Sarif
{
	/// <summary>
	/// A data class used in JSON serialization.
	/// </summary>
	public enum Severity
	{
		/// <summary>
		/// Something suspicious but allowed.
		/// </summary>
		Warning,

		/// <summary>
		/// Something not allowed by the rules of the language or other authority.
		/// </summary>
		Error,

		/// <summary>
		/// Information that does not indicate a problem (i.e. not prescriptive).
		/// </summary>
		Info
	}
}