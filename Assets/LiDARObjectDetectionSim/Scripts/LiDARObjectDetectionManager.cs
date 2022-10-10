using System.Collections.Generic;
using UnityEngine;
using Gyabi.LiDAR;
using System;
/// <summary>
/// 検出と可視化のモードを切り替えシーンを動かすエントリポイント
/// </summary>
public class LiDARObjectDetectionManager : MonoBehaviour
{
    // 動作モードEnum
    public enum Mode
    {
        SENSING,
        VISUALIZATION
    }

    [SerializeField, Header("疑似LiDARオブジェクト")]
    private UnityLiDAR liDAR;

    [SerializeField, Header("モード")]
    private Mode mode=Mode.SENSING;

    [SerializeField, Header("可視化時読み込みファイル")]
    private string detectDataPath = "";

    [SerializeField, Header("可視化用オブジェクト")]
    private DetectVisualizer detectVisualizer;

    private void Start()
    {
        switch(this.mode)
        {
            case Mode.SENSING:
                Debug.Log("スペースキー押下でLiDARを実行してデータを保存します");
                break;
            case Mode.VISUALIZATION:
                // 可視化実行
                this.detectVisualizer.CreateBBoxFromCSV(this.detectDataPath, this.liDAR.gameObject);
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if(this.mode == Mode.SENSING)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                // センシング実行
                List<float[]> datas = this.liDAR.GetLiDARData();
                // センサデータをPointPillarsの座標系に変換
                datas = PointPillarsConverter.UnityToPointPillarsList(datas);
                // データをcsv保存
                DateTime dt = DateTime.Now;
                String name = dt.ToString($"{dt:yyyyMMddHHmmss}");

                String fileName = Application.dataPath + "/StreamingAssets" + "/LiDAR" + "/" + name + ".csv";
                CSVWriter.WriteData(datas, fileName);

                Debug.Log($"LiDARデータを保存しました。{fileName}");
            }
        }
    }
}
