using System.Diagnostics;
using System.Text;

namespace NetBufferOptimisations
{
    /// <summary>
    /// A first-in-first-out (FIFO) bit packer that stores data in 4-byte chunks and can be manipulated on a bit level.
    /// </summary>
    public class BitPacker
    {
        #region Constants

        private static readonly char ZERO_CHAR = '0';
        private static readonly string SPACE_STRING = " ";

        private const int DEFAULT_CAPACITY = 8;
        private const int STRING_CAPACITY = 1024;
        private const int GROW_MULTIPLIER = 2;
        private const int GROW_ADDITION = 1;

        #endregion Constants

        #region PrivateFields

        /// <summary>
        /// The position of the bit that will be read next.
        /// </summary>
        private int m_ReadPos;

        /// <summary>
        /// The position of the bit that will be written next.
        /// </summary>
        private int m_WritePos;

        /// <summary>
        /// Size of the original data in bytes.
        /// </summary>
        private int m_OriginalBytes;

        /// <summary>
        /// Maximum size of the string builder.
        /// </summary>
        private int m_StringCapacity;

        /// <summary>
        /// Multipler that is used when the internal data structure is expanded.
        /// </summary>
        private int m_GrowMultiplier;

        /// <summary>
        /// Constant that is used when the internal data structure is expanded.
        /// </summary>
        private int m_GrowAddition;

        /// <summary>
        /// Chunks of 4 bytes for storing data.
        /// </summary>
        private uint[] m_Chunks;

        /// <summary>
        /// Encoding object for UTF8 strings.
        /// </summary>
        private UTF8Encoding m_EncodingUTF8;

        /// <summary>
        /// Encoding object for ASCII strings.
        /// </summary>
        private ASCIIEncoding m_EncodingASCII;

        /// <summary>
        /// Builder object for strings.
        /// </summary>
        private StringBuilder m_BuilderString;

        /// <summary>
        /// Builder object for characters.
        /// </summary>
        private char[] m_BuilderChars;

        /// <summary>
        /// Builder object for bytes.
        /// </summary>
        private byte[] m_BuilderBytes;

        /// <summary>
        /// Builder object for float values.
        /// </summary>
        private byte[] m_BuilderFloat;

        /// <summary>
        /// Builder object for double values.
        /// </summary>
        private byte[] m_BuilderDouble;

        #endregion PrivateFields

        #region Properties

        /// <summary>
        /// Size of the packed data in bits.
        /// </summary>
        public int PackedBits { get { return m_WritePos; } }

        /// <summary>
        /// Size of the packed data in bytes.
        /// </summary>
        public int PackedBytes { get { return ((m_WritePos - 1) >> 3) + 1; } }

        /// <summary>
        /// Size of the original data in bits.
        /// </summary>
        public int OriginalBits { get { return m_OriginalBytes * 8; } }

        /// <summary>
        /// Size of the original data in bytes.
        /// </summary>
        public int OriginalBytes { get { return m_OriginalBytes; } }

        /// <summary>
        /// Number of bytes saved by the bit packer.
        /// </summary>
        public int SavedBytes { get { return OriginalBytes > PackedBytes ? (OriginalBytes - PackedBytes) : 0; } }

        /// <summary>
        /// Percentage saved by the bit packer.
        /// </summary>
        public float SavedPercentage { get { return OriginalBytes > 0 ? (100f * (float)SavedBytes / (float)OriginalBytes) : 0; } }

        /// <summary>
        /// Returns true if the read position reaches the end of the data.
        /// </summary>
        public bool IsFinished { get { return m_ReadPos == m_WritePos; } }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="capacity">Size of the bit packer in 4 byte data chunks.</param>
        /// <param name="stringCapacity">Maximum length of the string that can be written into the bit packer.</param>
        /// <param name="growMultiplier">Multipler that is used when the internal data structure is expanded.</param>
        /// <param name="growAddition">Constant that is used when the internal data structure is expanded.</param>
        public BitPacker(
            int capacity = DEFAULT_CAPACITY,
            int stringCapacity = STRING_CAPACITY,
            int growMultiplier = GROW_MULTIPLIER,
            int growAddition = GROW_ADDITION)
        {
            m_Chunks = new uint[capacity];
            m_StringCapacity = stringCapacity;
            m_GrowMultiplier = growMultiplier;
            m_GrowAddition = growAddition;
            Clear();
        }

