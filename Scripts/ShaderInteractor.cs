using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class ShaderInteractor : MonoBehaviour
{
    public Material material;
    private Texture2D oldTex;
    public static Dictionary<Material, Dictionary<int, Dictionary<GameObject, Vector4>>> PositionDictionary = new Dictionary<Material, Dictionary<int, Dictionary<GameObject, Vector4>>>();

    void OnEnable()
    {
        if (!ValidateMaterial())
        {
            enabled = false;
            return;
        }
        UnityEngine.Rendering.RenderPipelineManager.beginCameraRendering += OnCameraRender;
        if (!PositionDictionary.ContainsKey(material))
            PositionDictionary.Add(material, new Dictionary<int, Dictionary<GameObject, Vector4>>());
    }
    void OnDisable()
    {
        UnityEngine.Rendering.RenderPipelineManager.beginCameraRendering -= OnCameraRender;
        if (PositionDictionary.ContainsKey(material))
            PositionDictionary.Remove(material);
    }

    private bool ValidateMaterial()
    {
        bool HasTextureProperty = material.HasProperty("_InteractorPositions");
        bool HasCountProperty = material.HasProperty("_Interactors");
        bool HasChannelCountProperty = material.HasProperty("_ChannelCount");
        return material != null || HasTextureProperty || HasCountProperty;
    }

    public void OnCameraRender(UnityEngine.Rendering.ScriptableRenderContext context, Camera cam)
    {
        if (PositionDictionary.TryGetValue(material, out Dictionary<int, Dictionary<GameObject, Vector4>> posDict))
        {
            int maxInteractors = 1;
            int maxChannel = 1;
            foreach (int key in posDict.Keys)
            {
                maxInteractors = Mathf.Max(posDict[key].Count, maxInteractors);
                maxChannel = Mathf.Max(key + 1, maxChannel);
            }

            Texture2D texture2D = oldTex;
            if (oldTex == null || oldTex.width != maxInteractors || oldTex.height != maxChannel)
            {
                texture2D = new Texture2D(maxInteractors, posDict.Count, TextureFormat.RGBAFloat, 0, true);
                material.SetTexture("_InteractorPositions", texture2D);
                material.SetFloat("_Interactors", maxInteractors);
                material.SetFloat("_ChannelCount", maxChannel);
            }
            if (texture2D != oldTex && oldTex != null)
                DestroyImmediate(oldTex);

            foreach (int key in posDict.Keys)
            {
                Vector4[] positions = posDict[key].Values.ToArray();
                oldTex = texture2D;
                for (int j = 0; j < positions.Length; j++)
                    texture2D.SetPixel(j, key, positions[j] - (Vector4)cam.transform.position);
                texture2D.Apply();
            }
        }
        else PositionDictionary.Add(material, new Dictionary<int, Dictionary<GameObject, Vector4>>());
    }

}
