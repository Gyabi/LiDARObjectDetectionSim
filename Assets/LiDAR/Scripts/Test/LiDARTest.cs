using System.Collections.Generic;
using UnityEngine;
using System;

namespace Gyabi.LiDAR
{
    public class LiDARTest : MonoBehaviour
    {
        [SerializeField]
        Camera MainCamera;

        float maxDistance = 100f;

        int LiDARLines = 64;
        float VAngel = 26.9f;
        // 角度分解能
        float anglePray = 0.1f;
        // float anglePray = 0.08f;
        // float anglePray = 10f;

        private void Start()
        {
            // カメラ画角から何度の範囲を取得するか計算
            float horizontalAngle = this.VerticalToHorizontalFov(this.MainCamera.fieldOfView, this.MainCamera.aspect);
            // 計算結果よりRayCastを実行して随時csvに書き込んでいく
            List<float[]> datas = new List<float[]>();
            // Rayを打って随時点群データを格納
            // 正面ベクトルを取得
            Vector3 forword = this.transform.forward;

            // 角度分解能に合わせてRayを打ち込む垂直方向回転を実施
            for(float angle=-horizontalAngle/2; angle<horizontalAngle/2; angle+=anglePray)
            {
                // 照射方向を計算
                Vector3 direction = Quaternion.AngleAxis(angle, transform.up) * forword;

                // directionの横方向ベクトルを取得しておく
                Vector3 VAxis = Quaternion.AngleAxis(angle, transform.up) * this.transform.right;
                
                // 垂直分解能と垂直画角に合わせてRayを生成し接触点をdataへ書き込む
                for(float angle2=-VAngel/2; angle2<VAngel/2; angle2+=VAngel/LiDARLines)
                {
                    Vector3 direction2 = Quaternion.AngleAxis(angle2, VAxis) * direction;
                    // Debug.DrawRay(this.transform.position, direction2, Color.red, 1f);
                    Ray ray = new Ray(this.transform.position, direction2);
                    RaycastHit hit = new RaycastHit();
                    if(Physics.Raycast(ray, out hit, maxDistance))
                    {
                        datas.Add(new float[]{hit.point.x, hit.point.y, hit.point.z, 0});
                    }
                }
            }
            Debug.Log(datas.Count);

            // デバッグとしてメッシュを作ってみる
            GameObject meshObj = new GameObject();
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

            // 点群の座標をLidar基準へ変換
            foreach(float[] data in datas)
            {
                Vector3 newpoint = this.transform.InverseTransformPoint(new Vector3(data[0], data[1], data[2]));

                data[0] = newpoint.x;
                data[1] = newpoint.y;
                data[2] = newpoint.z;
            }

            // そもそもの座標系も変換
            foreach(float[] data in datas)
            {
                float tmpx = data[0];
                float tmpy = data[1];
                float tmpz = data[2];
                data[0] = tmpz;
                data[1] = -tmpx;
                data[2] = tmpy;
            }
            // 保存するファイル名を決めて出力
            DateTime dt = DateTime.Now;
            String name = dt.ToString($"{dt:yyyyMMddHHmmss}");

            String fileName = Application.dataPath + "/StreamingAssets" + "/LiDAR" + "/" + name + ".csv";
            CSVWriter.WriteData(datas, fileName);


            // カメラ映像の保存
            // キャリブレーションデータの生成
        } 


        private float HorizontalToVerticalFov(float horizontalFov, float aspectRatio)
        {
            return 2f * Mathf.Rad2Deg * Mathf.Atan(Mathf.Tan(horizontalFov * 0.5f * Mathf.Deg2Rad) / aspectRatio);
        }

        private float VerticalToHorizontalFov(float verticalFov, float aspectRatio)
        {
            return 2f * Mathf.Rad2Deg * Mathf.Atan(Mathf.Tan(verticalFov * 0.5f * Mathf.Deg2Rad) * aspectRatio);
        }
    }
}