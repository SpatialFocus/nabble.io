// <copyright file="NabbleContextUnitOfWork.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Data
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;

	/// <summary>
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

		public Func<NabbleContext> CreationDelegate { get; set; } = () =>
		{
			NabbleContext nabbleContext = new NabbleContext();
			nabbleContext.Database.EnsureCreated();

			return nabbleContext;
		};

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
		public void Save()
		{
			NabbleContext.SaveChanges();
		}

		/// <inheritdoc />
		public async Task SaveAsync()
		{
			await NabbleContext.SaveChangesAsync();
		}

		/// <inheritdoc />
		public IQueryable<T> Set<T>() where T : class
		{
			return NabbleContext.Set<T>();
		}

		/// <summary>
		/// </summary>
		/// <param name="disposing"></param>
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