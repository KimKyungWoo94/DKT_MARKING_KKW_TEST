
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzIna
{
    public static class Geometry
    {
        // Find the point of intersection between
        // the lines p1 --> p2 and p3 --> p4.
        // http://csharphelper.com/blog/2014/08/determine-where-two-lines-intersect-in-c/
        public static void FindIntersection(
            double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4,
            out bool lines_intersect, out bool segments_intersect,
            out double intersection_x, out double intersection_y,
            out double close_x1, out double close_y1, out double close_x2, out double close_y2)
        {
            // Get the segments' parameters.
            double dx12 = x2 - x1;
            double dy12 = y2 - y1;
            double dx34 = x4 - x3;
            double dy34 = y4 - y3;

            // Solve for t1 and t2
            double denominator = (dy12 * dx34 - dx12 * dy34);

            double t1 =  ((x1 - x3) * dy34 + (y3 - y1) * dx34) / denominator;

            if (double.IsInfinity(t1))
            {
                // The lines are parallel (or close enough to it).
                lines_intersect = false;
                segments_intersect = false;
                intersection_x = double.NaN;
                intersection_y = double.NaN;
                close_x1 = double.NaN;
                close_y1 = double.NaN;
                close_x2 = double.NaN;
                close_y2 = double.NaN;
                return;
            }

            lines_intersect = true;

            double t2 = ((x3 - x1) * dy12 + (y1 - y3) * dx12) / -denominator;

            // Find the point of intersection.
            intersection_x = x1 + dx12 * t1;
            intersection_y = y1 + dy12 * t1;

            // The segments intersect if t1 and t2 are between 0 and 1.
            segments_intersect = ((t1 >= 0) && (t1 <= 1) && (t2 >= 0) && (t2 <= 1));

            // Find the closest points on the segments.
            if (t1 < 0)
            {
                t1 = 0;
            }
            else if (t1 > 1)
            {
                t1 = 1;
            }

            if (t2 < 0)
            {
                t2 = 0;
            }
            else if (t2 > 1)
            {
                t2 = 1;
            }

            close_x1 = x1 + dx12 * t1;
            close_y1 = y1 + dy12 * t1;
            close_x2 = x3 + dx34 * t2;
            close_y2 = y3 + dy34 * t2;
        }

        // Find a circle through the three points.
        // http://csharphelper.com/blog/2016/09/draw-a-circle-through-three-points-in-c/
        public static void FindCircle(
            double ax, double ay, double bx, double by, double cx, double cy,
            out double center_x, out double center_y, out double radius)
        {
            // Get the perpendicular bisector of (x1, y1) and (x2, y2).
            double x1 = (bx + ax) / 2;
            double y1 = (by + ay) / 2;
            double dy1 = bx - ax;
            double dx1 = -(by - ay);

            // Get the perpendicular bisector of (x2, y2) and (x3, y3).
            double x2 = (cx + bx) / 2;
            double y2 = (cy + by) / 2;
            double dy2 = cx - bx;
            double dx2 = -(cy - by);

            // See where the lines intersect.
            bool lines_intersect, segments_intersect;
            double intersection_x, intersection_y, close1_x, close1_y, close2_x, close2_y;
            FindIntersection(x1, y1, x1 + dx1, y1 + dy1, x2, y2, x2 + dx2, y2 + dy2, out lines_intersect, out segments_intersect, out intersection_x, out intersection_y, out close1_x, out close1_y, out close2_x, out close2_y);
            if (!lines_intersect)
            {
                MsgBox.Error("The points are colinear");
                center_x = 0;
                center_y = 0;
                radius = 0;
            }
            else
            {
                center_x = intersection_x;
                center_y = intersection_y;
                double dx = center_x - ax;
                double dy = center_y - ay;
                radius = (double)Math.Sqrt(dx * dx + dy * dy);
            }
        }
    }
}
