using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Onclick()
    {
        Debug.Log("On click");
    }

    public void Pressed()
    {
        Debug.Log("Pressed");
    }

    public void Released()
    {
        Debug.Log("Released");
    }

    public void TouchBegin()
    {
        Debug.Log("TouchBegin");
    }
    public void TouchEdn()
    {
        Debug.Log("TouchEnd");
    }
}
