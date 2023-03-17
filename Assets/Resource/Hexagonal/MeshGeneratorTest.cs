using UnityEngine;

namespace BM.MeshGenerator
{
    [ExecuteAlways]
    public class MeshGeneratorTest : MonoBehaviour
    {
        public MeshFilter meshFilter;

        void Start()
        {
            if (meshFilter == null)
                return;

            var squareMeshGenerator = new SquareMeshGenerator();
            meshFilter.mesh = squareMeshGenerator.Generate();
        }
    }
}