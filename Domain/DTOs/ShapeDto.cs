using System;
using System.Collections.Generic;

namespace WSCADViewer.Domain.DTOs
{
    public class ShapeDto
    {
        public string Type { get; init; }
        public Dictionary<string, string> Attributes { get; init; } = new();

        public ShapeDto(string type, Dictionary<string, string> attributes)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Attributes = attributes ?? throw new ArgumentNullException(nameof(attributes));
        }
    }
}
