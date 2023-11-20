import socket
import threading
sock=socket.socket(socket.AF_INET,socket.SOCK_DGRAM)
sock_pt=socket.socket(socket.AF_INET,socket.SOCK_DGRAM)
ip_addr="192.168.137.1"#"192.168.137.1"    "192.168.137.1";
sock.bind((ip_addr,5555))
unity_sock=socket.socket(socket.AF_INET,socket.SOCK_DGRAM)
unity_sock.bind(("127.0.0.1",5560))
unity_recv_sock=socket.socket(socket.AF_INET,socket.SOCK_DGRAM)
unity_recv_sock.bind((("127.0.0.1"),5550))
sock_pt.bind((ip_addr,5566))
watch_ip=socket.socket(socket.AF_INET,socket.SOCK_DGRAM)
watch_ip.bind((ip_addr,5590))
ip_received=False
watch_ipaddr="192.168.137.25"
def pointToUnity():
    while(True):
        #print(sock_pt)
        data,addr=sock_pt.recvfrom(1024)
        #print(data)
        msg=str(data,'utf-8')
        if(msg!=""):print(msg)
        #msg_arr=msg.split(",")
        unity_sock.sendto(data,("127.0.0.1",5566))
def watchToUnity():
    while(True):
        data,addr=sock.recvfrom(1024)
        msg=str(data,'utf-8')
        if(msg!=""):print(msg)
        #msg_arr=msg.split(",")
        unity_sock.sendto(data,("127.0.0.1",5556))
        
def unityToWatch():
    while(True):
        data,addr=unity_recv_sock.recvfrom(1024)
        msg=str(data,'utf-8')
        print(msg)
        if(msg!=""):
            watch_ip.sendto(data,(watch_ipaddr,5555))

thread1=threading.Thread(target=pointToUnity)
thread2=threading.Thread(target=watchToUnity)
thread3=threading.Thread(target=unityToWatch)
thread1.start()
thread2.start()
thread3.start()