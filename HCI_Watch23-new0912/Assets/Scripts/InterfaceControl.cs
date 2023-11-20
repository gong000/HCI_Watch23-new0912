// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;
// using System;

// using UnityEngine.UI;
// public class InterfaceControl : MonoBehaviour
// {
//     public GameObject answer_obj;
//     public TextMeshPro TMP_Text;
//     public GameObject tmp;   
//     // public GameObject[] cubes;
//     // GameObject[] quads;
//     public GameObject Radial;
//     public GameObject Radial2;
//     public GameObject icons;
//     public GameObject cursor;
//     // public Material selected;
//     // public Material unselected;
//     float cursor_offset_x;
//     float cursor_offset_y;
//     int current_level=0;
//     bool mode_change = true;
//     float duration;
//     //bool[] check_arr=new bool[6];
//     Volume_Control vc;
//     public GameObject[] levels;
//     public List<GameObject> task_queue;
//     bool isStart=false;
//     bool isgenerated=false;
//     string current_action = "";
//     bool[] check_queue=new bool[3];
//     string[] input_data=new string[5];
//     int number=1;
//     float start_time;
//     string prev_action = "";
//     // Start is called before the first frame update
//     void Start()
//     {
//         icons.SetActive(false);
//         Radial2.SetActive(false);
//         current_level=0;
//         //task_queue=new List<Level>(3);
//         for(int i=0;i<3;i++){
//             check_queue[i]=false;
//         }
//         cursor_offset_x=cursor.transform.position.x;
//         cursor_offset_y=cursor.transform.position.y+1.0f;
//         for(int i=0;i<3;i++){
//             levels[i].SetActive(false);
//         }
//         //icons.SetActive(true);
//         //generateTask();
//     }
//     void generateTask(){
//         Debug.Log(DataControl.instance.task_end);
//         task_queue=new List<GameObject>(3);
//         List<int> task=DataControl.instance.set_randomize_hierachy();
//         List<int> answer=DataControl.instance.set_randomize_answer();
//         for(int i=0;i<3;i++){
//             levels[i].GetComponent<Level>().set_answer(answer[i]);
            
//             task_queue.Add(levels[i]);
//         }
//         Debug.Log("ANSWER" + answer[0].ToString()+", "+ answer[1].ToString()+", "+answer[2].ToString());
//         isgenerated =true;
//         start_time=Time.time;
//         // input_data=new String[4];
//         // DataControl.instance.write_list(input_data);
//         input_data=new String[5];
//         input_data[0]=number.ToString();
//     }
//     IEnumerator getData(){
//         string[] msg=Network.instance.get_datas();
//         string[] msgs=Network.instance.get_points();
//         //Debug.Log(msg[0]);
//         //TMP_Text.text="";
//         if(msg[0]=="off"){
//             TMP_Text.text = "";
//             cursor.SetActive(false);
//             icons.SetActive(false);
//         }
//         else{
//             task_process();
//             //TMP_Text.text = msg[0] + " " + current_level.ToString();
//             //if(!icons.activeSelf)icons.SetActive(true);
//             if (msgs[0]=="np"){
//                 //tmp.SetActive(true);
//                 if(!icons.activeSelf)icons.SetActive(true);
//                 cursor.SetActive(false);
//                 cursor.transform.localPosition=new Vector3(cursor_offset_x,cursor_offset_y,cursor.transform.localPosition.z);
//             }else if(msgs[0]=="p"){
//                 //tmp.SetActive(false);
//                 if (!icons.activeSelf)
//                 {
//                     icons.SetActive(true);
                    
//                     //answer_obj= task_queue[current_level].GetComponent<Level>().answer

//                 }
//                 cursor.SetActive(true);
//                 //TMP_Text.text = "";
//                 drawing(msgs);
//             }else if (msgs[0] == "mm")
//             {
//                 mode_change = true;
//             }
//             if((msg[0]=="stb" || msg[0]=="bts" || msg[0]=="btap")){
                
//                 Debug.Log(msg[1]);
//                 if (mode_change)
//                 {   
//                     mode_change = false;
//                     current_action = msg[0];
//                     userAnswer(msg);
//                     TMP_Text.text = msg[0];

//                 }
//                 else if(!mode_change)
//                 {
//                     if (current_action == msg[0])
//                     {
//                         TMP_Text.text = msg[0];
//                         userAnswer(msg);
//                     }
//                 }
//                 if(current_level==0)input_data[0]+="["+msg[0]+"]";
                
