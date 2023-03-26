using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI.MessageBox;

namespace BM.MeshGenerator
{
    // MeshGenerator에 의해 생성된 Mesh를 MeshFillter및 MeshRenderer에 적용하는 역활을 합니다.
    public class HexagonalTest : MonoBehaviour
    {
        [SerializeField] private MeshFilter m_meshFilter;
        private Model m_model;
        private Mesh m_mesh;

        private void OnValidate()
        {
            Model model = new Model();

            var icosahedron = Icosahedron.GetIcosahedron();
            model.AddPolygons(icosahedron.vertices, icosahedron.triangles);

            //var icosahedron = meshGenerator.CreateIcosahedron();
            //var subdividiedIcosahedron = meshGenerator.SubdivideMesh(icosahedron);
            //// TODO subdivideMesh에서 파라메터로 몇번 나눌 수 있게 할지 해야함
            //subdividiedIcosahedron = meshGenerator.SubdivideMesh(subdividiedIcosahedron);
            //subdividiedIcosahedron.normalizeSphere(Vector3.zero);
            ////subdividiedIcosahedron.RecalculateNormals();
            //var model = meshGenerator.MakeModel(subdividiedIcosahedron);

            var meshGenerator = new MeshGenerator();
            var newMesh = meshGenerator.MakeMesh(model);

            m_meshFilter.mesh = newMesh;
            m_model = model;
            m_mesh = newMesh;
        }

#if UNITY_EDITOR
        const float GIZMO_SPHERE_SIZE = 0.01f;

        private void OnDrawGizmosSelected()
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.white;

            for (int index = 0; index < m_model.Vertices.Count; index++)
            {
                // 기즈모를 그리고 텍스트를 작성합니다.
                Vector3 worldPosition = transform.TransformPoint(m_model.Vertices[index].Position);
                Gizmos.DrawSphere(worldPosition, GIZMO_SPHERE_SIZE);
                Vector3 worldTextPosition = worldPosition + new Vector3(GIZMO_SPHERE_SIZE, 0.0f, 0.0f);
                Handles.Label(worldTextPosition, $"index {index}", style);
            }
        }
#endif
    }
}