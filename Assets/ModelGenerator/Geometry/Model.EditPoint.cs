using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using ModelGenerator.Geometry;
using System;
using Extension;

namespace ModelGenerator.Geometry
{
    public partial class Model : Shape
    {
        /// <summary>
        /// origin을 기준으로 length만큼의 길이를 가지도록 정규화 합니다.
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="length"></param>
        public void NormalizeSphere(Vector3 origin, float length = 1.0f)
        {
            EachPoint(point => {
                point.Position = origin + (point.Position - origin).normalized * length;
            });
        }

        /// <summary>
        /// 펄린 노이즈를 더합니다.
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="length"></param>
        /// <param name="noizeScale"></param>
        public void AddPerlinNoise(Vector3 origin, float scale = 1.0f, float weight = 1.0f)
        {
            EachPoint(point => {
                point.Position += (point.Position - origin).normalized * Perlin.Noise(point.Position * scale) * weight;
            });
        }

        /// <summary>
        /// 각 점에 대해 지정된 작업을 수행합니다.
        /// </summary>
        public void EachPoint(Action<Point> modifyPoint)
        {
            m_points.ForEach(modifyPoint);
        }

        /// <summary>
        /// 각 점에 회전을 구하여 적용시킵니다.
        /// </summary>
        /// <param name="getQuaternion"></param>
        public void RotateEachPoint(Func<Point, Quaternion> getQuaternionFromPoint)
        {
            EachPoint(point =>
            {
                point.Position = getQuaternionFromPoint(point) * point.Position;
            });
        }

        /// <summary>
        /// 모델을 회전시킵니다.
        /// </summary>
        /// <param name="quaternion">RotateEachPoint의 반환값으로 전달됩니다.</param>
        public void Rotate(Quaternion quaternion)
        {
            RotateEachPoint((point) => { return quaternion; });
        }
    }
} 