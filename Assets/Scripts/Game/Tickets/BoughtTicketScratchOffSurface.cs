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

    [ReadOnly]
    [Tooltip("Current position of the ray")]
    public RaycastHit? ScratchOffRaycastHit;

    [ReadOnly]
    [Tooltip("Previous uv hit point")]
    public Vector2? uvLastHitPoint;

    [ReadOnly]
    [Tooltip("Current uv hit point")]
    public Vector2? uvCurrentHitPoint;

    [ReadOnly]
    // The more you hold the stronger it gets
    [Tooltip("Strength of the scratch off")]
    public float Strength;

    [Tooltip("Date and time when scratch off started")]
    public DateTime ScratchOffStartTime;

    // TODO - move this somewhere else maybe ?
    [Tooltip("Texture to use to scratching this off")]
    public Texture2D ScratchingTipTexture;

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
        ScratchOffStartTime = DateTime.Now;
        ScratchOffMousePosition = mousePosition;
        ScratchOffRaycastHit = raycastHit;

        uvLastHitPoint = null;
        uvCurrentHitPoint = null;

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

        uvLastHitPoint = null;
        uvCurrentHitPoint = null;

        Strength = 0;
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

    private void WritePixels(float strength, Vector2? positionStart, Vector2? positionEnd, Texture2D texture)
    {
        if( positionStart.HasValue)
        {
            var start = positionStart.Value;
            var end = positionEnd.Value;

            /*
            for (var i = 0; i < 100 * 100; i++)
            {
                colors[i] = ScratchingTipTexture.GetPixel(;
            }
            */

            int textureSize = 128;

            // get current pixels
            var sourceColors = texture.GetPixels((int)end.x, (int)end.y, textureSize, textureSize);
            var targetColors = ScratchingTipTexture.GetPixels(0, 0, textureSize, textureSize);

            for(var x = 0; x < textureSize; x++)
            {
                for(var y = 0; y < textureSize; y++)
                {
                    var sourceColor = sourceColors[x + y * textureSize];
                    var targetColor = targetColors[x + y * textureSize];

                    // Make sure it doesn't go higher than current value, always lower
                    //var newAlpha = Math.Min(sourceColor.a, targetColor.a);
                    var newAlpha = 1.0f - targetColor.a;// (targetColor.a * strength);

                    // Only apply to alpha
                    sourceColors[x + y * textureSize].a = Mathf.Min(sourceColor.a, newAlpha); //Mathf.Lerp(sourceColor.a, newAlpha, strength);
                }
            }

            texture.SetPixels((int)end.x, (int)end.y, textureSize, textureSize, sourceColors);
            texture.Apply();


            /*
            var colorToSet = new Color(1, 1, 1, 0);
            var colorBefore = texture.GetPixel((int)end.x, (int)end.y);

            Debug.Log($"SET ({texture.updateCount}) {(int)end.x}, {(int)end.y} {colorBefore} -> {colorToSet}");

            var colors = new Color[100 * 100];
            for (var i = 0; i < 100 * 100; i++)
            {
                colors[i] = colorToSet;
            }

            texture.SetPixels((int)end.x, (int)end.y, 100, 100, colors);
            texture.Apply();

            var getPixel = texture.GetPixel((int)end.x, (int)end.y);
            Debug.Log($"GET ({texture.updateCount}) {(int)end.x}, {(int)end.y} = {getPixel}");

        */
        }
        // else - when there is no starting position, we skip

        /*
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
        */
    }

    public void ScratchOff(RaycastHit hit)
    {
        MeshCollider meshCollider = hit.collider as MeshCollider;

        if (meshCollider == null)
        {
            // No collision detected
            return;
        }

        uvLastHitPoint = uvCurrentHitPoint;

        var modifiedTexture = GetTextureToModify(hit);
        /*
        uvCurrentHitPoint = hit.textureCoord;
        uvCurrentHitPoint.Value.x = uvCurrentHitPoint.Value.x * modifiedTexture.width;
        uvCurrentHitPoint.Value.y = uvCurrentHitPoint.Value.y * modifiedTexture.height;
        */
        uvCurrentHitPoint = new Vector2(hit.textureCoord.x * modifiedTexture.width, hit.textureCoord.y * modifiedTexture.height);

        // 1 second to reach full strength
        var timeDif = DateTime.Now - ScratchOffStartTime;
        Strength = Mathf.Lerp(0.0f, 1.0f, Mathf.Min((float)timeDif.TotalMilliseconds, 1000));

        WritePixels(Strength, uvLastHitPoint, uvCurrentHitPoint, modifiedTexture);
    }
}
