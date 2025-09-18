using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using WSCADViewer.Domain.DTOs;

namespace WSCADViewer.Domain.Loaders
{
    public class JsonShapeDtoLoader : IShapeDtoLoader
    {

        public IEnumerable<ShapeDto> Load(Stream input)
        {
            var jsonDocument = JsonDocument.Parse(input);
            var rootElement = jsonDocument.RootElement;

            foreach (var shapeElement in rootElement.EnumerateArray())
            {
                var type = shapeElement.GetProperty("type").GetString() ?? throw new InvalidDataException("Shape type is missing.");
                var attributes = shapeElement
                    .EnumerateObject()
                    .Where(p => p.Name != "type")
                    .ToDictionary(
                        p => p.Name,
                        p => p.Value.ToString()
                    );

                yield return new ShapeDto(type, attributes);
            }
        }
    }
}
