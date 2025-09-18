using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WSCADViewer.Domain.Primitives
{
    public class TriangleShape : BaseShape
    {
        public Point A { get; }
        public Point B { get; }
        public Point C { get; }

        public override Color ShapeColor { get; }

        public override bool Filled { get; }

        public TriangleShape(Point a, Point b, Point c, Color color, bool filled)
        {
            A = a;
            B = b;
            C = c;
            ShapeColor = color;
            Filled = filled;
        }

        public override Shape ToWpfShape()
        {
            var triangle = new Polygon
            {
                Points = new PointCollection { A, B, C },
                Stroke = new SolidColorBrush(ShapeColor),
                StrokeThickness = BorderWidth,
            };
            if (Filled)
            {
                triangle.Fill = new SolidColorBrush(ShapeColor);
            }
            return triangle;
        }

        public override Rect BoundingBox()
        {
            double minX = Math.Min(A.X, Math.Min(B.X, C.X));
            double minY = Math.Min(A.Y, Math.Min(B.Y, C.Y));
            double maxX = Math.Max(A.X, Math.Max(B.X, C.X));
            double maxY = Math.Max(A.Y, Math.Max(B.Y, C.Y));
            return new Rect(new Point(minX, minY), new Point(maxX, maxY));
        }

        public override bool HitTest(Point p) 
        {
            double area = 0.5 * (-B.Y * C.X + A.Y * (-B.X + C.X) + A.X * (B.Y - C.Y) + B.X * C.Y);
            double s = 1 / (2 * area) * (A.Y * C.X - A.X * C.Y + (C.Y - A.Y) * p.X + (A.X - C.X) * p.Y);
            double t = 1 / (2 * area) * (A.X * B.Y - A.Y * B.X + (A.Y - B.Y) * p.X + (B.X - A.X) * p.Y);
            return s >= 0 && t >= 0 && (s + t) <= 1;
        }
    }
}
