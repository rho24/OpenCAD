using Caliburn.Micro;

namespace OpenCAD.GUI.ViewModels
{
    public abstract class AvalonViewModelBaseBase : PropertyChangedBase
    {
        public virtual string Title { get; set; }
    }
}