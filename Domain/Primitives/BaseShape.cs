using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WSCADViewer.Domain.Primitives
{
    public abstract class BaseShape : IPrimitive
    {
        public abstract Color ShapeColor { get; }
        public abstract bool Filled { get; }
        public double BorderWidth { get; } = 1.0;
        public abstract Shape ToWpfShape();
        public abstract Rect BoundingBox();
        public abstract bool HitTest(Point p);
    }
}
