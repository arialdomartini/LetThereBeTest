using System;
using System.IO;

namespace LetThereBeTest
{
	public class DirectoryResource
	{
		public DirectoryResource(string path)
		{
			_path = path;
		}

		private string _path;
		public string Path
		{
			get
			{
				return _path;
			}
		}

		public void RecursivelyDeleteDirectory()
		{
			RecursivelyDeleteDirectory(_path);
		}

		private void RecursivelyDeleteDirectory(string path)
		{
			string[] files = Directory.GetFiles(path);
			string[] dirs = Directory.GetDirectories(path);

			foreach (string file in files)
			{
				File.SetAttributes(file, FileAttributes.Normal);
				File.Delete(file);
			}

			foreach (string dir in dirs)
				RecursivelyDeleteDirectory(dir);

			Directory.Delete(path, false);
		}
	}
}