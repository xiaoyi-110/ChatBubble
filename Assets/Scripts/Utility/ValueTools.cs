
using UnityEngine;

public static class ValueTools
{
    public static float IntClamp(int value, int min, int max)
    {
        return Mathf.Clamp(value, min, max);
    }
    
}