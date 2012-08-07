using System;
using System.IO;

namespace LetThereBeTest
{
	public static class FileSystemHelper
	{
		private static FileSystem _fileSystem;

		static FileSystemHelper()
		{
			_fileSystem = new FileSystem();
		}

		public static DirectoryResource RandomDirectory()
		{
			string path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("n"));;
			Directory.CreateDirectory(path);
			return new DirectoryResource(_fileSystem, path);
		}
	}
}