        #endregion Constructors

        #region Operations

        /// <summary>
        /// Resets the position cursors of the bit packer. Does not overwrite values.
        /// </summary>
        public void Clear()
        {
            m_ReadPos = 0;
            m_WritePos = 0;
            m_OriginalBytes = 0;
        }

        /// <summary>
        /// Packs the content of the bit packer into the provided buffer and returns the size of the packed data in bytes.
        /// The size of the buffer should be at least the total original size of the data written into the bit packer.
        /// </summary>
        public int PackNonAlloc(byte[] buffer)
        {
            // Write an indicator bit to be able to find the correct position and clear bad data
            Write(1, 1);

            // Check whether the byte array is long enough the store all the data
            int numChunks = (m_WritePos >> 5) + 1;
            int chunkSize = numChunks * 4;
            if (buffer.Length < chunkSize)
            {
                Console.WriteLine("The provided byte array is too small to be able to store all the data. Required size: " + chunkSize + " bytes.");

                return -1;
            }

            // Read data in chunks of 4 bytes and store them in the byte array
            for (int i = 0; i < numChunks; i++)
            {
                int dataIdx = i * 4;
                uint chunk = m_Chunks[i];
                buffer[dataIdx] = (byte)(chunk);
                buffer[dataIdx + 1] = (byte)(chunk >> 8);
                buffer[dataIdx + 2] = (byte)(chunk >> 16);
                buffer[dataIdx + 3] = (byte)(chunk >> 24);
            }

            // Return size
            return PackedBytes;
        }

        /// <summary>
        /// Packs the content of the bit packer into a byte buffer.
        /// </summary>
        public byte[] Pack()
        {
            // Write an indicator bit to be able to find the correct position and clear bad data
            Write(1, 1);

            // Allocate byte array
            byte[] data = new byte[PackedBytes];

            // Read data in chunks of 4 bytes and store them in the byte array
            int numChunks = (m_WritePos >> 5) + 1;
            for (int i = 0; i < numChunks; i++)
            {
                int dataIdx = i * 4;
                uint chunk = m_Chunks[i];
                if (dataIdx < data.Length)
                {
                    data[dataIdx] = (byte)(chunk);
                }
                if ((dataIdx + 1) < data.Length)
                {
                    data[dataIdx + 1] = (byte)(chunk >> 8);
                }
                if ((dataIdx + 2) < data.Length)
                {
                    data[dataIdx + 2] = (byte)(chunk >> 16);
                }
                if ((dataIdx + 3) < data.Length)
                {
                    data[dataIdx + 3] = (byte)(chunk >> 24);
                }
            }

            // Return data
            return data;
        }
        
        /// <summary>
        /// Packs the content of the bit packer into a byte buffer.
        /// </summary>
        public byte[] Pack2()
        {
            // Write an indicator bit to be able to find the correct position and clear bad data
            Write(1, 1);
//WriteIndicatorBitForPacking();

            // Allocate byte array
            byte[] data = new byte[PackedBytes];

            uint chunk;
            int dataIdx = 0;
            int dataLength = data.Length;
            // Read data in chunks of 4 bytes and store them in the byte array
            int numChunks = (m_WritePos >> 5);

            for (int i = 0; i < numChunks; i++)
            {
                chunk = m_Chunks[i];
                data[dataIdx++] = (byte)(chunk);
                data[dataIdx++] = (byte)(chunk >> 8);
                data[dataIdx++] = (byte)(chunk >> 16);
                data[dataIdx++] = (byte)(chunk >> 24);
            }
            
            chunk = m_Chunks[numChunks];
            if (dataIdx < dataLength)
            {
                data[dataIdx] = (byte) (chunk);
            }
            dataIdx++;
            if (dataIdx < dataLength)
            {
                data[dataIdx] = (byte) (chunk >> 8);
            }

            dataIdx++;
            if (dataIdx < dataLength)
            {
                data[dataIdx] = (byte) (chunk >> 16);
            }

            dataIdx++;
            if (dataIdx < dataLength)
            {
                data[dataIdx] = (byte) (chunk >> 24);
            }

            // Return data
            return data;
        }

