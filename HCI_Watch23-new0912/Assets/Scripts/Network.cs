using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
public class Network : MonoBehaviour
{   
    static UdpClient srv;
    static UdpClient snd;
    static UdpClient srv_points;
    Thread thread;
    Thread thread2;
    IPEndPoint remoteEP;
    IPEndPoint remoteEP_pt;
    public GameObject cube;
    public Transform transfroms;
    static private AsyncCallback AC;
    static readonly object lockObject = new object();
    static readonly object lockObject2 = new object();
    bool msg_rec=false;
    // Start is called before the first frame update
    public static Network instance;
    string msg="";
    string msgs="";
    void Start()
    {   
        instance=this;
        srv=new UdpClient(5556);
        snd=new UdpClient(5580);
        srv_points=new UdpClient(5566);
        remoteEP=new IPEndPoint(IPAddress.Any,0);
        remoteEP_pt=new IPEndPoint(IPAddress.Any,0);
        thread=new Thread(new ThreadStart(Udpreceive));
        thread.Start();
        thread2=new Thread(new ThreadStart(Udpreceive_pt));
        thread2.Start();

    }
    void Udpreceive(){
        while(true){
            lock(lockObject){
                byte[] dgram = srv.Receive(ref remoteEP);
                msg=System.Text.Encoding.UTF8.GetString(dgram,0,dgram.Length);
                //Debug.Log("Action: "+msg);
            }
        }
    }
    void Udpreceive_pt(){
        while(true){
            lock(lockObject2){
                byte[] dgram = srv_points.Receive(ref remoteEP_pt);
                msgs=System.Text.Encoding.UTF8.GetString(dgram,0,dgram.Length);
                //Debug.Log("PT: "+msgs);
            }
        }
    }
    public string[] get_datas(){
        string[] datas=msg.Split(',');
        msg="";
        return datas;
    }
    public string[] get_points(){
        string[] points=msgs.Split(',');
        msgs="";
        return points;
        
    }
    public void UdpSend(String msg){
        IPEndPoint remoteEP_send=new IPEndPoint(IPAddress.Parse("127.0.0.1"),5550);
        byte[] data=System.Text.Encoding.UTF8.GetBytes(msg);
        snd.Send(data,data.Length,remoteEP_send);
    }
    // Update is called once per frame
    void Update()
    {   
        
    }
}
