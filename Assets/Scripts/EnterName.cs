using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterName : MonoBehaviour 
{
    // Start is called before the first frame update
    InputField inp;
    void OnEnable()
    {
        inp = GameObject.Find("NameInp").GetComponent<InputField>();
        inp.text  = UserData.UserName;       
    }
    void OnDisable()
    {
        UserData.UserName = inp.text == "" ? null : inp.text;
    }


}