        /// <summary>
        /// Unpacks the provided byte buffer and loads the data into the bit packer overwriting the current content.
        /// </summary>
        public void Unpack(byte[] buffer, int length)
        {
            // Expand chunks if needed
            int numChunks = (length / 4) + 1;
            if (m_Chunks.Length < numChunks)
            {
                m_Chunks = new uint[numChunks];
            }

            // Load data
            for (int i = 0; i < numChunks; i++)
            {
                int dataIdx = i * 4;
                uint chunk =
                  ((dataIdx) < length ? (uint)buffer[dataIdx] : 0) |
                  ((dataIdx + 1) < length ? (uint)buffer[dataIdx + 1] << 8 : 0) |
                  ((dataIdx + 2) < length ? (uint)buffer[dataIdx + 2] << 16 : 0) |
                  ((dataIdx + 3) < length ? (uint)buffer[dataIdx + 3] << 24 : 0);
                m_Chunks[i] = chunk;
            }

            // Find the indicator bit position
            int lastBytePos = length - 1;
            int indicatorBitPos = BitUtils.FindHighestBitPosition(buffer[lastBytePos]);

            // Substract one from the indicator bit position to get the position of the last bit
            int lastBitPos = indicatorBitPos - 1;

            // Calculate read and write positions
            m_WritePos = (lastBytePos * 8) + lastBitPos;
            m_ReadPos = 0;
        }

        /// <summary>
        /// Increases original size counter
        /// </summary>
        public void IncreaseOriginalSize(int bytes)
        {
            m_OriginalBytes += bytes;
        }

        #endregion Operations

        #region Bit

        /// <summary>
        /// Takes the lower numBits from the value and stores them in the bit packer.
        /// </summary>
        public void Write(int numBits, uint value)
        {
            // Handle error
            if (numBits < 0 || numBits > 32)
            {
                Console.WriteLine("Number of bits should be between 0 and 32 in a Write operation.");
                return;
            }

            // Calculate index
            int index = m_WritePos >> 5;
            int used = m_WritePos & 0x0000001F;

            // Expand array if needed
            if ((index + 1) >= m_Chunks.Length)
            {
                ExpandArray();
            }

            // Prepare data
            ulong chunkMask = ((1UL << used) - 1);
            ulong scratch = m_Chunks[index] & chunkMask;
            ulong result = scratch | ((ulong)value << used);

            // Write data
            m_Chunks[index] = (uint)result;
            m_Chunks[index + 1] = (uint)(result >> 32);

            // Increase write position
            m_WritePos += numBits;
        }
        
        public void WriteIndicatorBitForPacking()
        {
            // Calculate index
            int index = m_WritePos >> 5;
            int used = m_WritePos & 0x0000001F;

            // Expand array if needed
            if (index + 1 >= m_Chunks.Length)
            {
                ExpandArray();
            }

            // Prepare data
            ulong chunkMask = ((1UL << used) - 1);
            ulong scratch = m_Chunks[index] & chunkMask;
            ulong result = scratch | (1UL << used);

            // Write data
            m_Chunks[index] = (uint)result;
            m_Chunks[index + 1] = (uint)(result >> 32);

            // Increase write position
            m_WritePos += 1;
        }

        /// <summary>
        /// Reads the next numBits from the bit packer.
        /// </summary>
        public uint Read(int numBits)
        {
            // Peek data
            uint result = Peek(numBits);

            // Increase read position
            m_ReadPos += numBits;

            // Return data
            return result;
        }

