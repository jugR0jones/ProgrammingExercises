using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ArrayBasedStackImplementation
{
    /// <summary>
    /// A stack intended to be quick and for small operations.
    /// Similar to the stack in C#, but without the safety.
    /// Includes additional helper methods as well. Excludes the
    /// IEnumerable functionality found in the C# implementation.
    /// </summary>
    /// <typeparam name="T">The type of element to use for the stack.</typeparam>
    [DebuggerDisplay("Count = {Count}")]
    public class LHDFrameworkStack<T>
    {
        #region Public Property

        public int Count { get; private set; }

        #endregion

        #region Private Variables

        /// <summary>
        /// Backing array for the actual data.
        /// </summary>
        private T[] array;

        /// <summary>
        /// Default number of items to start a stack with.
        /// </summary>
        private const int defaultCapacity = 4;

        // Empty array used for the default constructor.
        private static T[] emptyArray = new T[0];

        #endregion

        public LHDFrameworkStack()
        {
            array = emptyArray;
            Count = 0;
        }

        public LHDFrameworkStack(int capacity)
        {
            array = new T[capacity];
            Count = 0;
        }

        public void Clear()
        {
            Array.Clear(array, 0, Count);
            Count = 0;
        }

        /// <summary>
        /// Check if the stack contains the specified item.<br>
        /// <i>This method does not handle null values. Its a performance thing.</i>
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            int count = Count;

            EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
            while (count-- > 0)
            {
                if (array[count] != null && equalityComparer.Equals(array[count], item))
                {
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(T[] destinationArray, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("destinationArray");
            }

            if (arrayIndex < 0 || arrayIndex > array.Length)
            {
                throw new ArgumentOutOfRangeException("arrayIndex");
            }

            if (array.Length - arrayIndex < Count)
            {
                throw new InvalidOperationException("Invalid operation: Destination array does not have enough elements.");
            }

            Array.Copy(array, 0, destinationArray, arrayIndex, Count);
            Array.Reverse(destinationArray, arrayIndex, Count);
        }

        public T Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Invalid operation: Stack is empty.");
            }

            return array[Count - 1];
        }

        public T Peek(int index)
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Invalid operation: Stack is empty.");
            }

            int arrayIndex = Count - index - 1;
            if (arrayIndex < 0 || arrayIndex > array.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            return array[arrayIndex];
        }

        public T Pop()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Invalid operation: Stack is empty.");
            }

            T item = array[--Count];
            array[Count] = default(T);

            return item;
        }

        public void Push(T item)
        {
            if (Count == array.Length)
            {
                int newSize = array.Length == 0 ? defaultCapacity : 2 * array.Length;
                T[] newArray = new T[newSize];

                Array.Copy(array, 0, newArray, 0, Count);

                array = newArray;
            }

            array[Count++] = item;
        }

        /// <summary>
        /// Move an item to the top of the stack.<br>
        /// <i>This method does not handle null values. Its a performance thing.</i>
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public void MoveItemToTopOfStack(T item)
        {
            if (((System.Object)item) == null)
            {
                throw new InvalidOperationException("Invalid operation: Item to move to top of stack is empty.");
            }

            int count = Count;

            EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
            while (count-- > 0)
            {
                if (array[count] != null && equalityComparer.Equals(array[count], item))
                {
                    // count contains the index of the item
                    T temporaryItem = array[count];
                    Array.Copy(array, count + 1, array, count, array.Length - count - 1);

                    array[Count-1] = temporaryItem;
                }
            }
        }
    }
}
