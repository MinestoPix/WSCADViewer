using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WSCADViewer.Domain.Primitives
{
    public class LineShape : BaseShape
    {
        public Point A { get; }
        public Point B { get; }

        public override Color ShapeColor { get; }

        public override bool Filled { get; } = false;

        public LineShape(Point a, Point b, Color color)
        {
            A = a;
            B = b;
            ShapeColor = color;
        }

        public override Shape ToWpfShape()
        {
            return new Line
            {
                X1 = A.X,
                Y1 = A.Y,
                X2 = B.X,
                Y2 = B.Y,
                Stroke = new SolidColorBrush(ShapeColor),
                StrokeThickness = BorderWidth,
            };
        }

        public override Rect BoundingBox()
        {
            double minX = Math.Min(A.X, B.X);
            double minY = Math.Min(A.Y, B.Y);
            double maxX = Math.Max(A.X, B.X);
            double maxY = Math.Max(A.Y, B.Y);
            return new Rect(new Point(minX, minY), new Point(maxX, maxY));
        }

        public override bool HitTest(Point p)
        {
            const double toleranceAdd = 1.0; // additional to BorderWidth
            double tolerance = BorderWidth + toleranceAdd;
            Vector ab = B - A;
            double abLengthSquared = ab.LengthSquared;
            if (abLengthSquared == 0)
            {
                return (p - A).Length <= tolerance;
            }
            Vector ap = p - A;
            double t = Vector.Multiply(ap, ab) / abLengthSquared;
            t = Math.Max(0, Math.Min(1, t));
            Point projection = A + t * ab;
            return (p - projection).Length <= tolerance;
        }
    }
}
