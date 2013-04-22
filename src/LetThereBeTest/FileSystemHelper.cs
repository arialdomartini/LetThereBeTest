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

		public static DisposableDirectory RandomDirectory()
		{
			string path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("n")); ;
			Directory.CreateDirectory(path);
			return new DisposableDirectory(_fileSystem, path);
		}

		public static DisposableFile RandomTemporaryFile()
		{
			return new DisposableFile(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("n")));
		}


		public class DisposableDirectory : IDisposable
		{
			private readonly IFileSystem _fileSystem;
			public string Path { get; private set; }

			public DisposableDirectory(IFileSystem fileSystem, string path)
			{
				_fileSystem = fileSystem;
				Path = path;
			}

			public void Dispose()
			{
				_fileSystem.RecursivelyDeleteDirectory(Path);
			}
		}

		public class DisposableFile : IDisposable
		{
			private readonly string _filename;

			public DisposableFile(string filename)
			{
				_filename = filename;
			}

			public string Path
			{
				get { return _filename; }
			}

			public void Dispose()
			{
				File.Delete(_filename);
			}
		}



	}
}