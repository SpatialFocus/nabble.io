﻿// <copyright file="ServiceException.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Exceptions
{
	using System;

	/// <summary>
	/// The base exception class used for exceptions occuring in Nabble.Core.
	/// </summary>
	public class ServiceException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceException" /> class.
		/// </summary>
		public ServiceException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceException" /> class.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		public ServiceException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceException" /> class.
		/// </summary>
		/// ///
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">
		/// The exception that is the cause of the current exception, or a null reference (Nothing in
		/// Visual Basic) if no inner exception is specified.
		/// </param>
		public ServiceException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}