        /// <summary>
        /// Peeks at the next numBits from the bit packer.
        /// </summary>
        public uint Peek(int numBits)
        {
            // Handle error
            if (numBits < 0 || numBits > 32)
            {
                    Console.WriteLine("Number of bits should be between 0 and 32 in a Peek operation.");
        
                return 0;
            }

            // Calculate index
            int index = m_ReadPos >> 5;
            int used = m_ReadPos & 0x0000001F;

            // Prepare data
            ulong chunkMask = ((1UL << numBits) - 1) << used;
            ulong scratch = (ulong)m_Chunks[index];
            if ((index + 1) < m_Chunks.Length)
            {
                scratch |= (ulong)m_Chunks[index + 1] << 32;
            }

            // Peek data
            ulong result = (scratch & chunkMask) >> used;
            return (uint)result;
        }

        /// <summary>
        /// Peeks at the next numBits from the bit packer using and offset.
        /// </summary>
        public uint Peek(int numBits, int offsetBits)
        {
            // Handle error
            if (numBits < 0 || numBits > 32)
            {
                    Console.WriteLine("Number of bits should be between 0 and 32 in a Peek operation.");
     
                return 0;
            }

            // Handle error
            int pos = (m_ReadPos + offsetBits);
            if (pos >= m_WritePos)
            {
                    Console.WriteLine("Offset is too big. You cannot exceed the buffer size.");
     
                return 0;
            }

            // Calculate index
            int index = pos >> 5;
            int used = pos & 0x0000001F;

            // Prepare data
            ulong chunkMask = ((1UL << numBits) - 1) << used;
            ulong scratch = (ulong)m_Chunks[index];
            if ((index + 1) < m_Chunks.Length)
            {
                scratch |= (ulong)m_Chunks[index + 1] << 32;
            }

            // Peek data
            ulong result = (scratch & chunkMask) >> used;
            return (uint)result;
        }

        /// <summary>
        /// Inserts data at a given position into the bit packer.
        /// </summary>
        public void Insert(int position, int numBits, uint value)
        {
            // Handle error
            if (numBits < 0 || numBits > 32)
            {
                Console.WriteLine("Number of bits should be between 0 and 32 in an Insert operation."); 

                return;
            }

            // Calculate index
            int index = position >> 5;
            int used = position & 0x0000001F;

            // Prepare data
            ulong valueMask = (1UL << numBits) - 1;
            ulong prepared = (ulong)(value & valueMask) << used;
            ulong scratch =
              (ulong)m_Chunks[index] |
              (ulong)m_Chunks[index + 1] << 32;
            ulong result = scratch | prepared;

            // Insert data
            m_Chunks[index] = (uint)result;
            m_Chunks[index + 1] = (uint)(result >> 32);
        }

        private void ExpandArray()
        {
            // Calculate new capacity
            int newCapacity = (m_Chunks.Length * m_GrowMultiplier) + m_GrowAddition;

            // Allocate an expanded data structure an copy data
            uint[] newChunks = new uint[newCapacity];
            Array.Copy(m_Chunks, newChunks, m_Chunks.Length);

            // Overwrite the old data structure
            m_Chunks = newChunks;
        }

        #endregion Bit

        #region Bool

        /// <summary>
        /// Writes a boolean value into the bit packer.
        /// </summary>
        public void WriteBool(bool value)
        {
            Write(1, value ? 1U : 0U);
            m_OriginalBytes += 1;
        }

        /// <summary>
        /// Reads a boolean value from the bit packer.
        /// </summary>
        public bool ReadBool()
        {
            return (Read(1) > 0);
        }

        /// <summary>
        /// Peeks at the next boolean value from the bit packer.
        /// </summary>
        public bool PeekBool()
        {
            return (Peek(1) > 0);
        }

        #endregion Bool

        #region Byte

        /// <summary>
        /// Writes a byte value into the bit packer.
        /// </summary>
        public void WriteByte(byte value)
        {
            Write(8, value);
            m_OriginalBytes += 1;
        }

        /// <summary>
        /// Reads a byte value from the bit packer.
        /// </summary>
        public byte ReadByte()
        {
            return (byte)Read(8);
        }

        /// <summary>
        /// Peeks at the next byte value from the bit packer.
        /// </summary>
        public byte PeekByte()
        {
            return (byte)Peek(8);
        }

        #endregion Byte

        #region Int

