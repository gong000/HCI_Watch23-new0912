using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class ShowMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject button1Outline;
    public GameObject button2Outline;
    public GameObject button3Outline;
    public GameObject button4Outline;
    public GameObject button5Outline;
    public GameObject button6Outline;
    public Interactable button1;
    public Interactable button2;
    public Interactable button3;
    public Interactable button4;
    public Interactable button5;
    public Interactable button6;
    public GameObject app1;
    public GameObject app2;
    public GameObject app3;
    public GameObject app4;
    public GameObject app5;
    public GameObject app6;
    int selected = 0;

    void Start()
    {
        transform.localScale = Vector3.zero;
        button1Outline.SetActive(false);
        button2Outline.SetActive(false);
        button3Outline.SetActive(false);
        button4Outline.SetActive(false);
        button5Outline.SetActive(false);
        button6Outline.SetActive(false);
    }

    public void OpenMenu()
    {
// transform.LeanScale(Vector3.one, 0.3f).setEaseInOutQuint();
    }

    public void CloseMenu()
    {
 //transform.LeanScale(Vector3.zero, 0.3f).setEaseInOutQuint();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.O))
        {
            OpenMenu();
            app1.SetActive(false);
            app2.SetActive(false);
            app3.SetActive(false);
            app4.SetActive(false);
            app5.SetActive(false);
            app6.SetActive(false);
        }
        if (Input.GetKey(KeyCode.C))
        {
            CloseMenu();
            selected = 0;
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            button2Outline.SetActive(false);
            button3Outline.SetActive(false);
            button4Outline.SetActive(false);
            button5Outline.SetActive(false);
            button6Outline.SetActive(false);
            button1Outline.SetActive(true);
            selected = 1;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            button1Outline.SetActive(false);
            button3Outline.SetActive(false);
            button4Outline.SetActive(false);
            button5Outline.SetActive(false);
            button6Outline.SetActive(false);
            button2Outline.SetActive(true);
            selected = 2;
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            button1Outline.SetActive(false);
            button2Outline.SetActive(false);
            button4Outline.SetActive(false);
            button5Outline.SetActive(false);
            button6Outline.SetActive(false);
            button3Outline.SetActive(true);
            selected = 3;
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            button1Outline.SetActive(false);
            button2Outline.SetActive(false);
            button3Outline.SetActive(false);
            button5Outline.SetActive(false);
            button6Outline.SetActive(false);
            button4Outline.SetActive(true);
            selected = 4;
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            button1Outline.SetActive(false);
            button2Outline.SetActive(false);
            button3Outline.SetActive(false);
            button4Outline.SetActive(false);
            button6Outline.SetActive(false);
            button5Outline.SetActive(true);
            selected = 5;
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            button1Outline.SetActive(false);
            button2Outline.SetActive(false);
            button3Outline.SetActive(false);
            button4Outline.SetActive(false);
            button5Outline.SetActive(false);
            button6Outline.SetActive(true);
            selected = 6;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (selected == 1)
            {
                button1.TriggerOnClick();
            }
            else if (selected == 2)
            {
                button2.TriggerOnClick();
            }
            else if (selected == 3)
            {
                button3.TriggerOnClick();
            }
            else if (selected == 4)
            {
                button4.TriggerOnClick();
            }
            else if (selected == 5)
            {
                button4.TriggerOnClick();
            }
            else if (selected == 6)
            {
                button4.TriggerOnClick();
            }
        }
    }
}
