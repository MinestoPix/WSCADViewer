using WSCADViewer.Domain.Primitives;

namespace WSCADViewer.Domain.Behaviors
{
    internal interface ISelectionBehavior
    {
        void OnSelect(IPrimitive primitive);
    }
}
