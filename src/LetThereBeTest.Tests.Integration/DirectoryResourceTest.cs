using System;
using System.IO;
using NUnit.Framework;
using SharpTestsEx;

namespace LetThereBeTest.Tests.Integration
{
	[TestFixture]
	public class DirectoryResourceTest
	{
		[Test]
		public void RecursivelyDeleteDirectory_EmptyDirectory_ShouldDeleteIt()
		{
			var tmpDirectoryPath = ATemporaryDirectory();
			var directoryResource = new DirectoryResource(tmpDirectoryPath);

			directoryResource.RecursivelyDeleteDirectory();

			Directory.Exists(tmpDirectoryPath).Should().Be.False();
		}


		[Test]
		public void RecursivelyDeleteDirectory_DirectoryWithOneFile_ShouldDeleteIt()
		{
			var tmpDirectoryPath = ATemporaryDirectory();
			var fileName = ATextFileIn(tmpDirectoryPath);
			var directoryResource = new DirectoryResource(tmpDirectoryPath);

			directoryResource.RecursivelyDeleteDirectory();

			Directory.Exists(tmpDirectoryPath).Should().Be.False();
			File.Exists(fileName).Should().Be.False();
		}


		[Test]
		public void RecursivelyDeleteDirectory_DirectoryWithNestedDirectoryAndOneFile_ShouldDeleteAll()
		{
			var tmpDirectoryPath = ATemporaryDirectory();
			var subDirPath = CreateSubDirectoryIn(tmpDirectoryPath);
			var fileName = ATextFileIn(subDirPath);
			var directoryResource = new DirectoryResource(tmpDirectoryPath);

			directoryResource.RecursivelyDeleteDirectory();

			Directory.Exists(tmpDirectoryPath).Should().Be.False();
			File.Exists(fileName).Should().Be.False();
		}

		private string ATemporaryDirectory()
		{
			var tempPath = Path.GetTempPath();
			var randomFileName = Path.GetRandomFileName();
			var tmpDirectoryFullPath = Path.Combine(tempPath, randomFileName);
			Directory.CreateDirectory(tmpDirectoryFullPath);
			return tmpDirectoryFullPath;
		}

		private static string CreateSubDirectoryIn(string tmpDirectoryFullPath)
		{
			var subDir = Path.Combine(tmpDirectoryFullPath, Guid.NewGuid().ToString("n"));
			Directory.CreateDirectory(subDir);
			return subDir;
		}

		private static string ATextFileIn(string tmpDirectoryPath)
		{
			var fileName = Path.Combine(tmpDirectoryPath, Guid.NewGuid().ToString("n"));
			File.AppendAllText(fileName, "foobar");
			return fileName;
		}
	}
}