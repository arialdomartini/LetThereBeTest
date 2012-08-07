using System;

namespace LetThereBeTest
{
	public class DirectoryResource : IDisposable
	{
		private readonly IFileSystem _fileSystem;
		public string Path { get; private set; }

		public DirectoryResource(IFileSystem fileSystem, string path)
		{
			_fileSystem = fileSystem;
			Path = path;
		}

		public void Dispose()
		{
			_fileSystem.RecursivelyDeleteDirectory(Path);
		}
	}
}