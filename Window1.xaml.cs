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

        private void canDraw_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // See which button was pressed.
            if (e.RightButton == MouseButtonState.Pressed)
            {
                // See if we are drawing a new polygon.
                if (NewPolyline != null)
                {
                    // POINTS = [A, B, C, D, A] => POINTS = [A, B, C, A]
                    if (NewPolyline.Points.Count > 4)
                    {
                        NewPolyline.Points.RemoveAt(NewPolyline.Points.Count - 2);      // Creating the loop
                        NewPolyline.Points[NewPolyline.Points.Count - 2] = e.GetPosition(canDraw); // Refresh the dash polygon  
                        return;
                    }
                    // POINTS = [A, B, C, A] => POINTS = [A, B]
                    if (NewPolyline.Points.Count == 4)
                    {
                        NewPolyline.Points.RemoveAt(NewPolyline.Points.Count - 1);
                    }
                    // POINTS = [A, B] => POINTS = [A]
                    // POINTS = [A] => POINTS = 0
                    NewPolyline.Points.RemoveAt(NewPolyline.Points.Count - 1);
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

                if (NewPolyline.Points.Count > 2)
                {
                    NewPolyline.Points[NewPolyline.Points.Count - 1] = e.GetPosition(canDraw);
                    NewPolyline.Points.Add(NewPolyline.Points[0]);
                }
                else if (NewPolyline.Points.Count == 2)
                {
                    NewPolyline.Points.Add(e.GetPosition(canDraw));
                    NewPolyline.Points.Add(NewPolyline.Points[0]);
                }
                else
                    NewPolyline.Points.Add(e.GetPosition(canDraw));
            }
        }

        private void canDraw_MouseMove(object sender, MouseEventArgs e)
        {
            if (NewPolyline == null || NewPolyline.Points.Count == 0) return;
            if (NewPolyline.Points.Count > 2)
            {
                NewPolyline.Points[NewPolyline.Points.Count - 2] = e.GetPosition(canDraw);
            }
            else
            {
                NewPolyline.Points[NewPolyline.Points.Count - 1] = e.GetPosition(canDraw);
            }
            //Console.WriteLine(String.Format("X:{0}  Y:{1}", e.GetPosition(canDraw).X, e.GetPosition(Owner).Y));
        }
        private void canDraw_MouseLeave(object sender, MouseEventArgs e)
        {
            if (NewPolyline == null || NewPolyline.Points.Count == 0) return;
            NewPolyline.Points.RemoveAt(NewPolyline.Points.Count - 1);
            NewPolyline.Points.RemoveAt(NewPolyline.Points.Count - 1);
            Poly poly = new Poly();
            poly.setPolygon();
            poly.setPoints(NewPolyline.Points);
            canDraw.Children.Add(poly.GetPolygon());
            Console.WriteLine(NewPolyline.Points.Count());
            canDraw.Children.Remove(NewPolyline);
            NewPolyline = null;
            
        }
    }
    class Poly
    {
        private Polygon polygon = null;

        public void setPolygon()
        {
            polygon = new Polygon();
            polygon.Stroke = Brushes.Blue;
            polygon.StrokeThickness = 2;
        }
        public void setPoints(PointCollection points)
        {
            this.polygon.Points = points;
            
        }
        public Polygon GetPolygon()
        {
            return polygon;
        }
    }
}