        /// <summary>
        /// Writes an integer value into the bit packer.
        /// </summary>
        public void WriteInt(int value)
        {
            // Use zig zag encoding: https://en.wikipedia.org/wiki/Variable-length_quantity#Zigzag_encoding
            uint zigzag = (uint)((value << 1) ^ (value >> 31));
            WriteUInt(zigzag);
        }

        /// <summary>
        /// Reads an integer value from the bit packer.
        /// </summary>
        public int ReadInt()
        {
            uint value = ReadUInt();
            int zagzig = (int)((value >> 1) ^ (-(value & 1)));
            return zagzig;
        }

        /// <summary>
        /// Peeks at the next integer value from the bit packer.
        /// </summary>
        public int PeekInt()
        {
            uint value = PeekUInt();
            int zagzig = (int)((value >> 1) ^ (-(value & 1)));
            return zagzig;
        }

        #endregion Int

        #region UInt

        /// <summary>
        /// Writes an unsigned integer value into the bit packer.
        /// </summary>
        public void WriteUInt(uint value)
        {
            // Writes a variable number of bytes based on the length of the number:
            // 
            //    Bits   Min Dec    Max Dec     Max Hex     Bytes Used
            //    0-7    0          127         0x0000007F  1 byte
            //    8-14   128        16383       0x00003FFF  2 bytes
            //    15-21  16384      2097151     0x001FFFFF  3 bytes
            //    22-28  2097152    268435455   0x0FFFFFFF  4 bytes
            //    29-32  268435456  4294967295  0xFFFFFFFF  5 bytes
            //

            uint data = 0x0u;
            do
            {
                // Take the lowest 7 bits
                data = value & 0x7Fu;
                value >>= 7;

                // If there is more data, set the 8th bit to true
                if (value > 0)
                {
                    data |= 0x80u;
                }

                // Store the next byte
                Write(8, data);
            }
            while (value > 0);
            m_OriginalBytes += 4;
        }

        /// <summary>
        /// Reads an unsigned integer value from the bit packer.
        /// </summary>
        public uint ReadUInt()
        {
            uint data = 0x0u;
            uint value = 0x0u;
            int s = 0;

            do
            {
                data = Read(8);

                // Add back in the shifted 7 bits
                value |= (data & 0x7Fu) << s;
                s += 7;

                // Continue if we're flagged for more
            } while ((data & 0x80u) > 0);

            return value;
        }

        /// <summary>
        /// Peeks at the next unsigned integer value from the bit packer.
        /// </summary>
        public uint PeekUInt()
        {
            int tempPosition = m_ReadPos;
            uint value = ReadUInt();
            m_ReadPos = tempPosition;
            return value;
        }

        #endregion UInt

        #region Float

        /// <summary>
        /// Writes a single-precision floating-point value into the bit packer.
        /// </summary>
        public void WriteFloat(float value)
        {
            // Manage builder
            ManageBuilderFloat();

            // Write float
            BitConvertHelper.FloatToBytes(value, m_BuilderFloat);
            WriteBytes(m_BuilderFloat, 0, 4);
        }

        /// <summary>
        /// Reads a single-precision floating-point value from the bit packer.
        /// </summary>
        public float ReadFloat()
        {
            // Manage builder
            ManageBuilderFloat();

            // Read float
            ReadBytes(m_BuilderFloat, 0, 4);
            return BitConvertHelper.FloatFromBytes(m_BuilderFloat);
        }

        /// <summary>
        /// Peeks at the next single-precision floating-point value from the bit packer.
        /// </summary>
        public float PeekFloat()
        {
            // Manage builder
            ManageBuilderFloat();

            // Peek float
            PeekBytes(m_BuilderFloat, 0, 4);
            return BitConvertHelper.FloatFromBytes(m_BuilderFloat);
        }

        private void ManageBuilderFloat()
        {
            // Allocate builder for the first time
            if (m_BuilderFloat == null)
            {
                m_BuilderFloat = new byte[4];
            }
            // Clear builder
            else
            {
                Array.Clear(m_BuilderFloat, 0, m_BuilderFloat.Length);
            }
        }

        #endregion Float

        #region Double

