using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Level : MonoBehaviour
{
    ////////////TO DO///////////////
    //Each Level should calculate their own progress time, the final level should have total time
    //Each Level should have their own button clicked, 
    //Each Level should append their input data to the data vector, the final level should append that data to csv, wrting it
    //Which level should be randomized?
    //Final Level should send signal to watch to end the task process with "off"
    //Should create that tells watch to be off when IMU is not in right position.
    public int answer;
    //public Material selected;
    //public Material unselected;
    List<Vector3> level_icons;
    int[] map = new int[6];
    //public GameObject[] level_objects;
    List<Vector3> random_icons;
    public int myLevel;
    public float level_time;
    private float start_time;
    bool start_level = false;
    List<int> random_idx = new List<int>(6);
    bool last_level = false;
    bool isWaiting = false;
    //private float end_time;
    //public GameObject level_interface;
    //int current_level=0;
    // Start is called before the first frame update
    public Level()
    {
        level_time = 0;
    }
    void Start()
    {

        this.gameObject.SetActive(false);
        //start_level=true;
        //setIconPosition();
        //randomize_icon();
    }
    int onlyonce = 0;

    float level_Time()
    {
        return Time.time - start_time;
    }                          
}