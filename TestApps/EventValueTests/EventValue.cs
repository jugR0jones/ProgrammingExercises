using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventValueTests
{
    public class EventValue<T>
    {
        // Properties.
        public T Value
        {
            get
            {
                return internalValue;
            }
            set
            {
                if (!equalityComparer.Equals(internalValue, value))
                {
                    internalValue = value;
                    onChangedCallback(value);
                }
            }
        }

        // Private.
        private T internalValue;
        private readonly Action<T> onChangedCallback = null;
        private readonly IEqualityComparer<T> equalityComparer;

        // Public Methods.
        public EventValue(T defaultValue, Action<T> onChangedCallback, IEqualityComparer<T> customEqualityComparer = null)
        {
            internalValue = defaultValue;
            this.onChangedCallback = onChangedCallback;
            equalityComparer = customEqualityComparer ?? EqualityComparer<T>.Default;
        }

    }
}
