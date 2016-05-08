// <copyright file="SarifResultNotFoundException.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Exceptions
{
	using System;

	/// <summary>
	/// The exception that is thrown when the specified SARIF result was not found.
	/// </summary>
	public class SarifResultNotFoundException : ServiceException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SarifResultNotFoundException" /> class.
		/// </summary>
		public SarifResultNotFoundException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SarifResultNotFoundException" /> class.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		public SarifResultNotFoundException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SarifResultNotFoundException" /> class.
		/// </summary>
		/// ///
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">
		/// The exception that is the cause of the current exception, or a null reference (Nothing in
		/// Visual Basic) if no inner exception is specified.
		/// </param>
		public SarifResultNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}