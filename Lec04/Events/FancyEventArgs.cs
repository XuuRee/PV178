using System;

namespace cv04
{
    public class FancyEventArgs : EventArgs
    {
        public FancyEventArgs(int prewValue, int newValue, string propertyName)
        {
            this.PrewValue = prewValue;
            this.NewValue = newValue;
            this.PropertyName = propertyName;
        }

        public int PrewValue { get; }
        public int NewValue { get; }
        public string PropertyName { get; }
    }
}