using System;

namespace cv04
{
    internal class FancyPoint
    {
        int x;
        int y;

        public FancyPoint()
        {
            x = 0;
            y = 0;
        }

        public int X
        {
            get => x;
            set
            {
                var oldX = x;
                x = value;
                OnValueChanged(new FancyEventArgs(oldX, x, "X"));
            }
        }

        public int Y
        {
            get => y;
            set
            {
                var oldY = y;
                y = value;
                OnValueChanged(new FancyEventArgs(oldY, y, "Y"));
            }
        }

        public event EventHandler ValueChanged;

        protected void OnValueChanged(EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }
    }
}
