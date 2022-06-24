using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rails : MonoBehaviour
{
    public enum Type
    {
        Horizontal,
        Vertical,
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight,
        
    }

    public Type type = Type.Horizontal;
    public void setType(Type t)
    {
        // start type - horizontal or Top Right
        type = t;
        switch (type)
        {
            case Type.Vertical:
                transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
            case Type.TopRight:
                transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
            case Type.BottomLeft:
                transform.rotation = Quaternion.Euler(0, 270, 0);
                break;
            case Type.BottomRight:
                transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
            default:
                break;
        }
    }

    void Start()
    {
        
    }
}
