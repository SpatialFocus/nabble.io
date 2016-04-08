// <copyright file="Class1.cs" company="Spatial Focus GmbH">
// Copyright (c) Spatial Focus GmbH. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Nabble.Core
{
	using System.IO;

	/// <summary>
	/// This class violates CA1001
	/// </summary>
	public class Class1
	{
		private FileStream newFile;

		/// <summary>
		/// Initializes a new instance of the <see cref="Class1" /> class.
		/// </summary>
		public Class1()
		{
			this.newFile = new FileStream(@"c:\temp.txt", FileMode.Open);
		}
	}
}