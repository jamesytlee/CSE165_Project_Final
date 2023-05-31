/**
 * This is an implementation of the Ramer-Douglas-Peucker (RDP) algorithm
 * which simplifies the points in the GuardianPointDisplayer class.
 * The algorithm reduces a curve composed of line segments to a similar curve with fewer points.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RDPAlgorithm
{
    public static List<Vector3> DouglasPeuckerReduction(List<Vector3> Points, double Tolerance)
    {
        if (Points == null || Points.Count < 3)
            return Points;

        int firstPoint = 0;
        int lastPoint = Points.Count - 1;
        List<int> pointIndexsToKeep = new List<int>();

        //Add the first and last index to the keepers
        pointIndexsToKeep.Add(firstPoint);
        pointIndexsToKeep.Add(lastPoint);

        //The first and the last point cannot be the same
        while (Points[firstPoint].Equals(Points[lastPoint]))
            lastPoint--;

        DouglasPeuckerReduction(Points, firstPoint, lastPoint, Tolerance, ref pointIndexsToKeep);

        List<Vector3> returnPoints = new List<Vector3>();
        pointIndexsToKeep.Sort();
        foreach (int index in pointIndexsToKeep)
            returnPoints.Add(Points[index]);

        return returnPoints;
    }

    private static void DouglasPeuckerReduction(List<Vector3> points, int firstPoint, int lastPoint, double tolerance, ref List<int> pointIndexsToKeep)
    {
        double maxDistance = 0;
        int indexFarthest = 0;

        for (int index = firstPoint; index < lastPoint; index++)
        {
            double distance = PerpendicularDistance(points[firstPoint], points[lastPoint], points[index]);
            if (distance > maxDistance)
            {
                maxDistance = distance;
                indexFarthest = index;
            }
        }

        if (maxDistance > tolerance && indexFarthest != 0)
        {
            //Add the largest point that exceeds the tolerance
            pointIndexsToKeep.Add(indexFarthest);

            DouglasPeuckerReduction(points, firstPoint, indexFarthest, tolerance, ref pointIndexsToKeep);
            DouglasPeuckerReduction(points, indexFarthest, lastPoint, tolerance, ref pointIndexsToKeep);
        }
    }

    public static double PerpendicularDistance(Vector3 Point1, Vector3 Point2, Vector3 Point)
    {
        double area = Math.Abs(.5 * (Point1.x * Point2.y + Point2.x * Point.y + Point.x * Point1.y - Point2.x * Point1.y - Point.x * Point2.y - Point1.x * Point.y));
        double bottom = Math.Sqrt(Math.Pow(Point1.x - Point2.x, 2) + Math.Pow(Point1.y - Point2.y, 2));
        double height = area / bottom * 2;

        return height;
    }
}