        /// <summary>
        /// Writes a double-precision floating-point value into the bit packer.
        /// </summary>
        public void WriteDouble(double value)
        {
            // Manage builder
            ManageBuilderDouble();

            // Write double
            BitConvertHelper.DoubleToBytes(value, m_BuilderDouble);
            WriteBytes(m_BuilderDouble, 0, 8);
        }

        /// <summary>
        /// Reads a double-precision floating-point value from the bit packer.
        /// </summary>
        public double ReadDouble()
        {
            // Manage builder
            ManageBuilderDouble();

            // Read double
            ReadBytes(m_BuilderDouble, 0, 8);
            return BitConvertHelper.DoubleFromBytes(m_BuilderDouble);
        }

        /// <summary>
        /// Peeks at the next double-precision floating-point value from the bit packer.
        /// </summary>
        public double PeekDouble()
        {
            // Manage builder
            ManageBuilderDouble();

            // Peek double
            PeekBytes(m_BuilderDouble, 0, 8);
            return BitConvertHelper.DoubleFromBytes(m_BuilderDouble);
        }

        private void ManageBuilderDouble()
        {
            // Allocate builder for the first time
            if (m_BuilderDouble == null)
            {
                m_BuilderDouble = new byte[8];
            }
            // Clear builder
            else
            {
                Array.Clear(m_BuilderDouble, 0, m_BuilderDouble.Length);
            }
        }

        #endregion Double

        #region Long

        /// <summary>
        /// Writes a long value into the bit packer.
        /// </summary>
        public void WriteLong(long value)
        {
            // More info: https://en.wikipedia.org/wiki/Variable-length_quantity#Zigzag_encoding
            ulong zigzag = (ulong)((value << 1) ^ (value >> 63));
            WriteULong(zigzag);
        }

        /// <summary>
        /// Reads a long value from the bit packer.
        /// </summary>
        public long ReadLong()
        {
            ulong value = ReadULong();
            long zagzig = ((long)(value >> 1) ^ (-(long)(value & 1)));
            return zagzig;
        }

        /// <summary>
        /// Peeks at the next long value from the bit packer.
        /// </summary>
        public long PeekLong()
        {
            ulong value = PeekULong();
            long zagzig = ((long)(value >> 1) ^ (-(long)(value & 1)));
            return zagzig;
        }

        #endregion Long

        #region ULong

        /// <summary>
        /// Writes an unsigned long value into the bit packer.
        /// </summary>
        public void WriteULong(ulong value)
        {
            // Writes a variable number of bytes based on the length of the number:
            // 
            //    Bits   Min Decimal          Max Decimal           Max Hexadecimal     Bytes Used
            //    0-7    0                    127                   0x000000000000007F  1 byte
            //    8-14   128                  16383                 0x0000000000003FFF  2 bytes
            //    15-21  16384                2097151               0x00000000001FFFFF  3 bytes
            //    22-28  2097152              268435455             0x000000000FFFFFFF  4 bytes
            //    29-35  268435456            34359738367           0x00000007FFFFFFFF  5 bytes
            //    36-42  34359738368          4398046511103         0x000003FFFFFFFFFF  6 bytes
            //    43-49  4398046511104        562949953421311       0x0001FFFFFFFFFFFF  7 bytes
            //    50-56  562949953421312      72057594037927935     0x00FFFFFFFFFFFFFF  8 bytes
            //    57-63  72057594037927936    9223372036854775807   0x7FFFFFFFFFFFFFFF  9 bytes
            //    64-64  9223372036854775808  18446744073709551615  0xFFFFFFFFFFFFFFFF 10 bytes

            ulong data = 0x0uL;
            do
            {
                // Take the lowest 7 bits
                data = value & 0x7FuL;
                value >>= 7;

                // If there is more data, set the 8th bit to true
                if (value > 0)
                {
                    data |= 0x80uL;
                }

                // Store the next byte
                Write(8, (uint)data);
            }
            while (value > 0);
            m_OriginalBytes += 8;
        }

        /// <summary>
        /// Reads an unsigned long value from the bit packer.
        /// </summary>
        public ulong ReadULong()
        {
            ulong data = 0x0uL;
            ulong value = 0x0uL;
            int s = 0;

            do
            {
                data = Read(8);

                // Add back in the shifted 7 bits
                value |= (data & 0x7FuL) << s;
                s += 7;

                // Continue if we're flagged for more
            } while ((data & 0x80uL) > 0);

            return value;
        }

