// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

namespace HeapVsStack;

internal static class Program
{
    internal static void Main()
    {
        StackVsHeapTestRunner stackVsHeapTestRunner = new StackVsHeapTestRunner();
        
        stackVsHeapTestRunner.Run();
    }

    private interface ITest
    {
        public void Run();
    }

    private class CounterOnTheHeap : ITest
    {
        private readonly int numberOfIterations;
        
        private int stackOuterLoopIndex;
        private int stackInnerLoopIndex;
        private int stackCounter;
        
        public CounterOnTheHeap(int numberOfIterations)
        {
            this.numberOfIterations = numberOfIterations;
        }
        
        #region ITest Methods
        
        public void Run()
        {
            Stopwatch stopwatch = new Stopwatch();
            
            stopwatch.Start();
            
            for (stackOuterLoopIndex = 0; stackOuterLoopIndex < numberOfIterations; stackOuterLoopIndex++)
            {
                stackCounter = 0;
                for (stackInnerLoopIndex = 0; stackInnerLoopIndex < numberOfIterations; stackInnerLoopIndex++)
                {
                    stackCounter++;
                }
            }
            
            stopwatch.Stop();
            Console.WriteLine("CounterOnTheHeap: " + stopwatch.ElapsedTicks);
        }
        
        #endregion
    }

    private class CounterOnTheStackSlow : ITest
    {
        private readonly int numberOfIterations;
        
        public CounterOnTheStackSlow(int numberOfIterations)
        {
            this.numberOfIterations = numberOfIterations;
        }
        
        #region ITest Methods
        
        public void Run()
        {
            Stopwatch stopwatch = new Stopwatch();
            
            stopwatch.Start();

            int counter;
            for(int outerLoopIndex = numberOfIterations-1;outerLoopIndex>=0;outerLoopIndex--)
            {
                counter = 0;
                for(int innerLoopIndex = numberOfIterations-1;innerLoopIndex>=0;innerLoopIndex--)
                {
                    counter++;
                }
            }
            
            stopwatch.Stop();
            Console.WriteLine("CounterOnTheStackSlow: " + stopwatch.ElapsedTicks);
        }
        
        #endregion
    }
    
    private class CounterOnTheStack : ITest
    {
        private readonly int numberOfIterations;
        
        public CounterOnTheStack(int numberOfIterations)
        {
            this.numberOfIterations = numberOfIterations;
        }
        
        #region ITest Methods
        
        public void Run()
        {
            Stopwatch stopwatch = new Stopwatch();
            
            stopwatch.Start();
            
            for (int outerLoopIndex = 0; outerLoopIndex < numberOfIterations; outerLoopIndex++)
            {
                int counter = 0;
                for (int innerLoopIndex = 0; innerLoopIndex < numberOfIterations; innerLoopIndex++)
                {
                    counter++;
                }
            }
            
            stopwatch.Stop();
            Console.WriteLine("CounterOnTheStack: " + stopwatch.ElapsedTicks);
        }
        
        #endregion
    }
    
    private class CounterOnTheStackFast : ITest
    {
        private readonly int numberOfIterations;
        
        public CounterOnTheStackFast(int numberOfIterations)
        {
            this.numberOfIterations = numberOfIterations;
        }
        
        #region ITest Methods
        
        public void Run()
        {
            Stopwatch stopwatch = new Stopwatch();
            
            stopwatch.Start();
            
            for(int outerLoopIndex = numberOfIterations-1;outerLoopIndex>=0;outerLoopIndex--)
            {
                int counter = 0;
                for(int innerLoopIndex = numberOfIterations-1;innerLoopIndex>=0;--innerLoopIndex)
                {
                    counter++;
                }
            }
            
            stopwatch.Stop();
            Console.WriteLine("CounterOnTheStackFast: " + stopwatch.ElapsedTicks);
        }
        
        #endregion
    }
    
    private class CounterOnTheStackWithMethods : ITest
    {
        private readonly int numberOfIterations;
        
        private int stackOuterLoopIndex;
        private int stackInnerLoopIndex;
        private int stackCounter;
        
        public CounterOnTheStackWithMethods(int numberOfIterations)
        {
            this.numberOfIterations = numberOfIterations;
        }
        
        #region ITest Methods
        
        public void Run()
        {
            Stopwatch stopwatch = new Stopwatch();
            
            stopwatch.Start();
            
            for (stackOuterLoopIndex = 0; stackOuterLoopIndex < numberOfIterations; stackOuterLoopIndex++)
            {
                stackCounter = 0;
                IncrementHeapCounterInALoop();
            }
            
            stopwatch.Stop();
            Console.WriteLine("CounterOnTheStackWithMethods: " + stopwatch.ElapsedTicks);
        }
        
