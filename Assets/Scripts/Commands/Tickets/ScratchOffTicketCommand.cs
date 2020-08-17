using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchOffTicketCommand : ICommand
{
    private GameObject _gameObject;
    private RaycastHit _hit;

    public ScratchOffTicketCommand(GameObject gameObject, RaycastHit raycastHit)
    {
        _gameObject = gameObject;
        _hit = raycastHit;
    }

    private Texture2D GetTextureToModify()
    {
        var rend = _hit.transform.GetComponent<Renderer>();

        if (rend == null || rend.material == null)
        {
            Debug.Log($"No renderer detected!");
        }

        var isMaskTextureCreated = rend.material.GetFloat("TestTicketShader_MaskTextureCreated");
        var textureToModify = (Texture2D)rend.material.GetTexture("TestTicketShader_MaskTextureSource");

        if (isMaskTextureCreated != 1.0f)
        {
            // Modify source texture
            Debug.Log($"Creating Modified texture...");

            if (textureToModify == null)
            {
                throw new Exception("No source texture detected!");
            }
            else
            {
                // Create new texture
                var newTextureToModify = new Texture2D(textureToModify.width, textureToModify.height, textureToModify.format, false, false)
                {
                    alphaIsTransparency = textureToModify.alphaIsTransparency,
                    anisoLevel = textureToModify.anisoLevel,
                    filterMode = textureToModify.filterMode,
                    hideFlags = textureToModify.hideFlags,
                    minimumMipmapLevel = textureToModify.minimumMipmapLevel,
                    mipMapBias = textureToModify.mipMapBias,
                    requestedMipmapLevel = textureToModify.requestedMipmapLevel,
                    wrapMode = textureToModify.wrapMode,
                    wrapModeU = textureToModify.wrapModeU,
                    wrapModeV = textureToModify.wrapModeV,
                    wrapModeW = textureToModify.wrapModeW
                };

                var textureData = textureToModify.GetPixelData<Color>(0);
                newTextureToModify.SetPixelData(textureData, 0);

                Debug.Log($"... created");

                rend.material.SetTexture("TestTicketShader_MaskTextureSource", newTextureToModify);

                rend.material.SetFloat("TestTicketShader_MaskTextureCreated", 1.0f);

                textureToModify = newTextureToModify;
            }
        }

        return textureToModify;
    }

    private void WritePixels(Vector2 position, Texture2D texture)
    {
        var colorToSet = new Color(1, 1, 1, 0);
        var colorBefore = texture.GetPixel((int)position.x, (int)position.y);

        Debug.Log($"SET {(int)position.x}, {(int)position.y} {colorBefore} -> {colorToSet}");

        var colors = new Color[100 * 100];
        for (var i = 0; i < 100 * 100; i++)
        {
            colors[i] = colorToSet;
        }

        texture.SetPixels((int)position.x, (int)position.y, 100, 100, colors);
        texture.Apply();

        var getPixel = texture.GetPixel((int)position.x, (int)position.y);
        Debug.Log($"GET {(int)position.x}, {(int)position.y} = {getPixel}");
    }

    public void Execute()
    {
        MeshCollider meshCollider = _hit.collider as MeshCollider;

        if (meshCollider == null)
        {
            // No collision detected
            return;
        }

        var modifiedTexture = GetTextureToModify();

        var pixelUV = _hit.textureCoord;
        pixelUV.x *= modifiedTexture.width;
        pixelUV.y *= modifiedTexture.height;

        WritePixels(pixelUV, modifiedTexture);
    }

    public void Undo()
    {
        throw new NotImplementedException();
    }
}
