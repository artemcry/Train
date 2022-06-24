using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    // Start is called before the first frame update
    public enum Type
    {
        Stone,
        Tree,
        Rails
    }
    protected Type type;    
}
