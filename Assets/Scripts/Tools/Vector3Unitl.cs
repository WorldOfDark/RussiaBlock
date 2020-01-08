using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Unitl
{
    public static Vector2 GetIntPos(this Vector3 v)
    {
        int x = Mathf.RoundToInt(v.x);
        int y = Mathf.RoundToInt(v.y);
        return new Vector2(x,y);
    }
}
