// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.IO;
// using System.Threading;
// public class DataControl : MonoBehaviour
// {
//     // Start is called before the first frame update
//     public static DataControl instance=new DataControl();
//     public float start_time;

//     public float end_time;
//     public float duration;
//     public bool task_end=false;
//     List<int> answerPath;
//     List<string[]> data;
//     public List<int> hierarchy;
//     int[] userPath;
//     void Start()
//     {   
//         instance=this;
//         data=new List<string[]>();
//         string nput_data="No,Task 1,Answer 1,Task 1 Duration,Task 2,Answer 2,Task 2 Duration,Task 3,Answer 3,Task 3 Duration,Accuracy,Duration";
//         //answerPath=new List<int>(3);
//         userPath=new int[3];
//     }
//     public List<int> set_randomize_hierachy(){
//         hierarchy=new List<int>(3);
//         for(int i=0;i<3;i++){
//             int level=Random.Range(0,3);
//             while(hierarchy.Contains(level)){
//                 level=Random.Range(0,3);
//                 Debug.Log("LEVEL"+level.ToString());
//             }
//             hierarchy.Add(level);
//         }
//         return hierarchy;
//     }
//     public List<int> set_randomize_answer(){
//         answerPath=new List<int>(3);
//         for(int i=0;i<3;i++){
//             int answer=Random.Range(0,6);
//             while(answerPath.Contains(answer)){
//                 answer=Random.Range(0,6);
//             }
//             answerPath.Add(answer);
//         }
//         return answerPath;
//     }
//     public void set_user_answer(int level, int answer){
//         userPath[level]=answer;
//     }
//     public float check_answers(){
//         float correct=0;
//         for(int i=0;i<3;i++){
//             if(answerPath[i]==userPath[i]){
//                 correct++;
//             }
//         }
//         userPath=new int[3];
//         return (correct/3.0f)*100;
//     }
//     // public float time_check(){
//     //     if(task_end){
//     //         duration=end_time-start_time;
//     //         return end_time-start_time;
//     //     }else{
//     //         return 0;
//     //     }
//     // }
//     // public void write_list(string[] input_data){
//     //     Thread thread = new Thread(() => write_lists(input_data));
//     //     thread.Start();
//     // }
//     public void write_list(string[] input_data){
//         // Debug.Log(":LLINNNNNNNNNNNNNNNNN");
//         // string csvline="";
//         // int i=0;
//         // string filePath="data.csv";
//         foreach(string line in input_data){
//             Debug.Log("INPUT "+line);
//             File.AppendAllText("./data.csv",line+",");
//          }
//          File.AppendAllText("./data.csv","\n");  
//         // using(StreamWriter sw = new StreamWriter(filePath,true)){
//         //     foreach(string line in input_data){
//         //     Debug.Log(line);
//         //         if(i==5){
//         //             Debug.Log("FILLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL");
//         //             csvline+=line;
//         //             sw.WriteLine(csvline);
//         //         }else{
//         //             csvline+=line+",";
//         //         }
//         //         i++;
//         //         //File.AppendAllText("./date.txt",line+"\n");
//         //     }   
//         //     sw.Close();
//         // }
        
//     } 
//     public void write_lists(string[] input_data){
//         //+System.DateTime.Now.ToString("mm")+
//         string filePath="data.csv";
//         Debug.Log(":LLINNNNNNNNNNNNNNNNN");
//         string csvline="";
//         int i=0;
//         using (StreamWriter sw = new StreamWriter(filePath,true)){
//               foreach(string line in input_data){
//                 Debug.Log(line);
//                 if(i==4){
//                     Debug.Log("FILLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL");
//                     csvline+=line;
//                     sw.WriteLine(csvline);
//                  }else{
//                     csvline+=line+",";
//                 }
//                 i++;
//                 //File.AppendAllText("./date.txt",line+"\n");
//             }   
//             sw.Close();
//         }
          
//     }  
//     void save_list(){

//     }
//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }
