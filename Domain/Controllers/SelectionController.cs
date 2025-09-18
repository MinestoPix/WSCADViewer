using System.Collections.Generic;
using System.Windows;
using WSCADViewer.Domain.Behaviors;
using WSCADViewer.Domain.Primitives;

namespace WSCADViewer.Domain.Controllers
{
    internal class SelectionController
    {
        private readonly ISelectionBehavior _behavior;

        public SelectionController(ISelectionBehavior behavior)
        {
            _behavior = behavior;
        }

        public void HandleClick(Point p, IEnumerable<IPrimitive> primitives)
        {
            foreach (var primitive in primitives)
            {
                if (primitive.HitTest(p))
                {
                    _behavior.OnSelect(primitive);
                    break; // Assuming only one primitive can be selected at a time
                }
            }
        }
    }
}
