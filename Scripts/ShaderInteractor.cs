using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class ShaderInteractor : MonoBehaviour
{
    public Material material;
    private Texture2D oldTex;
    public static Dictionary<Material, Dictionary<GameObject, Vector4>> PositionDictionary = new Dictionary<Material, Dictionary<GameObject, Vector4>>();

    public void Awake()
    {
        if (PositionDictionary.ContainsKey(material))
            return;
        PositionDictionary.Add(material, new Dictionary<GameObject, Vector4>());
    }
    void OnEnable()
    {
        UnityEngine.Rendering.RenderPipelineManager.beginCameraRendering += OnCameraRender;
    }
    void OnDisable()
    {
        UnityEngine.Rendering.RenderPipelineManager.beginCameraRendering -= OnCameraRender;
    }

    public void OnCameraRender(UnityEngine.Rendering.ScriptableRenderContext context, Camera cam)
    {
        if (PositionDictionary.TryGetValue(material, out Dictionary<GameObject, Vector4> posDict))
        {
            Vector4[] positions = posDict.Values.ToArray();
            Texture2D texture2D = oldTex;
            if (oldTex == null || oldTex.width != positions.Length)            
                texture2D = new Texture2D(positions.Length, 1, TextureFormat.RGBAFloat, 0, true);
            

            material.SetTexture("_InteractorPositions", texture2D);
            material.SetFloat("_Interactors", positions.Length);
            if (texture2D != oldTex && oldTex != null)
                Destroy(oldTex);
            oldTex = texture2D;
            for (int i = 0; i < positions.Length; i++)
            {
                texture2D.SetPixel(i, 0, positions[i] - (Vector4)cam.transform.position);
            }
            texture2D.Apply();
        }
        else PositionDictionary.Add(material, new Dictionary<GameObject, Vector4>());
    }

}
