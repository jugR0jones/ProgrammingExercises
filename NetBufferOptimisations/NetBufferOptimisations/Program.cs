// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

namespace NetBufferOptimisations
{
    internal static class Program
    {
        internal static void Main()
        {
            const byte count = 255;
            
            Stopwatch stopwatch = new Stopwatch();
            byte[] bytes;
            
            // Console.WriteLine("===========================");
            // Console.WriteLine("Clear, write 255 bytes and pack them.");
            // stopwatch.Restart();
            // NetBuffer.Clear();
            //
            // for (byte i = 0; i < count; i++)
            // {
            //     NetBuffer.WriteByte(i);
            // }
            //
            // bytes = NetBuffer.Pack();
            // stopwatch.Stop();
            //
            // Console.WriteLine();
            // Console.WriteLine("Results:");
            // for (int i = 0; i < bytes.Length; i++)
            // {
            //     Console.Write(bytes[i]);
            // }
            //
            // Console.WriteLine();
            // Console.WriteLine("Elapsed ticks: " + stopwatch.ElapsedTicks);
            
            Console.WriteLine("===========================");
            Console.WriteLine("Write 255 bytes and pack them.");

            NetBuffer.Clear();
            stopwatch.Restart();

            for (byte i = 0; i < count; i++)
            {
                NetBuffer.WriteByte(i);
            }

            bytes = NetBuffer.Pack();
            stopwatch.Stop();
            
            Console.WriteLine();
            Console.WriteLine("Results:");
            for (int i = 0; i < bytes.Length; i++)
            {
                Console.Write(bytes[i]);
            }
            
            Console.WriteLine();
            Console.WriteLine("Elapsed ticks: " + stopwatch.ElapsedTicks);
            
            // Console.WriteLine("===========================");
            // Console.WriteLine("Write 255 bytes.");
            //
            // NetBuffer.Clear();
            // stopwatch.Restart();
            //
            // for (byte i = 0; i < count; i++)
            // {
            //     NetBuffer.WriteByte(i);
            // }
            //
            // stopwatch.Stop();
            // bytes = NetBuffer.Pack();
            //
            // Console.WriteLine();
            // Console.WriteLine("Results:");
            // for (int i = 0; i < bytes.Length; i++)
            // {
            //     Console.Write(bytes[i]);
            // }
            //
            // Console.WriteLine();
            // Console.WriteLine("Elapsed ticks: " + stopwatch.ElapsedTicks);
            //
            // Console.WriteLine("===========================");
            // Console.WriteLine("Write 255 bytes using ByteStream.");
            //
            // NetBuffer.Clear();
            // stopwatch.Restart();
            //
            // for (byte i = 0; i < count; i++)
            // {
            //     NetBuffer.ByteStream.WriteByte(i);
            // }
            //
            // stopwatch.Stop();
            // bytes = NetBuffer.Pack();
            //
            // Console.WriteLine();
            // Console.WriteLine("Results:");
            // for (int i = 0; i < bytes.Length; i++)
            // {
            //     Console.Write(bytes[i]);
            // }
            //
            // Console.WriteLine();
            // Console.WriteLine("Elapsed ticks: " + stopwatch.ElapsedTicks);
            //
            // Console.WriteLine("===========================");
            // Console.WriteLine("Write 255 bytes using Write.");
            //
            // NetBuffer.Clear();
            // stopwatch.Restart();
            //
            // for (byte i = 0; i < count; i++)
            // {
            //     NetBuffer.ByteStream.Write(8, i);
            // }
            //
            // stopwatch.Stop();
            // bytes = NetBuffer.Pack();
            //
            // Console.WriteLine();
            // Console.WriteLine("Results:");
            // for (int i = 0; i < bytes.Length; i++)
            // {
            //     Console.Write(bytes[i]);
            // }
            //
            // Console.WriteLine();
            // Console.WriteLine("Elapsed ticks: " + stopwatch.ElapsedTicks);
            //
            // Console.WriteLine("===========================");
            // Console.WriteLine("Write 255 bytes using cached ByteStream.Write.");
            //
            // BitPacker byteStream = NetBuffer.ByteStream;
            // NetBuffer.Clear();
            // stopwatch.Restart();
            //
            // for (byte i = 0; i < count; i++)
            // {
            //     byteStream.Write(8, i);
            // }
            //
            // stopwatch.Stop();
            // bytes = NetBuffer.Pack();
            //
            // Console.WriteLine();
            // Console.WriteLine("Results:");
            // for (int i = 0; i < bytes.Length; i++)
            // {
            //     Console.Write(bytes[i]);
            // }
            //
            // Console.WriteLine();
            // Console.WriteLine("Elapsed ticks: " + stopwatch.ElapsedTicks);
            
            Console.WriteLine("===========================");
            Console.WriteLine("Write 255 bytes and pack them.");

            NetBuffer.Clear();
            stopwatch.Restart();

            for (byte i = 0; i < count; i++)
            {
                NetBuffer.WriteByte(i);
            }

            bytes = NetBuffer.Pack2();
        //    bytes = NetBuffer.Pack();
            stopwatch.Stop();
            
            Console.WriteLine();
            Console.WriteLine("Results:");
            for (int i = 0; i < bytes.Length; i++)
            {
                Console.Write(bytes[i]);
            }
            
            Console.WriteLine();
            Console.WriteLine("Elapsed ticks: " + stopwatch.ElapsedTicks);
        }
    }
}