        /// <summary>
        /// Peeks at the next unsigned long value from the bit packer.
        /// </summary>
        public ulong PeekULong()
        {
            int tempPosition = m_ReadPos;
            ulong value = ReadULong();
            m_ReadPos = tempPosition;
            return value;
        }

        #endregion ULong

        #region Bytes

        /// <summary>
        /// Writes a byte array into the bit packer.
        /// </summary>
        public void WriteBytes(byte[] value, int offset = 0)
        {
            WriteBytes(value, offset, value.Length - offset);
        }

        /// <summary>
        /// Writes a byte array into the bit packer.
        /// </summary>
        public void WriteBytes(byte[] value, int offset, int numBytes)
        {
            int length = offset + Math.Min(numBytes, value.Length - offset);
            for (int i = offset; i < length; i++)
            {
                Write(8, value[i]);
            }
            m_OriginalBytes += numBytes;
        }

        /// <summary>
        /// Reads a byte array from the bit packer.
        /// </summary>
        public byte[] ReadBytes(int offset, int numBytes)
        {
            byte[] value = new byte[offset + numBytes];
            for (int i = 0; i < numBytes; i++)
            {
                value[offset + i] = (byte)Read(8);
            }
            return value;
        }

        /// <summary>
        /// Reads a byte array from the bit packer.
        /// </summary>
        public void ReadBytes(byte[] value, int offset, int numBytes)
        {
            for (int i = 0; i < numBytes; i++)
            {
                value[offset + i] = (byte)Read(8);
            }
        }

        /// <summary>
        /// Peeks at the next numBytes from the bit packer.
        /// </summary>
        public byte[] PeekBytes(int offset, int numBytes)
        {
            byte[] value = new byte[offset + numBytes];
            for (int i = 0; i < numBytes; i++)
            {
                value[offset + i] = (byte)Peek(8, i * 8);
            }
            return value;
        }

        /// <summary>
        /// Peeks at the next numBytes from the bit packer.
        /// </summary>
        public void PeekBytes(byte[] value, int offset, int numBytes)
        {
            for (int i = 0; i < numBytes; i++)
            {
                value[offset + i] = (byte)Peek(8, i * 8);
            }
        }

        #endregion Bytes

        #region String

        /// <summary>
        /// Writes an ASCII string into the bit packer.
        /// </summary>
        public void WriteStringASCII(string value)
        {
            WriteString(value, true);
        }

        /// <summary>
        /// Reads an ASCII string from the bit packer.
        /// </summary>
        public string ReadStringASCII()
        {
            return ReadString(true);
        }

        /// <summary>
        /// Peeks at the next ASCII string from the bit packer.
        /// </summary>
        public string PeekStringASCII()
        {
            return PeekString(true);
        }

        /// <summary>
        /// Writes an UTF8 string into the bit packer.
        /// </summary>
        public void WriteStringUTF8(string value)
        {
            WriteString(value, false);
        }

        /// <summary>
        /// Reads an UTF8 string from the bit packer.
        /// </summary>
        public string ReadStringUTF8()
        {
            return ReadString(false);
        }

        /// <summary>
        /// Peeks at the next UTF8 string from the bit packer.
        /// </summary>
        public string PeekStringUTF8()
        {
            return PeekString(false);
        }

        private void WriteString(string value, bool isASCII)
        {
            // Handle error
            if (value == null)
            {
                    Console.WriteLine("Value cannot be null");

                return;
            }

            // Manage encoding
            Encoding encoding = ManageEncoding(isASCII);

            // Write size
            int size = encoding.GetByteCount(value);
            WriteUInt((uint)size);

            // Manage bytes builder
            ManageBuilderBytes(size);

            // Write bytes
            encoding.GetBytes(value, 0, value.Length, m_BuilderBytes, 0);
            WriteBytes(m_BuilderBytes, 0, size);
        }

