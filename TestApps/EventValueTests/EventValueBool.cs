using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventValueTests
{
    public class EventValueBool
    {
        // Properties.
        public bool Value
        {
            get
            {
                return internalValue;
            }
            set
            {
                if (internalValue != value)
                {
                    internalValue = value;
                    onChangedCallback(value);
                }
            }
        }

        public void SetValue(bool val)
        {
            if (internalValue != val)
            {
                internalValue = val;
                onChangedCallback(val);
            }
        }

        // Private.
        private bool internalValue;
        private readonly Action<bool> onChangedCallback = null;

        // Public Methods.
        public EventValueBool(bool defaultValue, Action<bool> onChangedCallback)
        {
            internalValue = defaultValue;
            this.onChangedCallback = onChangedCallback;
        }

    }
}
