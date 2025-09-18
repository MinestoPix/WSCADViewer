using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WSCADViewer.Domain.Primitives
{
    public interface IPrimitive
    {
        Color ShapeColor { get; }
        bool Filled { get; }
        double BorderWidth { get; }
        Shape ToWpfShape();
        Rect BoundingBox();
        bool HitTest(Point p);
    }

}