        private string ReadString(bool isASCII)
        {
            // Manage encoding
            Encoding encoding = ManageEncoding(isASCII);

            // Read size
            int size = (int)ReadUInt();

            // Manage bytes builder
            ManageBuilderBytes(size);

            // Read bytes
            ReadBytes(m_BuilderBytes, 0, size);

            // Manage string builder
            ManageBuilderString(encoding.GetCharCount(m_BuilderBytes, 0, size));

            // Construct string
            int length = encoding.GetChars(m_BuilderBytes, 0, size, m_BuilderChars, 0);
            m_BuilderString.Append(m_BuilderChars, 0, length);
            return m_BuilderString.ToString();
        }

        private string PeekString(bool isASCII)
        {
            // Manage encoding
            Encoding encoding = ManageEncoding(isASCII);

            // Peek size
            int size = (int)PeekUInt();

            // Calculate offset and number of bytes
            int offset = 0;
            if (size >= 0 && size < 128)
            {
                offset = 1;
            }
            else if (size >= 128 && size < 1024)
            {
                offset = 2;
            }
            else if (size >= 1024 && size < 2097152)
            {
                offset = 3;
            }
            else if (size >= 2097152 && size < 268435456)
            {
                offset = 4;
            }
            else
            {
                offset = 5;
            }
            int numBytes = (offset + size);

            // Manage bytes builder
            ManageBuilderBytes(numBytes);

            // Peek bytes
            PeekBytes(m_BuilderBytes, 0, numBytes);

            // Manage string builder
            ManageBuilderString(encoding.GetCharCount(m_BuilderBytes, offset, size));

            // Construct string
            int length = encoding.GetChars(m_BuilderBytes, offset, size, m_BuilderChars, 0);
            m_BuilderString.Append(m_BuilderChars, 0, length);
            return m_BuilderString.ToString();
        }

        private Encoding ManageEncoding(bool isASCII)
        {
            // Allocate encoding for the first time
            if (isASCII && m_EncodingASCII == null)
            {
                m_EncodingASCII = new ASCIIEncoding();
            }
            else if (!isASCII && m_EncodingUTF8 == null)
            {
                m_EncodingUTF8 = new UTF8Encoding();
            }

            // Return the correct encoding
            if (isASCII)
            {
                return m_EncodingASCII;
            }
            else
            {
                return m_EncodingUTF8;
            }
        }

        private void ManageBuilderBytes(int size)
        {
            // Allocate builder for the first time
            if (m_BuilderBytes == null)
            {
                m_BuilderBytes = new byte[size];
            }
            // Resize builder if needed
            else if (m_BuilderBytes.Length < size)
            {
                Array.Resize(ref m_BuilderBytes, size);
            }
            // Clear builder
            else
            {
                Array.Clear(m_BuilderBytes, 0, m_BuilderBytes.Length);
            }
        }

        private void ManageBuilderString(int length)
        {
            // Allocate builder for the first time
            if (m_BuilderChars == null)
            {
                m_BuilderChars = new char[length];
            }
            // Resize builder if needed
            else if (m_BuilderChars.Length < length)
            {
                Array.Resize(ref m_BuilderChars, length);
            }
            // Clear builder
            else
            {
                Array.Clear(m_BuilderChars, 0, m_BuilderChars.Length);
            }

            // Allocate builder for the first time
            if (m_BuilderString == null)
            {
                m_BuilderString = new StringBuilder(m_StringCapacity, m_StringCapacity);
            }
            // Clear builder
            else
            {
                m_BuilderString.Clear();
            }
        }

        #endregion String

        public override string ToString()
        {
            StringBuilder builderRaw = new StringBuilder();
            for (int i = m_Chunks.Length - 1; i >= 0; i--)
            {
                builderRaw.Append(Convert.ToString(m_Chunks[i], 2).PadLeft(32, ZERO_CHAR));
            }

            StringBuilder builderSpaced = new StringBuilder();
            for (int i = 0; i < builderRaw.Length; i++)
            {
                builderSpaced.Append(builderRaw[i]);
                if (((i + 1) % 8) == 0)
                {
                    builderSpaced.Append(SPACE_STRING);
                }
            }

            return builderSpaced.ToString();
        }
    }
}
