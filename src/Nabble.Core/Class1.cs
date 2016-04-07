namespace Nabble.Core
{
	using System.IO;

	// This violates CA1001
	public class Class1
	{
		FileStream newFile;

		public Class1()
		{
			this.newFile = new FileStream(@"c:\temp.txt", FileMode.Open);
		}
	}
}