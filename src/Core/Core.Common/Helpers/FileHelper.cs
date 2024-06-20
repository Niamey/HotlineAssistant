using System.IO;
using System.Threading.Tasks;

namespace Vostok.Hotline.Core.Common.Helpers
{
	public static class FileHelper
	{
		public static async Task<byte[]> GetFileAsync(string path)
		{
			if (!Directory.Exists(Path.GetDirectoryName(path)))
			{
				throw new DirectoryNotFoundException($"Path {Path.GetDirectoryName(path)} not exist");
			}
			
			if (!File.Exists(path))
			{
				throw new FileNotFoundException($"File {path} not exist");
			}

			using (FileStream fsSource = new FileStream(path,
				FileMode.Open, FileAccess.Read))
			{
				byte[] bytes = new byte[fsSource.Length];
				int numBytesToRead = (int)fsSource.Length;
				int readChunk = 256;
				int numBytesRead = 0;

				while (numBytesToRead > 0)
				{
					if (numBytesToRead < readChunk) readChunk = numBytesToRead;

					int n = await fsSource.ReadAsync(bytes, numBytesRead, readChunk);

					if (n == 0)
						break;

					numBytesRead += n;

					numBytesToRead -= n;
				}
				return bytes;
			}
		}
	}
}
