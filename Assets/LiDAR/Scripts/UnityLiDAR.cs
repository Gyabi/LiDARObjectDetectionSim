using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gyabi.LiDAR
{
    /// <summary>
    /// Unity実装疑似LiDAR
    /// </summary>
    public class UnityLiDAR : MonoBehaviour
    {
        [SerializeField]
        private LiDARShape _liDARShape;

        [SerializeField, Header("回転周波数[Hz]")]
        private int _freq;
        [SerializeField, Header("角度分解能[度]")]
        private float _angularResolution;

        private Vector3 _axis = Vector3.up;
        private LiDARRotater _liDARRotater;

        private void Awake()
        {
            this._liDARRotater = new LiDARRotater(this._freq, this._axis, this._angularResolution);
        }

        private void Update()
        {
            foreach(Quaternion rotate in this._liDARRotater.GetRotateAngles(Time.deltaTime))
            {
                this.gameObject.transform.rotation *= rotate;
            }
        }
        /// <summary>
        /// Inspectorから変更が入ると自動実行される関数
        /// </summary>
        void OnValidate()
        {
            this._liDARShape.SetShape(this.gameObject);
        }
    }
}
