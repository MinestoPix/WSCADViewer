using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WSCADViewer.Domain.Primitives
{
    public class CircleShape : BaseShape
    {
        public Point Center { get; }
        public double Radius { get; }

        public override Color ShapeColor { get; }

        public override bool Filled { get; }

        public CircleShape(Point center, double radius, Color color, bool filled)
        {
            Center = center;
            Radius = radius;
            ShapeColor = color;
            Filled = filled;
        }

        public override Shape ToWpfShape()
        {
            double diameter = 2 * Radius;
            var circle = new Ellipse
            {
                RenderTransform = new TranslateTransform(Center.X - Radius, Center.Y - Radius),
                Width = diameter,
                Height = diameter,
                Stroke = new SolidColorBrush(ShapeColor),
                StrokeThickness = BorderWidth,
            };
            if (Filled)
            {
                circle.Fill = new SolidColorBrush(ShapeColor);
            }
            return circle;
        }

        public override Rect BoundingBox()
        {
            double minX = Center.X - Radius;
            double minY = Center.Y - Radius;
            double maxX = Center.X + Radius;
            double maxY = Center.Y + Radius;
            return new Rect(new Point(minX, minY), new Point(maxX, maxY));
        }

        public override bool HitTest(Point p)
        {
            return p - Center is Vector v && v.Length <= Radius;
        }
    }
}
