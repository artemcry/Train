using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RailsPointer : MonoBehaviour
{
    Player player;
    public GameObject setRailsButton;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        GameObject.Find("GameControllers").GetComponent<SetRailsButton>().setActiveButton();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 v = player.transform.position;
        transform.position = new Vector3((int)Mathf.Round(v.x), 0, (int)Mathf.Round(v.z));
    }
}
