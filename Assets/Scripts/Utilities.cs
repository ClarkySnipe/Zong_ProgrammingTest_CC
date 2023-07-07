using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{

    public static bool IsLeftClicking()
    {
        return Input.GetMouseButton(0);
    }

    public static bool IsRightClicking()
    {
        return Input.GetMouseButton(1);
    }

}

