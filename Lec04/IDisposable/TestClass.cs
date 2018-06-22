using System.Drawing;

namespace IDisposable
{
    public class TestClass : System.IDisposable
    {
        private readonly Bitmap image;

        public TestClass()
        {
            image = new Bitmap(100, 100);
        }

        public void Dispose()
        {
            image.Dispose();
        }
    }
}
