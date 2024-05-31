using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineBoxColliders : MonoBehaviour
{
    void Start()
    {
        BoxCollider[] boxColliders = GetComponentsInChildren<BoxCollider>();

        if (boxColliders.Length == 0)
        {
            Debug.LogWarning("No BoxColliders found in children.");
            return;
        }

        CombineInstance[] combine = new CombineInstance[boxColliders.Length];

        for (int i = 0; i < boxColliders.Length; i++)
        {
            Mesh boxMesh = BoxColliderToMesh(boxColliders[i]);
            combine[i].mesh = boxMesh;
            combine[i].transform = boxColliders[i].transform.localToWorldMatrix;
            boxColliders[i].enabled = false; // Disable original box colliders
        }

        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combine);

        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = combinedMesh;

        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = combinedMesh;
    }

    Mesh BoxColliderToMesh(BoxCollider boxCollider)
    {
        Mesh mesh = new Mesh();

        Vector3 size = boxCollider.size;
        Vector3 center = boxCollider.center;

        Vector3[] vertices = new Vector3[8];
        vertices[0] = new Vector3(-size.x, -size.y, -size.z) * 0.5f + center;
        vertices[1] = new Vector3(size.x, -size.y, -size.z) * 0.5f + center;
        vertices[2] = new Vector3(size.x, -size.y, size.z) * 0.5f + center;
        vertices[3] = new Vector3(-size.x, -size.y, size.z) * 0.5f + center;
        vertices[4] = new Vector3(-size.x, size.y, -size.z) * 0.5f + center;
        vertices[5] = new Vector3(size.x, size.y, -size.z) * 0.5f + center;
        vertices[6] = new Vector3(size.x, size.y, size.z) * 0.5f + center;
        vertices[7] = new Vector3(-size.x, size.y, size.z) * 0.5f + center;

        int[] triangles = new int[]
        {
            0, 2, 1, 0, 3, 2,
            2, 3, 6, 3, 7, 6,
            0, 7, 3, 0, 4, 7,
            1, 6, 5, 1, 2, 6,
            4, 5, 7, 5, 6, 7,
            0, 1, 5, 0, 5, 4
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }
}
