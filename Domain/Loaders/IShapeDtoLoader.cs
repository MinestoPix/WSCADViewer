using System.Collections.Generic;
using System.IO;
using WSCADViewer.Domain.DTOs;

namespace WSCADViewer.Domain.Loaders
{
    public interface IShapeDtoLoader
    {
        public IEnumerable<ShapeDto> Load(Stream input);
    }
}
