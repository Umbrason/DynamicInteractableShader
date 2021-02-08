using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class InteractionAgent : MonoBehaviour
{
    public Material material;

    public void OnEnable()
    {
        if (ShaderInteractor.PositionDictionary.TryGetValue(material, out Dictionary<GameObject, Vector3> posDict))
            if (!posDict.ContainsKey(gameObject))
                posDict.Add(gameObject, transform.position);
    }

    public void OnDisable()
    {
        if (ShaderInteractor.PositionDictionary.TryGetValue(material, out Dictionary<GameObject, Vector3> posDict))
            if (posDict.ContainsKey(gameObject))
                posDict.Remove(gameObject);
    }
    public void Update()
    {
        if (ShaderInteractor.PositionDictionary.TryGetValue(material, out Dictionary<GameObject, Vector3> posDict))
        {
            if (posDict.ContainsKey(gameObject))
            {
                posDict[gameObject] = transform.position;
            }
            else posDict.Add(gameObject, transform.position);
        }
    }
}
