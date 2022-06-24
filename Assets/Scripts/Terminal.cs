using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using ResourceContainer = System.Collections.Generic.Dictionary<Resource.Type, int>;


public class Terminal : Train
{
    public float railsCreateTime = 2.0f;
    public ResourceContainer requiredResourcesCount;
    public int maxRailsCount = 2;

    public bool processed { get; private set; } = false;
    public int railsDone { get; private set; } = 0;
    Stock stock; 

    [SerializeField]
    private GameObject railsPointer;

   
    private void Start()
    {
        stock = GameObject.Find("TrainStock(Clone)").GetComponent<Stock>();
        requiredResourcesCount = new ResourceContainer() {
            { Resource.Type.Stone, 1 },
            { Resource.Type.Tree, 2 }
        };
    }

    public void strartCreate()
    {
        if (processed || railsDone >= maxRailsCount)
            return;
        processed = true;
        Invoke("endCreate", railsCreateTime);
        GetComponent<Animator>().SetBool("RailsProcessed", true);
        
    }
    void endCreate()
    {
        railsDone++;
        processed = false;
        UpdateRailsOnPlatform();
        GetComponent<Animator>().SetBool("RailsProcessed", false);
        stock.checkStartCreate();
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject g = collision.gameObject;
        if(railsDone == 0 || g.tag != "Player")
            return;

        Player p = g.GetComponent<Player>();
        if (p.inHandResource == Resource.Type.Rails)
            return;

        p.inHandResource = Resource.Type.Rails;
        Instantiate(railsPointer);
        railsDone--;

        UpdateRailsOnPlatform();
        stock.checkStartCreate();
    }
    private void UpdateRailsOnPlatform()
    {
        transform.Find($"Rails1").gameObject.SetActive(railsDone > 0);
        transform.Find($"Rails2").gameObject.SetActive(railsDone > 1);
    }
}
