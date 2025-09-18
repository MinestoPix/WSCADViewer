using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using WSCADViewer.Domain.Loaders;
using WSCADViewer.Domain.Primitives;

namespace WSCADViewer.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _message = "Hello, World!";
        public event EventHandler<Rect>? BoundingBoxChanged;

        public ObservableCollection<Shape?> RenderedShapes { get; } = new ObservableCollection<Shape?>();
        public List<IPrimitive> Primitives { get; set; } = new List<IPrimitive>();
        private TransformGroup _renderTransform;
        public TransformGroup RenderTransform
        {
            get => _renderTransform;
            set
            {
                if (_renderTransform != value)
                {
                    _renderTransform = value;
                    OnPropertyChanged(nameof(RenderTransform));
                }
            }
        }
        //public Transform RenderTransformFlipY { get; } = new ScaleTransform(1, -1);

        public string Message
        {
            get => _message;
            set
            {
                if (_message != value)
                {
                    _message = value;
                    OnPropertyChanged(nameof(Message));
                }
            }
        }

        public ICommand LoadFileCommand { get; }
        public ICommand ClickCanvasCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void OnLoadFile(object parameter)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "JSON Files (*.json)|*.json|XML Files(*.xml)|*.xml|All Files (*.*)|*.*",
                Title = "Select a Shapes File"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    using var stream = openFileDialog.OpenFile();
                    var loader = PrimitiveLoaderFactory.Create(System.IO.Path.GetExtension(openFileDialog.FileName));
                    var shapes = loader.LoadPrimitives(stream).ToList();  // must convert to list to access primitives later, e.g. for inspection
                    Primitives = shapes;
                    RenderedShapes.Clear();
                    Rect boundingBox = Rect.Empty;
                    foreach (var shape in shapes)
                    {
                        boundingBox = AddBoundingBoxes(boundingBox, shape.BoundingBox());
                        RenderedShapes.Add(shape.ToWpfShape());
                    }
                    BoundingBoxChanged?.Invoke(this, boundingBox);
                    Message = $"Loaded {RenderedShapes.Count} shapes from {openFileDialog.FileName}";
                }
                catch (Exception ex)
                {
                    Message = $"Error loading file: {ex.Message}";
                    Debug.WriteLine(Message);
                }
            }
        }

        public void OnClickCanvas(object parameter)
        {
            if (parameter is Point p)
            {
                Message = $"Canvas clicked at: {p}";
            }
        }

        private static Rect AddBoundingBoxes(Rect a, Rect b)
        {
            double minX = Math.Min(a.Left, b.Left);
            double minY = Math.Min(a.Top, b.Top);
            double maxX = Math.Max(a.Right, b.Right);
            double maxY = Math.Max(a.Bottom, b.Bottom);
            return new Rect(new Point(minX, minY), new Point(maxX, maxY));
        }

        public MainWindowViewModel()
        {
            LoadFileCommand = new RelayCommand(OnLoadFile);
            ClickCanvasCommand = new RelayCommand(OnClickCanvas);
            _renderTransform = new TransformGroup();
            _renderTransform.Children.Add(new ScaleTransform(1, -1));
            _renderTransform.Children.Add(new TranslateTransform(50, 50));
            var myLineModel = new LineShape(a: new Point(0, 0), b: new Point(150, 0), color: Colors.Goldenrod);
            var myVertLineModel = new LineShape(a: new Point(0, 0), b: new Point(0, 150), color: Colors.Goldenrod);
            var myCircleModel = new CircleShape(center: new Point(200, 200), radius: 50, color: Colors.Red, filled: true);
            RenderedShapes.Add(myCircleModel.ToWpfShape());
            RenderedShapes.Add(myLineModel.ToWpfShape());
            RenderedShapes.Add(myVertLineModel.ToWpfShape());
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool>? _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }
        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter!) ?? true;

        public void Execute(object? parameter) => _execute(parameter!);

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
