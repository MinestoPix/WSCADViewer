using System;

namespace WSCADViewer.Domain.Loaders
{
    internal class PrimitiveLoaderFactory
    {
        public static IPrimitiveLoader Create(string fileExtension)
        {
            return new ShapeDtoPrimitiveLoader(fileExtension.ToLower() switch
            {
                ".json" => new JsonShapeDtoLoader(),
                ".xml" => new XmlShapeDtoLoader(),
                _ => throw new NotSupportedException($"File extension '{fileExtension}' is not supported."),
            });
        }
    }
}
