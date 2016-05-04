namespace Nabble.Core.Data.Entities
{
	using System;

	/// <summary>
	/// </summary>
	public class Badge
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Badge" /> class.
		/// </summary>
		public Badge()
		{
			Created = DateTime.UtcNow;
		}

		/// <summary>
		/// </summary>
		public string BadgeIdentifier { get; set; }

		/// <summary>
		/// </summary>
		public DateTime Created { get; set; }

		/// <summary>
		/// </summary>
		public int Id { get; set; }
	}
}