using System.Collections.Generic;

public class PointPillarsConverter
{
    public static List<float[]> UnityToPointPillarsList(List<float[]> datas)
    {
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

        return datas;
    }

    public static float[] PointPillarsToUnity(float[] data)
    {
        float tmpx = data[0];
        float tmpy = data[1];
        float tmpz = data[2];

        return new float[]{-tmpy, tmpz, tmpx};  
    }
}