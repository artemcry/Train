using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Rating : MonoBehaviour
{
    public GameObject g;
    void Start()
    {
        UserData.Load();
        RectTransform rc = GameObject.Find("RatingList").GetComponents<RectTransform>()[0];
        RectTransform rtc = GameObject.Find("Rating").GetComponents<RectTransform>()[0];
        var rating = from entry in UserData.Rating orderby entry.Value descending select entry;


        int n = rating.Count();
        if(n * 100 > rtc.sizeDelta.y)
            rc.sizeDelta = new Vector2(rc.sizeDelta.x, n * 100);
        
        rc.transform.position += new Vector3(0, -rc.sizeDelta.y, 0);

        int even = (n % 2 == 0 ? 0 : -1);
        for (int i = n / 2 ; i > 0; i--)
        {
            RatingItem f = Instantiate(g).GetComponent<RatingItem>();
            f.uname = rating.ElementAt(n / 2 - i).Key;
            f.score = rating.ElementAt(n / 2 - i).Value.ToString();

            f.transform.position = new Vector3(f.transform.position.x, i * 100-(50*(even+1)), f.transform.position.z);
            if (i % 2 == 0)
            {
                f.GetComponentInChildren<Image>().color += new Color(0.01f, 0.01f, 0.01f);
                f.transform.Find("ScoreImg").GetComponent<Image>().color += new Color(0.01f, 0.01f, 0.01f); ;
            }

        }
        for (int i = 0; i < n/2 - even; i++)
        {
            RatingItem f = Instantiate(g).GetComponent<RatingItem>();
            f.uname = rating.ElementAt(n / 2 + i).Key;
            f.score = rating.ElementAt(n / 2 + i).Value.ToString();
            f.transform.position = new Vector3(f.transform.position.x, -i * 100-(50 *(even + 1)), f.transform.position.z);
            if (i % 2 == 0)
            {
                f.GetComponentInChildren<Image>().color += new Color(0.01f, 0.01f, 0.01f);
                f.transform.Find("ScoreImg").GetComponent<Image>().color += new Color(0.01f, 0.01f, 0.01f); ;
            }
                
        }
        
    }
    private void OnDestroy()
    {
        UserData.Save();
    }
}
