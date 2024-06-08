using System.Drawing;

namespace Ched.Drawing
{
    public class DrawingContext
    {
        public Graphics Graphics { get; }
        public ColorProfile ColorProfile { get; }

        public DrawingContext(Graphics g, ColorProfile colorProfile)
        {
            Graphics = g;
            ColorProfile = colorProfile;
        }
    }
}
