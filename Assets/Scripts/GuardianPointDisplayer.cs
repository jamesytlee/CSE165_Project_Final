using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GuardianPointDisplayer : MonoBehaviour
{
    public GameObject sphere;
    public double RDPtolerance;
    public float wallHeight;

    public Material floorMaterial;
    public Material[] wallMaterials;    // array of wall materials
    public int selectedMaterialIndex;

    private GameObject wall;            // reference to generated wall

    void Start()
    {
        DisplayGuardian();
    }

    void Update()
    {
        // Update wall material if necessary
        if (wall != null && wall.GetComponent<MeshRenderer>().sharedMaterial != wallMaterials[selectedMaterialIndex])
        {
            wall.GetComponent<MeshRenderer>().material = wallMaterials[selectedMaterialIndex];
        }
    }

    void DisplayGuardian()
    {
        Vector3[] guardianPoints = OVRManager.boundary.GetGeometry(OVRBoundary.BoundaryType.PlayArea);
        List<Vector3> simplifiedPoints = RDPAlgorithm.DouglasPeuckerReduction(new List<Vector3>(guardianPoints), RDPtolerance);
        CreateMesh(simplifiedPoints, wallHeight);  // 2.0f is the default wall height
        CreateFloor(simplifiedPoints);

        foreach (var position in simplifiedPoints)
        {
            Debug.Log("Position: " + position);
            Instantiate(sphere, position, Quaternion.identity);
        }
    }

    void CreateMesh(List<Vector3> points, float wallHeight)
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[points.Count * 2];
        int[] triangles = new int[(points.Count - 1) * 6];

        for (int i = 0; i < points.Count; i++)
        {
            // Bottom vertex
            vertices[i * 2] = points[i];
            // Top vertex
            vertices[i * 2 + 1] = points[i] + Vector3.up * wallHeight;

            // Skip the last point because it's the same as the first one
            if (i < points.Count - 1)
            {
                // Two triangles forming a quad
                triangles[i * 6] = i * 2;
                triangles[i * 6 + 1] = (i + 1) * 2;
                triangles[i * 6 + 2] = i * 2 + 1;

                triangles[i * 6 + 3] = i * 2 + 1;
                triangles[i * 6 + 4] = (i + 1) * 2;
                triangles[i * 6 + 5] = (i + 1) * 2 + 1;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        // Attach the created mesh to a new MeshFilter component
        wall = new GameObject("Wall");
        MeshFilter meshFilter = wall.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = wall.AddComponent<MeshRenderer>();

        meshFilter.mesh = mesh;
        meshRenderer.material = wallMaterials[selectedMaterialIndex];

        wall.transform.parent = transform;
    }

    void CreateFloor(List<Vector3> points)
    {
        float minX = points.Min(v => v.x);
        float minZ = points.Min(v => v.z);
        float maxX = points.Max(v => v.x);
        float maxZ = points.Max(v => v.z);

        Vector3[] vertices = new Vector3[4]
        {
            new Vector3(minX, 0, minZ),
            new Vector3(minX, 0, maxZ),
            new Vector3(maxX, 0, minZ),
            new Vector3(maxX, 0, maxZ)
        };

        int[] triangles = new int[6]
        {
            0, 2, 1,
            2, 3, 1
        };

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        GameObject floor = new GameObject("Floor");
        MeshFilter meshFilter = floor.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = floor.AddComponent<MeshRenderer>();

        meshFilter.mesh = mesh;
        meshRenderer.material = floorMaterial;

        floor.transform.parent = transform;
    }

    public void ChangeMaterial(int index)
    {
        // Ensure the provided index is within the correct range
        if (index >= 0 && index < wallMaterials.Length)
        {
            selectedMaterialIndex = index;
        }
    }
}