//             }else if(msg[0]=="sl"){
//                 if(Radial2.activeSelf){
//                     Radial2.SetActive(false);
//                 }
//                 TMP_Text.text=msg[0];
//                 Debug.Log("SL");
//                 setRadial(msg);
//             }
//             if(current_level==-1){
//                 reset();
//                 Debug.Log("WAITNG?");
//                 isGet = false;
//                 //msg[0] = "";
//                 yield return new WaitForSeconds(5);
//                 //mode_change = false;
//                 isGet=true;
//                 //mode_change = true;
//                 TMP_Text.text = "";
//             }
//         }
//     }
//     void task_process(){
//         //1. Generate first task -> while task is happening, do not generate new order of task
//         //icons.SetActive(true);
//         for(int i=0;i<3;i++){
//             if(i==current_level){
//                 if(!task_queue[i].activeSelf)task_queue[i].SetActive(true);
//                 answer_obj.transform.GetComponent<Renderer>().material = task_queue[current_level].GetComponent<Level>().materialss[task_queue[current_level].GetComponent<Level>().answer];
//             }
//             else{
//                 task_queue[i].SetActive(false);
//             }
//         }
//         //icons.SetActive(false);
//     }
//     void userAnswer(string[] msg){
//         //mode_change = false;
//         //Task Number
//         //Given Answer
//         int area=int.Parse(msg[1]);
//         //Debug.Log("Index "+area.ToString());
//         //Debug.Log("ACTUAL: "+(levels[current_level].GetComponent<Level>().answer).ToString());
//         //Actual Answer
//         if(task_queue[current_level].GetComponent<Level>().isAnswer(area, task_queue[current_level].GetComponent<Level>().answer)){
//             input_data[current_level+1]="1";
//         }else{
//             return;
//         }

//         //Accuracy
//         //Duration
//         input_data[current_level+1]+=","+(Time.time-start_time).ToString();
//         duration+=(Time.time-start_time);
//         if(current_level==2){
//             input_data[current_level+1]+=","+duration.ToString();
//             //Debug.Log(input_data[current_level]);
//             DataControl.instance.write_list(input_data);
//             //StartCoroutine(reset());
//             //DataControl.instance.task_end=true;
//             current_level=-1;
            
//         }
//         else{
//             current_level++;
//         }
//         //DataControl.instance.set_user_answer(current_level,area);
//     }
//     bool isGet=true;
//     void reset(){
//         input_data=new string[5];
//         current_level=0;
//         isgenerated=false;
//         number++;
//         for(int i=0;i<3;i++){
//             levels[i].SetActive(false);
//         }
//     }
//     // void selectCube(int index,bool select){
//     //     //cubes[index].SetActive(select);
//     //     if(select){
//     //         //quads[index].GetComponent<Renderer>().material=selected;
//     //         //check_arr[index]=true;
//     //         //TMP_Text.text="Widget "+index.ToString()+"\n"+"Selected";
//     //     }else{
//     //         //quads[index].GetComponent<Renderer>().material=unselected;
//     //         //check_arr[index]=false;
//     //         //TMP_Text.text="";
//     //     }
//     // }
//     void setRadial(string[] msg){
//         Debug.Log(msg.Length);
//         double endpoint=double.Parse(msg[1]);
//         if(endpoint<0.122)endpoint=0.122;
//         Radial.GetComponent<Image>().fillAmount=(float)endpoint;
//         tmp.GetComponent<Text>().text=endpoint.ToString();
//         Radial2.SetActive(true);
        
//     }
//     void drawing(string[] msg){
//         Debug.Log("MSGG "+msg[0]);
//         if(msg!=null){
//             float x=cursor_offset_x+float.Parse(msg[1])/(225.0f);
//             float y=cursor_offset_y+(-1)*float.Parse(msg[2])/(225.0f);
//             x=x*0.9f;
//             y=y*0.9f;
//             cursor.transform.localPosition=new Vector3(x,y,cursor.transform.localPosition.z);
//         }
//     }
//     // Update is called once per frame
//     void Update()
//     {   
//         if(!isgenerated){
//             generateTask();
//         }
//         if(isGet){
//             StartCoroutine(getData());
//         }
//     }
// }
