using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using WSCADViewer.Domain.DTOs;

namespace WSCADViewer.Domain.Loaders
{
    public class XmlShapeDtoLoader : IShapeDtoLoader
    {

        public IEnumerable<ShapeDto> Load(Stream input)
        {
            var doc = XDocument.Load(input);
            var rootElement = doc.Root ?? throw new InvalidDataException("XML document has no root element.");

            foreach (var shapeElement in rootElement.Elements())
            {
                var type = shapeElement.Name.LocalName;
                var attributes = shapeElement
                    .Elements()
                    .ToDictionary(
                        p => p.Name.LocalName,
                        p => p.Value
                    );

                yield return new ShapeDto(type, attributes);
            }
        }
    }
}