        #endregion
        
        private void IncrementHeapCounterInALoop()
        {
            for (stackInnerLoopIndex = 0; stackInnerLoopIndex < numberOfIterations; stackInnerLoopIndex++)
            {
                IncrementStackCounter();
            }
        }
        
        private void IncrementStackCounter()
        {
            stackCounter++;
        }
    }
    
    /// <summary>
    /// Run the tests. Set the parameters, etc.
    /// </summary>
    private class StackVsHeapTestRunner
    {
        private const int NumberOfIterations = 10_000;
        private const int NumberOfTimesToRunTheTests = 4;
        
        private int stackOuterLoopIndex;
        private int stackInnerLoopIndex;
        private int stackCounter;

        private readonly ITest counterOnTheHeapTest = new CounterOnTheHeap(NumberOfIterations);
        private readonly ITest counterOnTheStackTest = new CounterOnTheStack(NumberOfIterations);
        private readonly ITest counterOnTheStackWithMethods = new CounterOnTheStackWithMethods(NumberOfIterations);
        private readonly ITest counterOnTheStackFast = new CounterOnTheStackFast(NumberOfIterations);
        private readonly ITest counterOnTheStackSlow = new CounterOnTheStackSlow(NumberOfIterations);
            
        public void Run()
        {
            for (int i = 0; i < NumberOfTimesToRunTheTests; i++)
            {
                counterOnTheHeapTest.Run();                
            }

            for (int i = 0; i < NumberOfTimesToRunTheTests; i++)
            {
                counterOnTheStackWithMethods.Run();                
            }

            for (int i = 0; i < NumberOfTimesToRunTheTests; i++)
            {
                counterOnTheStackTest.Run();                
            }
            
            for (int i = 0; i < NumberOfTimesToRunTheTests; i++)
            {
                counterOnTheStackFast.Run();                
            }
            
            for (int i = 0; i < NumberOfTimesToRunTheTests; i++)
            {
                counterOnTheStackSlow.Run();                
            }
            
            RunStackLoopWithLotsOfMethods();
            RunStackLoopWithLotsOfMethods();
            RunStackLoopWithLotsOfMethods();
            RunStackLoopWithLotsOfMethods();
            RunHeapLoopWithLotsOfMethods();
            RunHeapLoopWithLotsOfMethods();
            RunHeapLoopWithLotsOfMethods();
            RunHeapLoopWithLotsOfMethods();
        }

        private void RunStackLoopWithLotsOfMethods()
        {
           
        }

      
        
   
        
        private void RunHeapLoopWithLotsOfMethods()
        {
            Stopwatch stopwatch = new Stopwatch();
            
            stopwatch.Start();
            
            for (int outerLoopIndex = 0; outerLoopIndex < NumberOfIterations; outerLoopIndex++)
            {
                IncrementStackCounterInALoop(NumberOfIterations);
            }
            
            stopwatch.Stop();
            Console.WriteLine("RunHeapLoopWithLotsOfMethods: " + stopwatch.ElapsedTicks);
        }

        private void IncrementStackCounterInALoop(int numberOfIterationsFromHeap)
        {
            int counter = 0;
            for (int innerLoopIndex = 0; innerLoopIndex < numberOfIterationsFromHeap; innerLoopIndex++)
            {
                counter = IncrementHeapCounterAndReturnTheValue(counter);
            }
        }
        
        private int IncrementHeapCounterAndReturnTheValue(int counter)
        {
            return counter++;
        }
        
        private void RunStackLoopWithLotsOfMethodsAndAReference()
        {
            Stopwatch stopwatch = new Stopwatch();
            
            stopwatch.Start();
            
            for (int outerLoopIndex = 0; outerLoopIndex < NumberOfIterations; outerLoopIndex++)
            {
                IncrementStackCounterInALoopWithAReference(NumberOfIterations);
            }
            
            stopwatch.Stop();
            Console.WriteLine("RunHeapLoopWithLotsOfMethods: " + stopwatch.ElapsedTicks);
        }
        
        private void IncrementStackCounterInALoopWithAReference(in int numberOfIterationsFromHeap)
        {
            int counter = 0;
            for (int innerLoopIndex = 0; innerLoopIndex < numberOfIterationsFromHeap; innerLoopIndex++)
            {
                counter = IncrementHeapCounterAndReturnTheValue(counter);
            }
        }
    }
}