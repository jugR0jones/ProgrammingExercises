namespace NetBufferOptimisations;

    public static class BitUtils
    {
        // Lookup table for fast Log2 calculations:
        // http://stackoverflow.com/questions/15967240/fastest-implementation-of-log2int-and-log2float
        private static readonly int[] m_DeBruijnLookup = new int[32]
        {
            0, 9, 1, 10, 13, 21, 2, 29, 11, 14, 16, 18, 22, 25, 3, 30,
            8, 12, 20, 28, 15, 17, 24, 7, 19, 27, 23, 6, 26, 5, 4, 31
        };

        /// <summary>
        /// Optimized implementation of Log2
        /// </summary>
        public static int Log2Fast(uint v)
        {
            v |= v >> 1;
            v |= v >> 2;
            v |= v >> 4;
            v |= v >> 8;
            v |= v >> 16;

            return m_DeBruijnLookup[(v * 0x07C4ACDDU) >> 27];
        }

        /// <summary>
        /// Normal implementation of Log2
        /// </summary>
        public static int Log2(uint x)
        {
            return (int)(Math.Log(x) / Math.Log(2));
        }

        /// <summary>
        /// Finds the highest bit position in the given byte
        /// </summary>
        public static int FindHighestBitPosition(byte data)
        {
            int shiftCount = 0;
            while (data > 0)
            {
                data >>= 1;
                shiftCount++;
            }
            return shiftCount;
        }


#if !NETFX_CORE
        /// <summary>
        /// Extension method that clears the string builder without deallocating memory
        /// </summary>
        public static void Clear(this System.Text.StringBuilder sb)
        {
            sb.Length = 0;
        }
#endif
    }