using System.Collections.Generic;
using System.IO;
using WSCADViewer.Domain.Primitives;

namespace WSCADViewer.Domain.Loaders
{
    internal interface IPrimitiveLoader
    {
        IEnumerable<IPrimitive> LoadPrimitives(Stream input);
    }
}
