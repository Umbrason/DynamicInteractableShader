using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class InteractionAgent : MonoBehaviour
{
    public Material material;
    public float interactionRange = 1f;
    public int channel = 0;

    public void OnEnable()
    {
        if (ShaderInteractor.PositionDictionary.TryGetValue(material, out Dictionary<int, Dictionary<GameObject, Vector4>> channelPosDict) && channelPosDict.TryGetValue(channel, out Dictionary<GameObject, Vector4> posDict))
            if (!posDict.ContainsKey(gameObject))
                posDict.Add(gameObject, transform.position);
    }

    public void OnDisable()
    {
        if (ShaderInteractor.PositionDictionary.TryGetValue(material, out Dictionary<int, Dictionary<GameObject, Vector4>> channelPosDict) && channelPosDict.TryGetValue(channel, out Dictionary<GameObject, Vector4> posDict))
            if (channelPosDict[channel].Count == 1)
                channelPosDict.Remove(channel);
            else if (posDict.ContainsKey(gameObject))
                posDict.Remove(gameObject);
    }
    public void LateUpdate()
    {
        if (ShaderInteractor.PositionDictionary.TryGetValue(material, out Dictionary<int, Dictionary<GameObject, Vector4>> channelPosDict))
            if (channelPosDict.TryGetValue(channel, out Dictionary<GameObject, Vector4> posDict))
            {
                if (posDict.ContainsKey(gameObject))
                {
                    posDict[gameObject] = (Vector4)transform.position + new Vector4(0, 0, 0, interactionRange * (transform.lossyScale.x + transform.lossyScale.y + transform.lossyScale.z) / 3);
                }
                else posDict.Add(gameObject, transform.position);
            }
            else
            {
                channelPosDict.Add(channel, new Dictionary<GameObject, Vector4>());
            }
    }
}
