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
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private Polyline NewPolyline = null;
        //private Polygon new_polygon = null;

        private Polygon getPolygon() {
            Polygon polygon = new Polygon();
            polygon.Stroke = Brushes.Blue;
            polygon.StrokeThickness = 0.5;
            return polygon;
        }
        
        private void canDraw_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // See which button was pressed.
            if (e.RightButton == MouseButtonState.Pressed)
            {
                // See if we are drawing a new polygon.
                if (NewPolyline != null)
                {
                    NewPolyline.Points.RemoveAt(NewPolyline.Points.Count - 2);
                    if (NewPolyline.Points.Count > 2)
                    {
                        NewPolyline.Points[NewPolyline.Points.Count - 1] = NewPolyline.Points[NewPolyline.Points.Count - 2];
                    }
                }
                //return;
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
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                
                if (NewPolyline.Points.Count >= 2)
                {
                    NewPolyline.Points[NewPolyline.Points.Count - 1] = e.GetPosition(canDraw);
                    NewPolyline.Points.Add(NewPolyline.Points[0]);
                }
                else
                    NewPolyline.Points.Add(e.GetPosition(canDraw));
            }
                
        }

        private void canDraw_MouseMove(object sender, MouseEventArgs e)
        {
            if (NewPolyline == null) return;
            if (NewPolyline.Points.Count > 2)
            {
                NewPolyline.Points[NewPolyline.Points.Count - 2] = e.GetPosition(canDraw);
            }
            else
            {
                NewPolyline.Points[NewPolyline.Points.Count - 1] = e.GetPosition(canDraw);
            }
            //NewPolyline.Points.Add(NewPolyline.Points[0]);
            //canDraw.Children.Add(NewPolyline);
            // Convert the new polyline into a polygon.
            Polygon polygon = getPolygon();
            
            //polygon.Points = NewPolyline.Points;
            canDraw.Children.Add(polygon);
        }
    }
}
