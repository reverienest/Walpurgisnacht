using UnityEngine;

public class Util
{
    public static Color ModifyAlpha(Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }
}