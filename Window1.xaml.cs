using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace howto_wpf_draw_polygon
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private Polyline NewPolyline = null;

        private void canDraw_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // See which button was pressed.
            if (e.RightButton == MouseButtonState.Pressed)
            {
                // See if we are drawing a new polygon.
                if (NewPolyline != null)
                {
                    if (NewPolyline.Points.Count > 3)
                    {
                        // Remove the last point.
                        NewPolyline.Points.RemoveAt(NewPolyline.Points.Count - 1);

                        // Convert the new polyline into a polygon.
                        Polygon new_polygon = new Polygon();
                        new_polygon.Stroke = Brushes.Blue;
                        new_polygon.StrokeThickness = 2;
                        new_polygon.Points = NewPolyline.Points;
                        canDraw.Children.Add(new_polygon);
                    }
                    canDraw.Children.Remove(NewPolyline);
                    NewPolyline = null;
                }
                return;
            }

            // If we don't have a new polygon, start one.
            if (NewPolyline == null)
            {
                // We have no new polygon. Start one.
                NewPolyline = new Polyline();
                NewPolyline.Stroke = Brushes.Red;
                NewPolyline.StrokeThickness = 1;
                NewPolyline.StrokeDashArray = new DoubleCollection();
                NewPolyline.StrokeDashArray.Add(5);
                NewPolyline.StrokeDashArray.Add(5);
                NewPolyline.Points.Add(e.GetPosition(canDraw));
                canDraw.Children.Add(NewPolyline);
            }

            // Add a point to the new polygon.
            NewPolyline.Points.Add(e.GetPosition(canDraw));
        }

        private void canDraw_MouseMove(object sender, MouseEventArgs e)
        {
            if (NewPolyline == null) return;
            NewPolyline.Points[NewPolyline.Points.Count - 1] = e.GetPosition(canDraw);
        }
    }
}
