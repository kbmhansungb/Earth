using UnityEngine;

public class GeodesicSphere : MonoBehaviour
{
    public MeshFilter meshFilter;

    void Start()
    {
        Mesh mesh = new Mesh();

        // generate vertices
        Vector3[] vertices = new[] {
            // generating three vertices for triangle
            new Vector3(0.0f, 0.0f, 0.0f),
            new Vector3(-1.0f, 1.0f, 0.0f),
            new Vector3(1.0f, 1.0f, 0.0f),
        };
        Vector2[] uv = new[] {
    // generated vertices will be mapped to corresponding coordinates in uv
    new Vector2(0.5f, 0.0f),
    new Vector2(0.0f, 1.0f),
    new Vector2(1.0f, 1.0f),
};
        mesh.uv = uv;
        mesh.vertices = vertices;
    }
}
