namespace NetBufferOptimisations;

 public static class BitConvertHelper
    {
#if OPTIMIZED_CONVERT
        /// <summary>
        /// Helper struct required for optimized conversion of float values.
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        struct FloatConvertHelper
        {
            [FieldOffset(0)]
            public float value;
#if BIG_ENDIAN
            [FieldOffset(3)]
            public byte byte0;

            [FieldOffset(2)]
            public byte byte1;

            [FieldOffset(1)]
            public byte byte2;

            [FieldOffset(0)]
            public byte byte3;
#else

            [FieldOffset(0)]
            public byte byte0;

            [FieldOffset(1)]
            public byte byte1;

            [FieldOffset(2)]
            public byte byte2;

            [FieldOffset(3)]
            public byte byte3;
#endif
        }

        /// <summary>
        /// Helper struct required for optimized conversion of double values.
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        struct DoubleConvertHelper
        {
            [FieldOffset(0)]
            public double value;

#if BIG_ENDIAN
            [FieldOffset(7)]
            public byte byte0;

            [FieldOffset(6)]
            public byte byte1;

            [FieldOffset(5)]
            public byte byte2;

            [FieldOffset(4)]
            public byte byte3;

            [FieldOffset(3)]
            public byte byte4;

            [FieldOffset(2)]
            public byte byte5;

            [FieldOffset(1)]
            public byte byte6;

            [FieldOffset(0)]
            public byte byte7;
#else
            [FieldOffset(0)]
            public byte byte0;

            [FieldOffset(1)]
            public byte byte1;

            [FieldOffset(2)]
            public byte byte2;

            [FieldOffset(3)]
            public byte byte3;

            [FieldOffset(4)]
            public byte byte4;

            [FieldOffset(5)]
            public byte byte5;

            [FieldOffset(6)]
            public byte byte6;

            [FieldOffset(7)]
            public byte byte7;
#endif
        }
#endif

        /// <summary>
        /// Converts a float value to a byte array.
        /// </summary>
        public static void FloatToBytes(float value, byte[] bytes)
        {
#if OPTIMIZED_CONVERT
            FloatConvertHelper fch = default(FloatConvertHelper);
            fch.value = value;
            bytes[0] = fch.byte0;
            bytes[1] = fch.byte1;
            bytes[2] = fch.byte2;
            bytes[3] = fch.byte3;
#else
            bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
#endif
        }

        /// <summary>
        /// Converts a byte array to a float value.
        /// </summary>
        public static float FloatFromBytes(byte[] bytes)
        {
#if OPTIMIZED_CONVERT
            FloatConvertHelper fch = default(FloatConvertHelper);
            fch.byte0 = bytes[0];
            fch.byte1 = bytes[1];
            fch.byte2 = bytes[2];
            fch.byte3 = bytes[3];
            return fch.value;
#else
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            return BitConverter.ToSingle(bytes, 0);
#endif
        }

        /// <summary>
        /// Converts a double value to a byte array.
        /// </summary>
        public static void DoubleToBytes(double value, byte[] bytes)
        {
#if OPTIMIZED_CONVERT
            DoubleConvertHelper dch = default(DoubleConvertHelper);
            dch.value = value;
            bytes[0] = dch.byte0;
            bytes[1] = dch.byte1;
            bytes[2] = dch.byte2;
            bytes[3] = dch.byte3;
            bytes[4] = dch.byte4;
            bytes[5] = dch.byte5;
            bytes[6] = dch.byte6;
            bytes[7] = dch.byte7;
#else
            bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
#endif
        }

        /// <summary>
        /// Converts a byte array to a double value.
        /// </summary>
        public static double DoubleFromBytes(byte[] bytes)
        {
#if OPTIMIZED_CONVERT
            DoubleConvertHelper dch = default(DoubleConvertHelper);
            dch.byte0 = bytes[0];
            dch.byte1 = bytes[1];
            dch.byte2 = bytes[2];
            dch.byte3 = bytes[3];
            dch.byte4 = bytes[4];
            dch.byte5 = bytes[5];
            dch.byte6 = bytes[6];
            dch.byte7 = bytes[7];
            return dch.value;
#else
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            return BitConverter.ToDouble(bytes, 0);
#endif
        }
    }