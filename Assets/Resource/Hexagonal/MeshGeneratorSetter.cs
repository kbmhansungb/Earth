using UnityEngine;

namespace BM.MeshGenerator
{
    // MeshGenerator에 의해 생성된 Mesh를 MeshFillter및 MeshRenderer에 적용하는 역활을 합니다.
    public class MeshGeneratorSetter : MonoBehaviour
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