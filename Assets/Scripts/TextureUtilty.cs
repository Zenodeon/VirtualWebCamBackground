using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureUtilty
{
    public static Texture2D ToTexture2D(this Texture texture)
    {
        Texture2D texture2D = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
        Graphics.CopyTexture(texture, texture2D);
        return texture2D;
    }

    public static Texture2D ToTexture2D(this RenderTexture texture)
    {
        RenderTexture.active = texture;
        Texture2D texture2D = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
        texture2D.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);
        texture2D.Apply();
        return texture2D;
    }

    public static void LayerTexture(Texture2D basetexture, Texture2D topTexture)
    {
        Color[] baseData = basetexture.GetPixels();
        Color[] topData = topTexture.GetPixels();

        int dataLength = topData.Length;
        for (int pi = 0; pi < dataLength; pi++)
        {
            baseData[pi] += topData[pi] / topData[pi].a;
        }

        basetexture.SetPixels(baseData);
        basetexture.Apply();
    }
}
