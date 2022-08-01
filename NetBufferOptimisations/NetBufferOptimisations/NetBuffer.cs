namespace NetBufferOptimisations
{
	public class NetBuffer
	{
		// Properties.
		public static BitPacker ByteStream => bitPacker;

		private static BitPacker bitPacker = new BitPacker(8, 1024, 2, 1);
		
		// Public Methods.
		public static void Clear()
		{
			ByteStream.Clear();
		}

		public static void Load(byte[] data)
		{
			Clear();
			if (data.Length == 0) return;
			ByteStream.Unpack(data, data.Length);
		}

		public static byte[] Pack()
		{
			return ByteStream.Pack();
		}
		
		public static byte[] Pack2()
		{
			return ByteStream.Pack2();
		}
		
		public static bool ReadBool()
		{
			return ByteStream.ReadBool();
		}

		public static void WriteBool(bool b)
		{
			ByteStream.WriteBool(b);
		}

		public static void WriteGuid(Guid b)
		{
			var byteArray = b.ToByteArray();
			ByteStream.WriteBytes(byteArray);
		}

		public static Guid ReadGuid()
		{
			var bytes = ReadBytes();
			return new Guid(bytes);
		}

		public static int ReadInt()
		{
			return ByteStream.ReadInt();
		}

		public static int[] ReadIntArray()
		{
			int length = ReadInt();
			int[] array = new int[length];
			for (int i = 0; i < array.Length; i++) array[i] = ReadInt();
			return array;
		}

		public static void WriteInt(int integer)
		{
			ByteStream.WriteInt(integer);
		}

		public static void WriteIntArray(int[] intArray)
		{
			if (intArray == null)
			{
				WriteInt(0);
				return;
			}

			WriteInt(intArray.Length);
			for (int i = 0; i < intArray.Length; i++) WriteInt(intArray[i]);
		}

		public static void WriteFloat(float f)
		{
			ByteStream.WriteFloat(f);
		}

		public static float ReadFloat()
		{
			return ByteStream.ReadFloat();
		}

		public static void WriteString(string s)
		{
			if (s == null)
			{
				ByteStream.WriteStringUTF8("");
				return;
			}

			ByteStream.WriteStringUTF8(s);
		}

		public static string ReadString()
		{
			return ByteStream.ReadStringUTF8();
		}

		public static void WriteByte(byte b)
		{
			ByteStream.WriteByte(b);
		}

		public static byte ReadByte()
		{
			return ByteStream.ReadByte();
		}

		public static void WriteBytes(byte[] bytes)
		{
			if (bytes == null)
			{
				WriteInt(0);
				return;
			}

			WriteInt(bytes.Length);
			for (int i = 0; i < bytes.Length; i++) WriteByte(bytes[i]);
		}

		public static byte[] ReadBytes()
		{
			byte[] bytes = new byte[ReadInt()];
			for (int i = 0; i < bytes.Length; i++) bytes[i] = ReadByte();
			return bytes;
		}
	}
}
