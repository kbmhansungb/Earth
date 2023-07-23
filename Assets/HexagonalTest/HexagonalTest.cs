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
        [SerializeField] int subdivisionLevel = 0;
        [SerializeField] float noiseScale = 1.0f;
        [SerializeField] float noiseWeight = 1.0f;
        private Model m_model;

        private void OnValidate()
        {
            m_model = Model.CreateIcosahedron();

            for (int subdivision = 0; subdivision < subdivisionLevel; subdivision++)
            {
                m_model = m_model.CreateSubdivision();
            }

            m_model.NormalizeSphere(Vector3.zero);
            m_model = m_model.CreatePentaHexagonalSphere();
            m_model.AddPerlinNoise(Vector3.zero, noiseScale, noiseWeight);

            Mesh newMesh = m_model.CreateMesh();
            newMesh.RecalculateNormals();
            m_meshFilter.mesh = newMesh;
        }
    }
}