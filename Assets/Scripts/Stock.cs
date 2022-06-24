using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ResourceContainer = System.Collections.Generic.Dictionary<Resource.Type, int>;

public class Stock : Train
{
    private ResourceContainer resources;
    public static ResourceContainer getContainer()
    {
        ResourceContainer res = new ResourceContainer();
        foreach (Resource.Type e in Resource.Type.GetValues(typeof(Resource.Type)))
            res.Add(e, 0);
        return res;
    }

    public Stock()
    {
        resources = getContainer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject g = collision.gameObject;
        if (g.tag != "Player")
            return;
        Player p = g.GetComponent<Player>();

        foreach (Resource.Type e in new Resource.Type[] { Resource.Type.Stone, Resource.Type.Tree })
        {
            resources[e] += p.resources[e];
            p.resources[e] = 0;
        }
        checkStartCreate();
    }
    public void checkStartCreate()
    {       
        Terminal terminal = GameObject.Find("TrainTerminal(Clone)").GetComponent<Terminal>();
        if (terminal.railsDone >= terminal.maxRailsCount)
            return;
        bool startCreate = true;
        ResourceContainer required = terminal.requiredResourcesCount;
        foreach (var req in required.Keys)
        {
            if (resources[req] < required[req])
            {
                startCreate = false;
                break;
            }
        }

        if (startCreate)
        {
            foreach (var req in required.Keys)
                resources[req] -= required[req];
            terminal.strartCreate();
        }
    }
}
