using UnityEngine;

namespace BM.MeshGenerator
{
    public class MeshGeneratorTest : MonoBehaviour
    {
        public MeshFilter meshFilter;

        void Start()
        {
            if (meshFilter == null)
                return;

            var meshGenerator = new MeshGenerator();
            meshGenerator.AddIcosahedron();

            meshFilter.mesh = SubdivideMeshGenerator.SubdivideMesh(meshGenerator.Generate());
        }
    }
}