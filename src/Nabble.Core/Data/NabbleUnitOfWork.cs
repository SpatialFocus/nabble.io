// <copyright file="NabbleUnitOfWork.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Data
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;

	/// <summary>
	/// Provides an implementation of <see cref="IUnitOfWork" />. Wraps an underlying <see cref="NabbleContext" /> instance,
	/// which performs the actual operations.
	/// </summary>
	public class NabbleUnitOfWork : IUnitOfWork, IDisposable
	{
		private NabbleContext nabbleContext;

		/// <summary>
		/// Gets the NabbleContext
		/// </summary>
		public NabbleContext NabbleContext
		{
			get
			{
				if (this.nabbleContext == null)
				{
					this.nabbleContext = CreationDelegate();
				}

				return this.nabbleContext;
			}
		}

		/// <summary>
		/// Gets or sets the CreationDelegate used to access a <see cref="NabbleContext" /> instance.
		/// </summary>
		public Func<NabbleContext> CreationDelegate { get; set; } = () => new NabbleContext();

		/// <inheritdoc />
		public void Add<T>(T entity) where T : class
		{
			NabbleContext.Set<T>().Add(entity);
		}

		/// <inheritdoc />
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <inheritdoc />
		public IQueryable<T> GetSet<T>() where T : class
		{
			return NabbleContext.Set<T>();
		}

		/// <inheritdoc/>
		public void Remove<T>(T entity) where T : class
		{
			NabbleContext.Set<T>().Remove(entity);
		}

		/// <inheritdoc />
		public void RevertChanges()
		{
			Dispose();
		}

		/// <inheritdoc />
		public async Task SaveAsync()
		{
			await NabbleContext.SaveChangesAsync();
		}

		/// <summary>
		/// Disposes the underlying <see cref="NabbleContext" /> instance.
		/// </summary>
		/// <param name="disposing">
		/// Indicates whether the method was invoked from the IDisposable.Dispose implementation or from
		/// the finalizer.
		/// </param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.nabbleContext != null)
			{
				this.nabbleContext.Dispose();
				this.nabbleContext = null;
			}
		}
	}
}