using UnityEngine;

namespace BM.MeshGenerator
{
    public class GeneratedMeshRenderer : MonoBehaviour
    {
        public MeshFilter meshFilter;

        void Start()
        {
            if (meshFilter == null)
                return;

            var meshGenerator = new MeshGenerator();
            var icosahedron = meshGenerator.CreateIcosahedron();
            var subdividiedIcosahedron = meshGenerator.SubdivideMesh(icosahedron);

            meshFilter.mesh = subdividiedIcosahedron;
        }
    }
}