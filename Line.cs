using System.Drawing;

namespace FractalGenerator
{
    public class Line
    {
        public PointF Start { get; set; }
        public PointF End { get; set; }

        public Line(PointF start, PointF end)
        {
            Start = start;
            End = end;
        }
    }

}
