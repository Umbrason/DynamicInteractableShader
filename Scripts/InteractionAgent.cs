using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class InteractionAgent : MonoBehaviour
{
    public Material material;
    public float interactionRange = 1f;
    public int channel = 0;
    private int oldChannel;
    private Material oldMaterial;

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
        CheckForChangedVariables();
        //Get Dictionary by material
        if (ShaderInteractor.PositionDictionary.TryGetValue(material, out Dictionary<int, Dictionary<GameObject, Vector4>> channelPosDict))
        {
            //Get Dictionary by channel ID
            if (channelPosDict.TryGetValue(channel, out Dictionary<GameObject, Vector4> posDict))
                if (posDict.ContainsKey(gameObject))
                    posDict[gameObject] = (Vector4)transform.position + new Vector4(0, 0, 0, interactionRange * (transform.lossyScale.x + transform.lossyScale.y + transform.lossyScale.z) / 3);
                else posDict.Add(gameObject, transform.position);
            else
                channelPosDict.Add(channel, new Dictionary<GameObject, Vector4>());
        }
    }

    private void CheckForChangedVariables()
    {
        if (oldChannel != channel || material != oldMaterial)
        {
            //Get Dictionary by old channel ID and material
            if (oldMaterial != null || oldChannel != null)
                if (ShaderInteractor.PositionDictionary.TryGetValue(oldMaterial, out Dictionary<int, Dictionary<GameObject, Vector4>> channelPosDict) && channelPosDict.TryGetValue(oldChannel, out Dictionary<GameObject, Vector4> posDict))
                    posDict.Remove(gameObject);
            oldChannel = channel;
            oldMaterial = material;
        }
    }
}
