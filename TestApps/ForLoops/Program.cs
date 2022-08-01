using System;
using System.Diagnostics;

namespace ForLoops
{
    class Program
    {
        public class CabDisplayAlarm
        {
            #region Public Properties

            /// <summary>
            /// Is the alarm still active? Or has it been cleared.
            /// </summary>
            public bool IsActive {get; private set; }

            public string Time { get; private set; }
            public string Date { get; private set; }
            public string AmPm { get; private set; }
        
            public int Code { get; private set; }
            public string AlarmMessage { get; private set; }

            #endregion

            #region Constructors

            public CabDisplayAlarm(int code, string alarmMessage)
            {
                Code = code;
                AlarmMessage = alarmMessage;
            }

            #endregion

            #region Public Methods

            public void Activate()
            {
                IsActive = true;
            
                DateTime now = DateTime.Now;

                Time = now.ToString("hh:mm:ss");
                Date = now.ToString("dd/MM/yyyy");

                if (now.Hour < 13)
                {
                    AmPm = "AM";
                }
                else
                {
                    AmPm = "PM";
                }
            }
        
            public void Deactivate()
            {
                IsActive = false;
            }

            #endregion
        }
        
        static void Main(string[] args)
        {
            const int numberOfIterations = 100000;
            
            Stopwatch stopwatch = new Stopwatch();

            ////////////////////////////////////////////

            string[] sourceArray = new string[]
            {
                "a", "b", "c", "d", "e", "f", "g", "h", "i"
            };

            string[] destinationArray = new string[9];
            string tempString;
            
            stopwatch.Restart();

            for (int i = 0; i < numberOfIterations; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    tempString = sourceArray[3] + sourceArray[3] + sourceArray[3] + sourceArray[3];
                }
                
                tempString = String.Empty;
            }
            
            stopwatch.Stop();
            
            Console.WriteLine("Elapsed Ticks: " + stopwatch.ElapsedTicks);
            
            ////////////////////////////////////////////
            
            stopwatch.Restart();

            for (int i = 0; i < numberOfIterations; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    string a = sourceArray[3];
                    tempString = a+a+a+a;
                }
                
                tempString = String.Empty;
            }
            
            stopwatch.Stop();
            
            Console.WriteLine("Elapsed Ticks: " + stopwatch.ElapsedTicks);
            
            ////////////////////////////////////////////
            
            stopwatch.Restart();

            for (int i = 0; i < numberOfIterations; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    tempString = "a" + "a" + "a" + "a";
                }
                
                tempString = String.Empty;
            }
            
            stopwatch.Stop();
            
            Console.WriteLine("Elapsed Ticks: " + stopwatch.ElapsedTicks);
            
            ////////////////////////////////////////////
        }
    }
}