using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class DetectVisualizer : MonoBehaviour
{
    [SerializeField, Header("バウンディングボックス")]
    GameObject bboxS;

    [SerializeField, Header("歩行者用マテリアル")]
    Material pedestrianMat;
    [SerializeField, Header("車両用マテリアル")]
    Material carMat;
    [SerializeField, Header("自転車用マテリアル")]
    Material cyclistMat;

    List<GameObject> BBoxes = new List<GameObject>();
    public void CreateBBoxFromCSV(string csvPath, GameObject offsetObj)
    {
        // csvを読み込み
        using(StreamReader sr = new StreamReader(csvPath))
        {
            while(0 <= sr.Peek())
            {
                string[] detectData = sr.ReadLine()?.Split(",");
                if (detectData is null) continue;

                switch(int.Parse(detectData[7]))
                {
                    case 0:
                        this.GenerateBBox(new DetectData(ObjectType.PEDESTRIAN, float.Parse(detectData[0]), float.Parse(detectData[1]), float.Parse(detectData[2]), float.Parse(detectData[3]),
                        float.Parse(detectData[4]), float.Parse(detectData[5]), float.Parse(detectData[6])), offsetObj);
                        break;
                    case 1:
                        this.GenerateBBox(new DetectData(ObjectType.CYCLIST, float.Parse(detectData[0]), float.Parse(detectData[1]), float.Parse(detectData[2]), float.Parse(detectData[3]),
                        float.Parse(detectData[4]), float.Parse(detectData[5]), float.Parse(detectData[6])), offsetObj);
                        break;
                    case 2:
                        this.GenerateBBox(new DetectData(ObjectType.CAR, float.Parse(detectData[0]), float.Parse(detectData[1]), float.Parse(detectData[2]), float.Parse(detectData[3]),
                        float.Parse(detectData[4]), float.Parse(detectData[5]), float.Parse(detectData[6])), offsetObj);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void GenerateBBox(DetectData detectData, GameObject offsetObj)
    {
        // 座標系をUnity座標系に変換
        float[] bboxPosition = PointPillarsConverter.PointPillarsToUnity(new float[]{detectData.objPosX, detectData.objPosY, detectData.objPosZ});
        // LiDARのローカル座標からワールド座標へ変換
        Vector3 worldPosition = new Vector3(bboxPosition[0], bboxPosition[1], bboxPosition[2]);
        worldPosition = offsetObj.transform.TransformPoint(worldPosition);
        // バウンディングボックスを生成
        GameObject bbox = Instantiate(this.bboxS, worldPosition, Quaternion.identity);

        // サイズを変更
        float[] bboxScale = PointPillarsConverter.PointPillarsToUnity(new float[]{detectData.boxLenX, detectData.boxLenY, detectData.boxLenZ});
        bbox.transform.localScale = new Vector3(Mathf.Abs(bboxScale[0]), Mathf.Abs(bboxScale[1]), Mathf.Abs(bboxScale[2]));
        // 回転を調整
        bbox.transform.rotation = offsetObj.transform.rotation;
        bbox.transform.rotation *= Quaternion.AngleAxis(detectData.rot*Mathf.Rad2Deg, offsetObj.transform.up);

        // 矩形の高さ/2だけ沈むので補正
        bbox.transform.position = new Vector3(bbox.transform.position.x,bbox.transform.position.y+Mathf.Abs(bboxScale[1])/2,bbox.transform.position.z);

        // ボックスの色を変更
        switch(detectData.type)
        {
            case ObjectType.CAR:
                bbox.GetComponent<Renderer>().material = this.carMat;
                break;
            case ObjectType.PEDESTRIAN:
                bbox.GetComponent<Renderer>().material = this.pedestrianMat;
                break;
            case ObjectType.CYCLIST:
                bbox.GetComponent<Renderer>().material = this.cyclistMat;
                break;
            default:
                break;
        }
    }
    public void DestroyAllBBox()
    {
        foreach(GameObject obj in this.BBoxes)
        {
            Destroy(obj);
        }
    }
}