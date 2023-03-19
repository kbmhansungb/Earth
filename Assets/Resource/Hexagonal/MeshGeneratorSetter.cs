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
            // TODO subdivideMesh에서 파라메터로 몇번 나눌 수 있게 할지 해야함
            subdividiedIcosahedron = meshGenerator.SubdivideMesh(subdividiedIcosahedron);
            subdividiedIcosahedron.normalizeSphere(Vector3.zero);
            subdividiedIcosahedron.RecalculateNormals();

            meshFilter.mesh = subdividiedIcosahedron;
        }
    }
}