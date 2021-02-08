using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class ShaderInteractor : MonoBehaviour
{
    public Material material;
    public static Dictionary<Material, Dictionary<GameObject, Vector3>> PositionDictionary = new Dictionary<Material, Dictionary<GameObject, Vector3>>();

    public void Awake()
    {
        if (PositionDictionary.ContainsKey(material))
            return;
        PositionDictionary.Add(material, new Dictionary<GameObject, Vector3>());
    }
    void OnEnable()
    {
        UnityEngine.Rendering.RenderPipelineManager.beginCameraRendering += OnCameraRender;
    }
    void OnDisable()
    {
        UnityEngine.Rendering.RenderPipelineManager.beginCameraRendering -= OnCameraRender;
    }

    public void OnCameraRender(UnityEngine.Rendering.ScriptableRenderContext context,Camera cam)
    {        
        if (PositionDictionary.TryGetValue(material, out Dictionary<GameObject, Vector3> posDict))
        {
            Vector3[] positions = posDict.Values.ToArray();
            Texture2D texture2D = new Texture2D(positions.Length, 1, TextureFormat.RGBAFloat, 0, true);              
            for (int i = 0; i < positions.Length; i++)
            {
                texture2D.SetPixel(i, 0, (Vector4)(positions[i] - cam.transform.position));
            }
            texture2D.Apply();
            material.SetTexture("_InteractorPositions", texture2D);
            material.SetFloat("_Interactors", positions.Length);            
        }
        else PositionDictionary.Add(material, new Dictionary<GameObject, Vector3>());
    }

}
