using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AssetUtility 
{
    public static string GetSceneAsset(string assetName)
    {
        return string.Format("Assets/Scenes/{0}.unity", assetName);
    }

    public static string GetUIFormAsset(string assetName)
    {
        return string.Format("Assets/Resouces/Prefabs/UI/{0}.prefab", assetName);
    }

    
}
