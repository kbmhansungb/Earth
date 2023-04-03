using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using ModelGenerator.Geometry;

namespace ModelGenerator
{
    // MeshGenerator에 의해 생성된 Mesh를 MeshFillter및 MeshRenderer에 적용하는 역활을 합니다.
    public class HexagonalTest : MonoBehaviour
    {
        [SerializeField] private MeshFilter m_meshFilter;
        int subdivisionLevel = 0;
        private Model m_model;

        private void OnValidate()
        {
            var icosahedronGenerator = new IcosahedronGenerator();
            m_model = icosahedronGenerator.CreateIcosahedron();

            var subdivisionGenerator = new SubdivisionGenerator();
            m_model = subdivisionGenerator.CreateSubdivision(m_model);
            m_model = subdivisionGenerator.CreateSubdivision(m_model);
            m_model = subdivisionGenerator.CreateSubdivision(m_model);
            m_model = subdivisionGenerator.CreateSubdivision(m_model);

            var meshGenerator = new ModelGenerator();
            m_model.NormalizeSphere(Vector3.zero);

            m_model = icosahedronGenerator.CreatePentaHexagonalSphere(m_model);

            Mesh newMesh = meshGenerator.MakeMesh(m_model);
            newMesh.RecalculateNormals();
            m_meshFilter.mesh = newMesh;
        }
    }
}