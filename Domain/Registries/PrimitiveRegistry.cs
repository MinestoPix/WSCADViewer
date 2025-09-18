using System;
using System.Collections.Generic;
using WSCADViewer.Domain.DTOs;
using WSCADViewer.Domain.Primitives;

namespace WSCADViewer.Domain.Registries
{
    public static class PrimitiveRegistry
    {
        private static readonly Dictionary<string, Func<ShapeDto, IPrimitive>> _creators = new();

        public static void Register(string type, Func<ShapeDto, IPrimitive> creator)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Type cannot be null or whitespace.", nameof(type));
            _creators[type] = creator ?? throw new ArgumentNullException(nameof(creator));
        }

        public static IPrimitive Create(ShapeDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            if (!_creators.TryGetValue(dto.Type, out var creator))
                throw new NotSupportedException($"Shape type '{dto.Type}' is not supported.");
            return creator(dto);
        }
    }
}
