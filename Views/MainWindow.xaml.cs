using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using WSCADViewer.Domain.Behaviors;
using WSCADViewer.Domain.Controllers;
using WSCADViewer.ViewModels;

namespace WSCADViewer.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Rect BoundingBox { get; set; } = Rect.Empty;

        public MainWindow()
        {
            InitializeComponent();
            var vm = new MainWindowViewModel();
            DataContext = vm;
            vm.BoundingBoxChanged += MainWindow_BoundingBoxChanged;
            renderCanvas.Loaded += RenderCanvas_SetTransform;
            renderCanvas.SizeChanged += RenderCanvas_SetTransform;
            renderCanvas.MouseLeftButtonDown += RenderCanvas_HandleClick;
        }

        private void MainWindow_BoundingBoxChanged(object? sender, Rect boundingBox)
        {
            BoundingBox = boundingBox;
            RenderCanvas_SetTransform(null, null);
        }

        private void RenderCanvas_SetTransform(object? sender, RoutedEventArgs? e)
        {
            double canvasWidth = renderCanvas.ActualWidth;
            double canvasHeight = renderCanvas.ActualHeight;
            if (BoundingBox.IsEmpty)
            {
                renderCanvas.RenderTransform = new TransformGroup
                {
                    Children =
                    {
                        new ScaleTransform(1, -1),
                        new TranslateTransform(0, canvasHeight)
                    }
                };
                return;
            }
            double scaleX = canvasWidth / BoundingBox.Width;
            double scaleY = canvasHeight / BoundingBox.Height;
            double scale = Math.Min(Math.Min(scaleX, scaleY), 1);
            double canvasOffsetX = Math.Min(BoundingBox.Left, 0);
            double canvasOffsetY = Math.Min(BoundingBox.Top, 0);
            double offsetX = -canvasOffsetX * scale;
            double offsetY = -canvasOffsetY * scale;
            renderCanvas.RenderTransform = new TransformGroup
            {
                Children =
                {
                    new ScaleTransform(scale, -scale),  // inverts Y
                    new TranslateTransform(offsetX, canvasHeight - offsetY)  // try set Y origin to canvasHeight
                }
            };
        }

        private void RenderCanvas_HandleClick(object? sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataContext is MainWindowViewModel vm)
            {
                SelectionController debugSelectionCtrl = new(new DebugInspectBehavior());
                debugSelectionCtrl.HandleClick(e.GetPosition(renderCanvas), vm.Primitives);
                //Point clickPoint = e.GetPosition(renderCanvas);
                //vm.ClickCanvasCommand.Execute(clickPoint);
                //Debug.WriteLine($"Click detected. Coords: {clickPoint}");
            }
        }
    }
}
