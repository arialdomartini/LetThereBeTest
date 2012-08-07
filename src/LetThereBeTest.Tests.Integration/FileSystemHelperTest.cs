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
	}
}