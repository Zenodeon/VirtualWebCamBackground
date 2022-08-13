using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureUtilty
{
    public static Texture2D ToTexture2D(this Texture texture)
    {
        Texture2D texture2D = new Texture2D(texture.width, texture.height, TextureFormat.ARGB32, false); 
        Graphics.CopyTexture(texture, texture2D);
        return texture2D;
    }
}
