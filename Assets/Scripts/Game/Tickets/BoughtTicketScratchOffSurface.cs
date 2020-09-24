using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoughtTicketScratchOffSurface : MonoBehaviour
{
    // Private

    private const string MaterialMaskTextureCreated = "PrototypeTicketShader_MaskTextureCreated";
    private const string MaterialMaskTextureSource = "PrototypeTicketShader_MaskTextureSource";

    // Exposed

    [ReadOnly]
    [Tooltip("Current position of the mouse")]
    public Vector3? ScratchOffMousePosition;

    [Tooltip("Current position of the ray")]
    public RaycastHit? ScratchOffRaycastHit;

    public bool IsScratchingStarted
    {
        get
        {
            return ScratchOffMousePosition.HasValue;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void StartScratching(Vector3 mousePosition, RaycastHit raycastHit)
    {
        ScratchOffMousePosition = mousePosition;
        ScratchOffRaycastHit = raycastHit;

        ScratchOff(ScratchOffRaycastHit.Value);
    }

    internal void UpdateScratching(Vector3 mousePosition, RaycastHit raycastHit)
    {
        ScratchOffMousePosition = mousePosition;
        ScratchOffRaycastHit = raycastHit;

        ScratchOff(ScratchOffRaycastHit.Value);
    }

    internal void StopScratching()
    {
        ScratchOffMousePosition = null;
        ScratchOffRaycastHit = null;
    }


    private Texture2D GetTextureToModify(RaycastHit hit)
    {
        var rend = hit.transform.GetComponent<Renderer>();

        if (rend == null || rend.material == null)
        {
            Debug.Log($"No renderer detected!");
        }

        var isMaskTextureCreated = rend.material.GetFloat(MaterialMaskTextureCreated);
        var textureToModify = (Texture2D)rend.material.GetTexture(MaterialMaskTextureSource);

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

                rend.material.SetTexture(MaterialMaskTextureSource, newTextureToModify);

                rend.material.SetFloat(MaterialMaskTextureCreated, 1.0f);

                textureToModify = newTextureToModify;
            }
        }

        return textureToModify;
    }

    private void WritePixels(Vector2 position, Texture2D texture)
    {
        var colorToSet = new Color(1, 1, 1, 0);
        var colorBefore = texture.GetPixel((int)position.x, (int)position.y);

        Debug.Log($"SET ({texture.updateCount}) {(int)position.x}, {(int)position.y} {colorBefore} -> {colorToSet}");

        var colors = new Color[100 * 100];
        for (var i = 0; i < 100 * 100; i++)
        {
            colors[i] = colorToSet;
        }

        texture.SetPixels((int)position.x, (int)position.y, 100, 100, colors);
        texture.Apply();

        var getPixel = texture.GetPixel((int)position.x, (int)position.y);
        Debug.Log($"GET ({texture.updateCount}) {(int)position.x}, {(int)position.y} = {getPixel}");
    }

    public void ScratchOff(RaycastHit hit)
    {
        MeshCollider meshCollider = hit.collider as MeshCollider;

        if (meshCollider == null)
        {
            // No collision detected
            return;
        }

        var modifiedTexture = GetTextureToModify(hit);

        var pixelUV = hit.textureCoord;
        pixelUV.x *= modifiedTexture.width;
        pixelUV.y *= modifiedTexture.height;

        WritePixels(pixelUV, modifiedTexture);
    }
}
