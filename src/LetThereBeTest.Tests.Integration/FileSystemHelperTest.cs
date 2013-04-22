using System;
using System.IO;
using NUnit.Framework;
using SharpTestsEx;

namespace LetThereBeTest.Tests.Integration
{
	[TestFixture]
	public class FileSystemHelperTest
	{
		[Test]
		public void RandomDirectory_ShouldBeAbleToCreateADirectory()
		{
			using (var directoryResource = FileSystemHelper.RandomDirectory())
			{
				Directory.Exists(directoryResource.Path).Should().Be.True();
			}
		}

		[Test]
		public void RandomDirectory_ShouldBeAbleDeleteAllDirectoryContentWhenDisposing()
		{
			string filePath;
			using (var directoryResource = FileSystemHelper.RandomDirectory())
			{
				filePath = Path.Combine(directoryResource.Path, Guid.NewGuid().ToString("n"));
				File.WriteAllText(filePath, "foobar");
			}

			File.Exists(filePath).Should().Be.False();
		}


		[Test]
		public void RandomTemporaryFile_should_create_a_disposable_file()
		{
			string filePath;
			using (var randomTemporaryFile = FileSystemHelper.RandomTemporaryFile())
			{
				filePath = randomTemporaryFile.Path;
				File.WriteAllText(filePath, "foobar");
				File.Exists(filePath).Should().Be.True();
			}

			File.Exists(filePath).Should().Be.False();
		}

	}
}