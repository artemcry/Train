using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetRailsButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void setActiveButton()
    {
        transform.Find("SetRails").gameObject.SetActive(true);
    }
    public void onClick()
    {
        GameGrid g = GameObject.Find("Grid").GetComponent<GameGrid>();
        GameObject rp = GameObject.Find("RailsPointer(Clone)");

        if (g.setRails((int)rp.transform.position.x, (int)rp.transform.position.z)) { 
            GameObject.Find("Player").GetComponent<Player>().inHandResource = null;
            Destroy(rp);
            transform.Find("SetRails").gameObject.SetActive(false); 
        }
    }
}
