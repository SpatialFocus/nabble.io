namespace Nabble.Core.Test
{
	using System.IO;
	using System.Reflection;

	public class ResourceHelper
	{
		public static Stream GetResourceStream(string resourceName)
		{
			return
				Assembly.GetExecutingAssembly()
					.GetManifestResourceStream(string.Format("{0}.data.{1}", typeof(ResourceHelper).Namespace, resourceName));
		}
	}
}