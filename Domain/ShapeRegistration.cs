using System.Globalization;
using WSCADViewer.Domain.Primitives;
using WSCADViewer.Domain.Registries;
using WSCADViewer.Domain.Util;

namespace WSCADViewer.Domain
{
    public static class ShapeRegistration
    {
        public static void RegisterAll()
        {
            PrimitiveRegistry.Register("line", dto =>
                new LineShape(
                    Parsers.ParsePoint(dto.Attributes["a"]!),
                    Parsers.ParsePoint(dto.Attributes["b"]!),
                    Parsers.ParseColor(dto.Attributes["color"]!)
                )
            );
            PrimitiveRegistry.Register("circle", dto =>
                new CircleShape(
                    Parsers.ParsePoint(dto.Attributes["center"]!),
                    double.Parse(dto.Attributes["radius"]!, CultureInfo.InvariantCulture),
                    Parsers.ParseColor(dto.Attributes["color"]!),
                    bool.Parse(dto.Attributes["filled"]!)
                )
            );
            PrimitiveRegistry.Register("triangle", dto =>
                new TriangleShape(
                    Parsers.ParsePoint(dto.Attributes["a"]!),
                    Parsers.ParsePoint(dto.Attributes["b"]!),
                    Parsers.ParsePoint(dto.Attributes["c"]!),
                    Parsers.ParseColor(dto.Attributes["color"]!),
                    bool.Parse(dto.Attributes["filled"]!)
                )
            );
        }
    }
}
