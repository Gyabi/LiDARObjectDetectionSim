using UnityEngine;

[System.Serializable]
public class LiDARShape
{
    [SerializeField, Header("半径[m]")]
    public float radius;
    [SerializeField, Header("高さ[m]")]
    public float height;

    public LiDARShape(float radius, float height)
    {
        this.radius = radius;
        this.height = height;
    }

    public void SetShape(GameObject obj)
    {
        obj.transform.localScale = new Vector3(radius, height/2, radius);
    }
}
