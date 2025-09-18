using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using WSCADViewer.Domain.Primitives;
using WSCADViewer.Domain.Registries;

namespace WSCADViewer.Domain.Loaders
{
    internal class ShapeDtoPrimitiveLoader : IPrimitiveLoader
    {
        private readonly IShapeDtoLoader _dtoLoader;

        public ShapeDtoPrimitiveLoader(IShapeDtoLoader dtoLoader)
        {
            _dtoLoader = dtoLoader;
        }

        public IEnumerable<IPrimitive> LoadPrimitives(Stream input)
        {
            foreach (var dto in _dtoLoader.Load(input))
            {
                yield return PrimitiveRegistry.Create(dto);
            }
        }

        //public IEnumerable<IPrimitive> LoadPrimitives(Stream input)
        //{
        //    foreach (var dto in _dtoLoader.Load(input))
        //    {
        //        IPrimitive primitive;
        //        try
        //        {
        //            primitive = PrimitiveRegistry.Create(dto);
        //        }
        //        catch (NotSupportedException)
        //        {
        //            Debug.WriteLine($"WARNING: Unsupported shape type: {dto.Type}, skipping.");
        //            continue;
        //        }
        //        yield return primitive;
        //    }
        //}
    }
}
