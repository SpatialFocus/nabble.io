// <copyright file="IUnitOfWork.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core.Data
{
	using System.Linq;
	using System.Threading.Tasks;

	/// <summary>
	/// Represents an interface for the UnitOfWork Pattern.
	/// </summary>
	public interface IUnitOfWork
	{
		/// <summary>
		/// Adds an entity to the corresponding collection.
		/// </summary>
		/// <param name="entity">The entity to add.</param>
		/// <typeparam name="T">The type of the entity to add.</typeparam>
		void Add<T>(T entity) where T : class;

		/// <summary>
		/// Removes an entity from the corresponding collection.
		/// </summary>
		/// <param name="entity">The entity to remove.</param>
		/// <typeparam name="T">The type of the entity to remove.</typeparam>
		void Remove<T>(T entity) where T : class;

		/// <summary>
		/// Reverts all previously performed changes.
		/// </summary>
		void RevertChanges();

		/// <summary>
		/// Saves all previously made changes.
		/// </summary>
		/// <returns>
		/// The task object representing the asynchronous operation.
		/// </returns>
		Task SaveAsync();

		/// <summary>
		/// Returns the corresponding entity collection.
		/// </summary>
		/// <typeparam name="T">The type of the entity of the collection.</typeparam>
		/// <returns>An IQueryable of <typeparamref name="T" />.</returns>
		IQueryable<T> GetSet<T>() where T : class;
	}
}