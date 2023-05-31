using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianPointDisplayer : MonoBehaviour
{
    public GameObject sphere;
    public double RDPtolerance;

    void Start()
    {
        DisplayGuardian();
    }

    void DisplayGuardian()
    {
        Vector3[] guardianPoints = OVRManager.boundary.GetGeometry(OVRBoundary.BoundaryType.PlayArea);
        List<Vector3> simplifiedPoints = RDPAlgorithm.DouglasPeuckerReduction(new List<Vector3>(guardianPoints), RDPtolerance);

        foreach (var position in simplifiedPoints)
        {
            Instantiate(sphere, position, Quaternion.identity);
        }
    }
}
