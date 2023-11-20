package com.example.samsungsample;

import android.app.Activity;
import android.graphics.Color;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.os.Bundle;
import android.view.KeyEvent;
import android.view.MotionEvent;
import com.google.android.material.snackbar.Snackbar;

import androidx.appcompat.app.AppCompatActivity;

import android.util.Log;
import android.view.View;

import com.example.samsungsample.databinding.ActivityBtsBinding;

import java.io.IOException;
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;
import java.net.SocketException;

public class BTS extends Activity implements SensorEventListener {
    private ActivityBtsBinding binding;



    float start_x=0;
    float start_y=0;
    String recv_msg;
    BTS_View bts_view;
    int touch_number;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        binding = ActivityBtsBinding.inflate(getLayoutInflater());
        //setContentView(binding.getRoot());
        bts_view=new BTS_View(this);
        setContentView(bts_view);
        new Thread(() -> {
            MainActivity.sendData("btson", 5555);
        }).start();
        new Thread(() -> {
            while(MainActivity.wifi_found)recvData();
        }).start();
        bts_view.setBackgroundColor(Color.parseColor("lime"));
        touch_number=0;
    }

    @Override
    public void onBackPressed() {
        super.onBackPressed();
        new Thread(() -> {
            MainActivity.sendData("btsoff", 5555);
        }).start();
        finish();
    }

    public boolean onTouchEvent(MotionEvent event) {
        //setContentView(this);

        int x = (int) event.getX();
        int y = (int) event.getY();

        switch (event.getAction()) {
            case MotionEvent.ACTION_DOWN:
                //touch_number++;
                new Thread(() -> {
                    MainActivity.sendData("np," + String.valueOf(225) + "," + String.valueOf(225), 5566);
                }).start();

                start_x = x;
                start_y = y;
                return true;
            case MotionEvent.ACTION_MOVE:
                new Thread(() -> {
                    MainActivity.sendData("p," + String.valueOf(x) + "," + String.valueOf(y), 5566);
                }).start();
                return true;
            case MotionEvent.ACTION_UP:
                touch_number++;
                    Log.d("INNER","GETINNERPOINT1");
                    if (MainActivity.is_bezel_point(start_x, start_y)) {
                        double dist = Math.sqrt(Math.pow((x - start_x), 2) + Math.pow((y - start_y), 2));
                        Log.d("INNER","GETINNERPOINT2"+String.valueOf((dist)));
                        if (!MainActivity.is_bezel_point(x, y) && dist > 120) {
                            bts_view.get_inner_point(start_x, start_y);

                            Log.d("INNER","GETINNERPOINT3");
                            if(MainActivity.watch_active){
                                new Thread(() -> {
                                    MainActivity.sendData("bts," + String.valueOf(bts_view.getBezelArea(start_x, start_y))+ ","+String.valueOf(touch_number), 5555);
                                }).start();
                            }
                        }
                    }
                setContentView(bts_view);
                return true;
        }

        return true;
    }
    public void recvData(){
        try {
            DatagramSocket udpsocket = new DatagramSocket();
            byte[] buffer=new byte[100];
            DatagramPacket packet = new DatagramPacket(buffer,buffer.length, InetAddress.getByName(MainActivity.ip), 5580);
            udpsocket.receive(packet);
            recv_msg = new String(packet.getData(),0,packet.getLength());
            Log.d("RECV",recv_msg);
            if(recv_msg=="off"){
                finish();
            }
            //Log.d("recv",msg);
        } catch (SocketException e) {
            System.out.println(e);
        } catch (IOException e) {
            System.out.println(e);
        }
    }
    @Override
    public void onSensorChanged(SensorEvent sensorEvent) {

    }

    @Override
    public void onAccuracyChanged(Sensor sensor, int i) {

    }
}