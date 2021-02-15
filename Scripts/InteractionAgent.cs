using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class InteractionAgent : MonoBehaviour
{
    public Material material;
    public float interactionRange = 1f;

    public void OnEnable()
    {
        if (ShaderInteractor.PositionDictionary.TryGetValue(material, out Dictionary<GameObject, Vector4> posDict))
            if (!posDict.ContainsKey(gameObject))
                posDict.Add(gameObject, transform.position);
    }

    public void OnDisable()
    {
        if (ShaderInteractor.PositionDictionary.TryGetValue(material, out Dictionary<GameObject, Vector4> posDict))
            if (posDict.ContainsKey(gameObject))
                posDict.Remove(gameObject);
    }
    public void LateUpdate()
    {
        if (ShaderInteractor.PositionDictionary.TryGetValue(material, out Dictionary<GameObject, Vector4> posDict))
        {
            if (posDict.ContainsKey(gameObject))
            {
                posDict[gameObject] = (Vector4)transform.position + new Vector4(0, 0, 0, interactionRange);
            }
            else posDict.Add(gameObject, transform.position);
        }
    }
}
