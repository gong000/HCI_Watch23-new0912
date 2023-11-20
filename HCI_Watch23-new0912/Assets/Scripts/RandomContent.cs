using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Text;
public class RandomContent : MonoBehaviour
{
    public GameObject phoneIcon;
    //public GameObject watchIcon;
    public GameObject swimIcon;
    //public GameObject walkIcon;
    public GameObject music;
    public TMP_Text time1;
    public TMP_Text phonecall;
    public TMP_Text airquaility;
    public static RandomContent instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
    public string randomPhone()
    {
        if (Random.Range(0, 2) == 1)
        {
            phoneIcon.SetActive(true);
            return "yesPhone";
            //Debug.Log(1);
        }
        else
        {
            phoneIcon.SetActive(false);
            return "noPhone";
            //Debug.Log(0);
        }
    }
    /* 
    public string randomWatch()
    {
        if (Random.Range(0, 2) == 1)
        {
            watchIcon.SetActive(true);
            return "yesWatch";
            //Debug.Log(1);
        }
        else
        {
           watchIcon.SetActive(false);
            return "noWatch";
            //Debug.Log(0);
        }
    }
    */
    public string randomMusic()
    {
        string[] options = { "Butter", "Spicy","ETA" };// Set the button's string text to either "Mom" or "Dad"
        music.GetComponent<TMP_Text>().text=options[Random.Range(0, options.Length)];
        return music.GetComponent<TMP_Text>().text;
    }
    public string randomSwim()
    {
        if (Random.Range(0, 2) == 1)
        {
            swimIcon.SetActive(true);
            return "yesSwim";
            //Debug.Log(1);
        }
        else
        {
            swimIcon.SetActive(false);
            return "noSwim";
            //Debug.Log(0);
        }
    }
    /*
    public string randomWalk()
    {
        if (Random.Range(0, 2) == 1)
        {
            walkIcon.SetActive(true);
            return "yesWalk";
            //Debug.Log(1);
        }
        else
        {
            walkIcon.SetActive(false);
            return "noWalk";
            //Debug.Log(0);
        }
    }
    */
    
    public string randomCall()
    {
        string[] options = { "Jisu", "Mark" };// Set the button's string text to either "Mom" or "Dad"
        phonecall.text = options[Random.Range(0, options.Length)];
        return phonecall.text;
    }
    public string randomAir()
    {
        string[] options = { "Good","Bad" };// Set the button's string text to either "Mom" or "Dad"
        airquaility.text = options[Random.Range(0, options.Length)];
        return airquaility.text;
    }
    public string randomTime()
    {
        string[] options = { "1:00", "7:00", "12:00" };// Set the button's string text to either "Mom" or "Dad"
        time1.text = options[Random.Range(0, options.Length)];
        return time1.text;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
