using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class LevelManager : MonoBehaviour
{   
    //Level Manager should have what level is into certain task group. -> Should be grouped
    //Level Manager should control the flow of the tasks taken
    //Level Manager should be the one to finally write csv data
    //Level Manager should call end signal to watch after one input group is over?
    // Start is called before the first frame update
    //point : np(cursor_reset), p(moving), 
    //action : off,stb, bts, btap [stbon, btson, btapon](Button selected -> level should start after this button selection), off(level finish -> resets watch to button selection)
    //Appli[] -> select -> follow the hiearchy of the selected gameobject.
    bool stb_mode=false;
    bool bts_mode=false;
    bool btap_mode=false;
    public bool isMain=true;
    public GameObject interface_active;
    public bool watch_active=false;
    public GameObject cursor;
    float cursor_offset_x;
    float cursor_offset_y;
    GameObject before_level;
    public GameObject[] appli_List;
    public GameObject Main_interface;
    GameObject selected_appli;
    //0.Setting 1.Exercise 2.Music 3.App 4.Call 5.Weather
    public int level=0;
    public int current_level=0;
    public List<string> level_data;
    List<Vector3> random_icons;
    List<Vector3> level_icons;
    public bool isLevelOver=true;
    public int max_trial;
    public int trial=0;
    public bool trial_done=false;
    public static LevelManager instance;
    List<int> random_idx=new List<int>(6);
    public int[][] answerPath; // trial, answerpath
    float start_time;
    int[] map=new int[6];
    int onlyonce=0;
    void Start()
    {   
        instance=this;
        trial=0;
        trial_done=false;
        level_data=new List<string>();
        level_icons=new List<Vector3>();
        for(int i=0;i<6;i++){
            map[i]=0;
        }
        for(int i=0;i<6;i++){
            level_icons.Add(new Vector3(0,0,0));
            level_icons[i]=Main_interface.transform.GetChild(i).transform.localPosition;
        }
        answerPath=new int[][]{
            
    //0.Setting 1.Exercise 2.Music 3.App 4.Call 5.Weather
    new int[]{0,0,2},
    new int[]{1,0,5},
    new int[]{0,0,2},
    new int[]{4,2,3},
    new int[]{3,3,2},
    new int[]{5,3,1},
    new int[]{2,1,3},
    new int[]{3,3,2},
    new int[]{4,2,3},
    new int[]{5,3,1},
    new int[]{1,0,5},
    new int[]{2,1,3},
    new int[]{0,0,2},
    new int[]{1,0,5},
    new int[]{0,0,2},
    new int[]{4,2,3},
    new int[]{2,1,3},
    new int[]{3,3,2},
    new int[]{5,3,1}
        };
        cursor_offset_x=cursor.transform.position.x;
        cursor_offset_y=cursor.transform.position.y+1.0f;
        start_time=Time.time;
        max_trial=answerPath.GetLength(0);
    }
    void shuffleMain(){
        random_idx = new List<int>(6);
        for(int i=0;i<6;i++){
            random_idx.Add(-1);
        }
        random_icons =new List<Vector3>();
        resetPos();
        for (int i = 0; i < 6; i++)
            {
               int index = Random.Range(0, 6);
               while (random_idx.Contains(index))
               {
                  index = Random.Range(0, 6);
                  start_time=Time.time;
               }
               random_idx[i]=index;
            }
        for(int i=0;i<6;i++){
            random_icons.Add(new Vector3(0,0,0));
            random_icons[i]=level_icons[random_idx[i]];
            map[random_idx[i]]=i;
        }
        for(int i=0;i<6;i++){
            Main_interface.transform.GetChild(i).transform.localPosition=random_icons[i];
        }
    }
    void waitforWatchSetup(){
        //Network.instance.UdpSend("off");
        string[] action_input=Network.instance.get_datas();
        Debug.Log("SETUPP");
        switch(action_input[0]){
            case "stbon": stb_mode=true;break;
            case "btson": bts_mode=true;break;
            case "btapon": btap_mode=true;break;
            default:break;
        }
    }
    void getCursor(){
        string[] cursor_pos=Network.instance.get_points();
        if(cursor_pos!=null && (cursor_pos[0]=="p" || cursor_pos[0]=="np")){
            float x=cursor_offset_x+float.Parse(cursor_pos[1])/(225.0f);
            float y=cursor_offset_y+(-1)*float.Parse(cursor_pos[2])/(225.0f);
            x=x*0.9f;
            y=y*0.9f;
            cursor.transform.localPosition=new Vector3(x,y,cursor.transform.localPosition.z);
        }
        //cursor.transform.position=new Vector3(cursor_offset_x,cursor_offset_y,cursor.transform.localPosition.z);
    }
    public void setLeverOver(bool isOver){
        isLevelOver=isOver;
    }
    void resetPos()
    {
         for(int i=0;i<6;i++){
            Main_interface.transform.GetChild(i).transform.localPosition=level_icons[i];
            //Debug.Log("Icon Name"+","+i.ToString()+","+this.gameObject.transform.GetChild(i).transform.position);
        }
    }
    void chooseApp(string[] action_input){
        //first input is for the main input -> to other hierachies
        if (max_trial == trial)
        {
                trial_done = true;
        }else{
            if(onlyonce ==0){
                shuffleMain();
                onlyonce=1;
            }
        }
        if((action_input[0]=="stb" || action_input[0]=="bts" || action_input[0]=="btap")&&current_level==0&&!trial_done){
            if (map[int.Parse(action_input[1])]==answerPath[trial][current_level])
            {
                selected_appli = appli_List[map[int.Parse(action_input[1])]];
                selected_appli.SetActive(true);
                level = selected_appli.transform.childCount;
                //isMain=false;
                isLevelOver = false;
                level_data.Add(action_input[0].ToString() + "," + action_input[2].ToString() + "," + (Time.time - start_time).ToString() + ",");
                Debug.Log("WHYYYY");
                
                Main_interface.SetActive(false);
                onlyonce=0;
                //selected_appli.transform.GetChild(current_level).gameObject.GetComponent<Level>().enabled = true;
                selected_appli.transform.GetChild(current_level++).gameObject.SetActive(true);
                
            }


            //chooseAction();
            //this.gameObject.SetActive(false);
        }
    }
    void getData(){
        string[] action_input=Network.instance.get_datas();
        Debug.Log("TRAIL"+trial.ToString()+","+current_level.ToString());
        //Debug.Log("isMain"+isMain.ToString());
        // if(!stb_mode && !bts_mode && !btap_mode && watch_active){
        //     waitforWatchSetup();
        // }
        if(action_input[0]=="off"){
            interface_active.SetActive(false);
            watch_active=false;
        }else{
            if(!interface_active.activeSelf)interface_active.SetActive(true);
            watch_active=true;
        }
        if(current_level==0 && watch_active){
            chooseApp(action_input);
        }
                //yield return null;
            // }else{
            //     yield return new WaitForSeconds(5);
            //     current_level=0;
            //     chooseApp(action_input);
            // }
            //yield return null;
    }
    public void append_data(string level_log){
        level_data.Add(level_log);
        //return "";
    }
    public void save_data(){
            string filePath = @"C:\dev\0918_Watchhand\watch.csv";
        
            if (!File.Exists(filePath)) {
                string headers = "Modes, Blank, First time, First tries, Second Time, Second Tries, Answer, Third Time, Third Tries";
                File.AppendAllText(filePath, headers);
                File.AppendAllText(filePath, "\n");
            }
        
            foreach(string line in level_data){
                Debug.Log("INPUT " + line);
                File.AppendAllText(filePath, line);
            }
        
            level_data = new List<string>();  
            //File.AppendAllText(filePath, "\n");  
        }

        
    // Update is called once per frame
    void Update()
    {   
        //string[] action_input=Network.instance.get_datas();
        getCursor();
        if(isLevelOver && !trial_done){
            getData();
        }else if(trial_done){
            Debug.Log("FINISH");
        }
    }
}