// <copyright file="NullDisposable.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Common
{
	using System;

	/// <summary>
	/// Provides an Null Object Pattern implementation of <see cref="IDisposable" />.
	/// </summary>
	public class NullDisposable : IDisposable
	{
		/// <inheritdoc />
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <param name="disposing">
		/// Indicates whether the method was invoked from the IDisposable.Dispose implementation or from
		/// the finalizer.
		/// </param>
		protected virtual void Dispose(bool disposing)
		{
		}
	}
}