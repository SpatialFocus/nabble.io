namespace Nabble.Core.Data
{
	using System.Linq;
	using System.Threading.Tasks;

	/// <summary>
	/// </summary>
	public interface IUnitOfWork
	{
		/// <summary>
		/// </summary>
		/// <param name="entity"></param>
		/// <typeparam name="T"></typeparam>
		void Add<T>(T entity) where T : class;

		/// <summary>
		/// </summary>
		/// <param name="entity"></param>
		/// <typeparam name="T"></typeparam>
		void Remove<T>(T entity) where T : class;

		/// <summary>
		/// </summary>
		void RevertChanges();

		/// <summary>
		/// </summary>
		void Save();

		/// <summary>
		/// </summary>
		Task SaveAsync();

		/// <summary>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		IQueryable<T> Set<T>() where T : class;
	}
}