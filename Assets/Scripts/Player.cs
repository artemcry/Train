using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ResourceContainer = System.Collections.Generic.Dictionary<Resource.Type, int>;




public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 10;
    public ResourceContainer resources;
    public Resource.Type? inHandResource = null;
    private Animator animator;
    private Rigidbody rb;
    public FloatingJoystick joystick;    
    bool f = false;
    void Start()
    {
        resources = Stock.getContainer();
        //resources[Resource.Type.Stone] = 342;
        //resources[Resource.Type.Tree] = 342;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {      
        float v = joystick.Horizontal;
        float h = joystick.Vertical;
        var dirvec = new Vector3(v, 0, h);
        if (Mathf.Abs(dirvec.magnitude) > Mathf.Abs(0.05f))
           transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dirvec), rotationSpeed);
        animator.SetFloat("speed", Vector3.ClampMagnitude(dirvec, 1).magnitude);
        rb.velocity = Vector3.ClampMagnitude(dirvec, 1) * speed;
    }  
}
