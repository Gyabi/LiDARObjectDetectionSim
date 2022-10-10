public class DetectData
{
    public ObjectType type;
    public float objPosX;
    public float objPosY;
    public float objPosZ;

    public float boxLenX;
    public float boxLenY;
    public float boxLenZ;

    public float rot;
    public DetectData(ObjectType type, float objPosX, float objPosY, float objPosZ, float boxLenX, float boxLenY, float boxLenZ, float rot)
    {
        this.type = type;

        this.objPosX = objPosX;
        this.objPosY = objPosY;
        this.objPosZ = objPosZ;

        this.boxLenX = boxLenX;
        this.boxLenY = boxLenY;
        this.boxLenZ = boxLenZ;

        this.rot = rot;
    }
}