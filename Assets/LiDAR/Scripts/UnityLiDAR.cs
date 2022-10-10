using System.Collections.Generic;
using UnityEngine;

namespace Gyabi.LiDAR
{
    /// <summary>
    /// Unity実装疑似LiDAR
    /// </summary>
    public class UnityLiDAR : MonoBehaviour
    {
        [SerializeField, Header("Rayの最長距離")]
        private float _maxDistance = 100f;
        [SerializeField, Header("水平画角")]
        private float _horizontalAngle=114f;

        [SerializeField, Header("垂直画角")]
        private float _verticalAngle=26.9f;
        
        [SerializeField, Header("垂直方向分解能（Ray照射数）")]
        private int _lidarLines = 64;

        [SerializeField, Header("水平方向分解能[°]")]
        private float _horizontalResolution = 0.1f;


        /// <summary>
        /// LiDARデータを取得
        /// </summary>
        /// <param name="createMesh">メッシュを生成するかフラグ</param>
        /// <returns></returns>
        public List<float[]> GetLiDARData(bool createMesh=false)
        {
            // 計算結果よりRayCastを実行して随時csvに書き込んでいく
            List<float[]> datas = new List<float[]>();
            // 正面ベクトルを取得
            Vector3 forword = this.transform.forward;
            // 角度分解能に合わせてRayを打ち込む垂直方向回転を実施
            for(float angle=-this._horizontalAngle/2; angle<this._horizontalAngle/2; angle+=this._horizontalResolution)
            {
                // 照射方向を計算
                Vector3 direction = Quaternion.AngleAxis(angle, transform.up) * forword;

                // directionの横方向ベクトルを取得しておく
                Vector3 VAxis = Quaternion.AngleAxis(angle, transform.up) * this.transform.right;
                
                // 垂直分解能と垂直画角に合わせてRayを生成し接触点をdataへ書き込む
                for(float angle2=-this._verticalAngle/2; angle2<this._verticalAngle/2; angle2+=this._verticalAngle/this._lidarLines)
                {
                    Vector3 direction2 = Quaternion.AngleAxis(angle2, VAxis) * direction;
                    Ray ray = new Ray(this.transform.position, direction2);
                    RaycastHit hit = new RaycastHit();
                    if(Physics.Raycast(ray, out hit, this._maxDistance))
                    {
                        datas.Add(new float[]{hit.point.x, hit.point.y, hit.point.z, 0});
                    }
                }
            }

            // メッシュを生成
            if(createMesh) this.CreateMesh(datas);

            // LiDARオブジェクト基準座標へ変換
            foreach(float[] data in datas)
            {
                Vector3 newpoint = this.transform.InverseTransformPoint(new Vector3(data[0], data[1], data[2]));

                data[0] = newpoint.x;
                data[1] = newpoint.y;
                data[2] = newpoint.z;
            }

            return datas;
        }

        public void CreateMesh(List<float[]> datas)
        {
            // デバッグとしてメッシュを作ってみる
            GameObject meshObj = new GameObject();
            // デバッグ用のレイヤを充てる
            meshObj.layer = 6;
            MeshFilter meshFilter = meshObj.AddComponent<MeshFilter>();
            meshObj.AddComponent<MeshRenderer>();

            int pointNum = datas.Count;
            Vector3[] points = new Vector3[pointNum];
            int[] indecies = new int[pointNum];

            for(int i=0; i<pointNum; i++)
            {
                points[i] = new Vector3(datas[i][0],datas[i][1],datas[i][2]);
                indecies[i] = i;
            }

            Mesh mesh = new Mesh{vertices=points};
            mesh.SetIndices(indecies, MeshTopology.Points, 0);
    
            meshFilter.mesh = mesh;
        }
    }

}
