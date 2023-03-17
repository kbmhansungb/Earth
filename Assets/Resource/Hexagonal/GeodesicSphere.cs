using UnityEngine;

[ExecuteAlways]
public class GeodesicSphere : MonoBehaviour
{
    public MeshFilter meshFilter;

    void Start()
    {
        if (meshFilter == null)
            return;

        Mesh mesh = new Mesh();

        Vector3[] vertices = new [] {
             // creating vertices of quad. aligning them in shape of square
             
             new Vector3(0, 0, 0),
             new Vector3(0, 1, 0),
             new Vector3(1, 0, 0),
             new Vector3(1, 1, 0),
         };
         mesh.vertices = vertices;
         
         
         // generate uv
         Vector2[] uv = new [] {
             // generate uv for corresponding vertices also in form of square
             
             new Vector2(0, 0),
             new Vector2(0, 1),
             new Vector2(1, 0),
             new Vector2(1, 1),
         };
         mesh.uv = uv;
         
         Vector3[] normals = new [] {
             // normals same as tris
             -Vector3.forward,
             -Vector3.forward,
             -Vector3.forward,
             -Vector3.forward,
         };
         mesh.normals = normals;
         
         int[] triangles = new [] {
             // tris are viewed as group of three
             // remember to order them in clockwise
             // position of index is not importaint as long as they are in clockwise order
             
             0,1,2,// first tris
             2,1,3// second tris
         };
         mesh.triangles = triangles;

         meshFilter.mesh = mesh;
    }
}
