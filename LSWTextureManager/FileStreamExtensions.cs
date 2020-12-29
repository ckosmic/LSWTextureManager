using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSWTextureManager
{
	static class FileStreamExtensions
	{
		public static int ReadInt32(this FileStream fs) {
			byte[] array = new byte[4];
			fs.Read(array, 0, 4);
			return BitConverter.ToInt32(array, 0);
		}

		public static short ReadInt16(this FileStream fs) {
			byte[] array = new byte[2];
			fs.Read(array, 0, 2);
			return BitConverter.ToInt16(array, 0);
		}
	}
}
