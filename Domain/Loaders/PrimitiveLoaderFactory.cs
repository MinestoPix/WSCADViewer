using System;

namespace WSCADViewer.Domain.Loaders
{
    internal class PrimitiveLoaderFactory
    {
        public static IPrimitiveLoader Create(string fileExtension)
        {
            return fileExtension.ToLower() switch
            {
                ".json" => new ShapeDtoPrimitiveLoader(new JsonShapeDtoLoader()),
                _ => throw new NotSupportedException($"File extension '{fileExtension}' is not supported."),
            };
        }
    }
}
