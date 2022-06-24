using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatingItem : MonoBehaviour
{
    public string uname { set { transform.Find("NameImg").GetComponentInChildren<Text>().text = value; } }
    public string score { set { transform.Find("ScoreImg").GetComponentInChildren<Text>().text = value; } }
    void Start()
    {
        transform.SetParent(GameObject.Find("RatingList").transform, false);
    }
}
