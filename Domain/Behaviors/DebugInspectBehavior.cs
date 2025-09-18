using System.Diagnostics;
using WSCADViewer.Domain.Primitives;

namespace WSCADViewer.Domain.Behaviors
{
    internal class DebugInspectBehavior : ISelectionBehavior
    {
        public void OnSelect(IPrimitive primitive)
        {
            var type = primitive.GetType();
            var props = type.GetProperties();
            Debug.WriteLine($"Inspecting {type.Name}:");
            foreach (var kv in props)
            {
                Debug.WriteLine($"\t{kv.Name}: {kv.GetValue(primitive)}");
            }
        }
    }
}
