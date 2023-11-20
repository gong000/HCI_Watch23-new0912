using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Trigger : MonoBehaviour
{   
    bool cube_active=false;
    public GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // private void OnTriggerEnter(Collider other){

    // }
    public void setCube(){
        cube_active=!cube_active;
        cube.SetActive(cube_active);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
