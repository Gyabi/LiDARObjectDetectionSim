using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiDARRader
{
    // 毎回正面をとってきてどの方向にRadarを広げるのか計算させる
    // チャネル数の最低を設ける
    private GameObject _origin;
    private Vector3 OriginPosition{get{return this._origin.transform.position;}}

    private Vector3 OriginForwordDirection{get{return this._origin.transform.forward;}}
    /// <summary>
    /// 水平画角
    /// </summary>
    private float _horizontalAngle;

    /// <summary>
    /// チャネル数
    /// </summary>
    private int _channel;

    /// <summary>
    /// 基準角度
    /// </summary>
    private float _defaultAngle;

    /// <summary>
    /// 最大距離
    /// </summary>
    private float _maxDistance;

    /// <summary>
    /// Rayを展開する方向
    /// </summary>
    private Vector3 RayHolAxis = Vector3.right;

    private List<Vector3> rayDirections = new List<Vector3>(); 

    public LiDARRader(GameObject origin, float horizontalAngle, int channel, float defaultAngle, float maxDistance)
    {
        this._origin = origin;
        this._horizontalAngle = horizontalAngle;
        this._channel = channel;
        this._defaultAngle = defaultAngle;
        this._maxDistance = maxDistance;
        
        this.InitRaders();
    }

    /// <summary>
    /// 各Rayの角度を事前に計算しておく
    /// </summary>
    public void InitRaders()
    {
        // // 設定した垂直画角からRayの最下部に当たる角度を取得
        // float min = -this._horizontalAngle/2;

        // // _channelの数だけ実行
        // for(int i=1; i <= this._channel; i++)
        // {
        //     this.rayDirections.Add(Quaternion.AngleAxis(this._defaultAngle+this._horizontalAngle*i/(this._channel+1), this.RayHolAxis)*)
        // }
    }

    public List<float[]> PushRay(Quaternion rotate)
    {
        // rotate分だけ回転させてからRayを放つ


        List<float[]> data = new List<float[]>();



        return data;

    }
}


