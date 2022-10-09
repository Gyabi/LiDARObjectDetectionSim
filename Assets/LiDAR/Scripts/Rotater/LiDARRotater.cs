using UnityEngine;
using System.Collections.Generic;

public class LiDARRotater
{
    // 回転方向と360度以外の回転を設定できるように
    // -180から回転を始めるようにする

    /// <summary>
    /// 回転周波数
    /// </summary>
    private int _freq;
    /// <summary>
    /// 回転軸
    /// </summary>
    private Vector3 _axis;

    /// <summary>
    /// 角度分解能
    /// </summary>
    private float _angularResolution;

    /// <summary>
    /// 1秒あたりの回転角
    /// </summary>
    private float angelPerSec;

    public LiDARRotater(int freq, Vector3 axis, float angularResolution)
    {
        this._freq = freq;
        this._axis = axis;
        this._angularResolution = angularResolution;
    }

    /// <summary>
    /// 経過時間からフレーム内で計算しなければならない回転を算出して返却
    /// </summary>
    /// <param name="deltaTime"></param>
    /// <returns></returns>
    public List<Quaternion> GetRotateAngles(float deltaTime)
    {
        List<Quaternion> rotateAngles = new List<Quaternion>();




        return rotateAngles;
    }
}
