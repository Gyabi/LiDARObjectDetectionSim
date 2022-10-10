using UnityEngine;
using System.IO;

namespace Gyabi.LiDAR
{
    public static class JpegWriter
    {
        public static void SaveRenderTextureToJpg(RenderTexture RenderTextureRef, string saveFilePath)
        {
            Texture2D tex = new Texture2D(RenderTextureRef.width, RenderTextureRef.height, TextureFormat.RGB24, false);
            RenderTexture.active = RenderTextureRef;
            tex.ReadPixels(new Rect(0, 0, RenderTextureRef.width, RenderTextureRef.height), 0, 0);
            tex.Apply();

            // Encode texture into PNG
            byte[] bytes = tex.EncodeToPNG();
            UnityEngine.Object.Destroy(tex);

            //Write to a file in the project folder
            File.WriteAllBytes(saveFilePath, bytes);
        }
    }
}