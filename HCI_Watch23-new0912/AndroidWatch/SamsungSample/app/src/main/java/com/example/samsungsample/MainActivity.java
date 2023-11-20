package com.example.samsungsample;

import static android.content.ContentValues.TAG;

import android.Manifest;
import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.gesture.Gesture;
import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Path;
import android.graphics.PixelFormat;
import android.hardware.SensorEventListener;
import android.net.wifi.WifiManager;
import android.os.Build;
import android.os.Bundle;
import android.provider.SyncStateContract;
import android.text.format.Formatter;
import android.util.Log;
import android.view.GestureDetector;
import android.view.Gravity;
import android.view.MotionEvent;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.view.View;

import com.example.samsungsample.databinding.ActivityMainBinding;
import com.example.samsungsample.dollar.Dollar;
import com.example.samsungsample.dollar.DollarListener;
import com.example.samsungsample.dollar.Point;
import com.google.common.collect.BiMap;

import android.hardware.Sensor;
import android.hardware.SensorManager;
import android.hardware.SensorEvent;

import androidx.annotation.NonNull;
import androidx.core.view.WindowCompat;
import androidx.core.view.WindowInsetsCompat;
import androidx.core.view.WindowInsetsControllerCompat;

import java.io.ByteArrayOutputStream;
import java.io.ObjectOutputStream;
import java.net.UnknownHostException;
import java.util.ArrayList;
import java.util.List;

import java.io.IOException;
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;
import java.net.SocketException;
import java.util.Vector;

public class MainActivity extends Activity implements SensorEventListener{


    PaintView paintView;
    //Slider slider;
    boolean mode_one=true;
    boolean mode_two=false;
    boolean mode_three=false;
    boolean mode_four=false;
    private TextView mTextView;
    public static boolean watch_active=false;
    private TextView mTextView2;
    private ImageView imgview;
    private String data="";
    private String gyrodata="";
    static String IPAddr="192.168.0.3";

    private ActivityMainBinding binding;
    float[] mRotationMatrix = new float[9];
    float[] orientation = new float[3];
    float[] prev_orientation = {0,0,0};
    static int[] mid_point= {225,225};
    private float[] accelerometerReading = new float[3];
    private float[] magnetometerReading = new float[3];
    private GestureDetector myGesture;
    private GestureDetector.OnDoubleTapListener listener;
    Dollar dollar;
    static boolean wifi_found=false;
    static String ip="";
    //String recv_msg="";
    //@Override


    public void screenOnOff(int index){
        if(index==1){
            getWindow().addFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);
        }else{
            getWindow().clearFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);
        }
    }
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        binding = ActivityMainBinding.inflate(getLayoutInflater());
        setContentView(binding.getRoot());

        screenOnOff(1); // 화면이 계속 켜지게 설정
        paintView=new PaintView(this);
        paintView.setBackgroundColor(Color.BLACK);
        Button stb_btn=(Button)findViewById(R.id.STB);
        Button bts_btn=(Button)findViewById(R.id.BTS);
        Button btap_btn=(Button)findViewById(R.id.BTAP);
        stb_btn.setOnClickListener(v -> startActivity(new Intent(MainActivity.this,STB.class)));
        bts_btn.setOnClickListener(v -> startActivity(new Intent(MainActivity.this,BTS.class)));
        btap_btn.setOnClickListener(v -> startActivity(new Intent(MainActivity.this,BTAP.class)));
        new Thread(() -> {
            while(!wifi_found){
                try {
                    ip=InetAddress.getLocalHost().getHostAddress();
                    wifi_found=true;
                    Log.d("IUIP",ip);
                    new Thread(() -> {
                        sendData(ip+",",5590);
                    }).start();
                } catch (UnknownHostException e) {
                    Log.d("WHYYY","WHYYYY");
                    continue;
                }
            }
        }).start();


        //mTextView = binding.texting;
//        paintView=new PaintView(this);
//        dollar=new Dollar(1);

//        setContentView(paintView);
//        paintView.setBackgroundColor(Color.WHITE);
//        dollar.setListener(new DollarListener() {
//            @Override
//            public void dollarDetected(Dollar dollar) {
//            }
//        });
        SensorManager mSensorManager=((SensorManager)getSystemService(SENSOR_SERVICE));
        //List<Sensor> sensors=mSensorManager.getSensorList(Sensor.TYPE_ALL);
        mSensorManager.registerListener(this, mSensorManager.getDefaultSensor(Sensor.TYPE_ACCELEROMETER), SensorManager.SENSOR_DELAY_NORMAL);
        mSensorManager.registerListener(this, mSensorManager.getDefaultSensor(Sensor.TYPE_MAGNETIC_FIELD), SensorManager.SENSOR_DELAY_NORMAL);
        //mSensorManager.registerListener(this, mSensorManager.getDefaultSensor(Sensor.TYPE_ROTATION_VECTOR), SensorManager.SENSOR_DELAY_NORMAL,SensorManager.SENSOR_DELAY_UI);
        try {
            Class.forName("dalvik.system.CloseGuard")
                    .getMethod("setEnabled", boolean.class)
                    .invoke(null, true);
        } catch (ReflectiveOperationException e) {
            throw new RuntimeException(e);
        }

    }
//    boolean bezel_point=false;
//    boolean ismodeChange=true;
//    int level_count=0;
//    int mode=1;
//    int start_x=0;
//    int start_y=0;


    public static boolean is_bezel_point(float x, float y){
        double ux=x-mid_point[0];
        double uy=y-mid_point[1];
        double dist=Math.sqrt(Math.pow(ux,2)+Math.pow(uy,2));
        if(dist>170)return true;
        else{
            return false;
        }
    }
//    public boolean onTouchEvent(MotionEvent event) {
//        if (!watch_active) {
//            return false;
//        }
//        //dollar.setActive(true);
//        //setContentView(paintView);
//        int x = (int) event.getX();
//        int y = (int) event.getY();
////        if (level_count == 3) {
////            new Thread(() -> {
////                sendData("mm", 5566);
////            }).start();
////            ismodeChange = true;
////            level_count = 0;
////        }
//        switch (event.getAction()) {
//            case MotionEvent.ACTION_DOWN:
////                if (mode_one) {
////                    paintView.setBackgroundColor(Color.WHITE);
////                } else if (mode_two) {
////                    paintView.setBackgroundColor(Color.YELLOW);
////                } else if (mode_three) {
////                    paintView.setBackgroundColor(Color.GRAY);
////                } else if (mode_four) {
////                    paintView.setBackgroundColor(Color.BLUE);
////                }
////                dollar.pointerPressed(x, y);
////                if (mode_two && !ismodeChange) {
////                    paintView.modetoTwo = true;
////                    paintView.setSlider(x, y);
////                    Log.d("40??", "DOWWWN");
////                } else {
//                    new Thread(() -> {
//                        sendData("np," + String.valueOf(225) + "," + String.valueOf(225), 5566);
//                    }).start();
//                //}
//                start_x = x;
//                start_y = y;
//                return true;
//            case MotionEvent.ACTION_MOVE:
//                //setContentView(paintView);
////                if (mode_two && !ismodeChange) {
////                    //Log.d("40??","MOVVVE");
////                    paintView.setSlider(x, y);
////                } else {
//                    new Thread(() -> {
//                        sendData("p," + String.valueOf(x) + "," + String.valueOf(y), 5566);
//                    }).start();
//                //}
////                if (is_bezel_point(x, y)) bezel_point = true;
////                else {
////                    bezel_point = false;
////                }
//                dollar.pointerDragged(x, y);
//                return true;
//            case MotionEvent.ACTION_UP:
//                dollar.pointerReleased(x, y);
////                if (mode_one && !ismodeChange) {
////                    if (!is_bezel_point(start_x, start_y)) {
////                        double dist = Math.sqrt(Math.pow((x - start_x), 2) + Math.pow((y - start_y), 2));
////                        if (is_bezel_point(x, y) && dist >= 120) {
////                            paintView.get_bezel_point(x, y);
////                            new Thread(() -> {
////                                sendData("stb," + String.valueOf(paintView.getBezelArea(x, y)), 5555);
////                            }).start();
////                            level_count++;
////                        }
////                    }
////
////                } else if (mode_two && !ismodeChange) {
////                    paintView.setSlider(x, y);
////                    //Log.d("40??","UPPPPP");
////                } else if (mode_three && !ismodeChange) {
////                    //Log.d("INNER","GETINNERPOINT1");
////                    if (is_bezel_point(start_x, start_y)) {
////                        double dist = Math.sqrt(Math.pow((x - start_x), 2) + Math.pow((y - start_y), 2));
////                        //Log.d("INNER","GETINNERPOINT2"+String.valueOf((dist)));
////                        if (!is_bezel_point(x, y) && dist > 120) {
////                            paintView.get_inner_point(start_x, start_y);
////                            //Log.d("INNER","GETINNERPOINT3");
////                            new Thread(() -> {
////                                sendData("bts," + String.valueOf(paintView.getBezelArea(start_x, start_y)), 5555);
////                            }).start();
////                            level_count++;
////                        }
////                    }
////
////                } else if (mode_four && !ismodeChange) {
////                    if (is_bezel_point(x, y)) {
////                        double dist = Math.sqrt(Math.pow((x - start_x), 2) + Math.pow((y - start_y), 2));
////                        if (dist <= 120) {
////                            paintView.get_bezel_point(x, y);
////                            new Thread(() -> {
////                                sendData("btap," + String.valueOf(paintView.getBezelArea(x, y)), 5555);
////                            }).start();
////                            level_count++;
////                        }
////                    }
////
////                }
////                if (dollar.getScore() > 0.76f) {
////                    Log.d("DDD", dollar.getName() + ", Acc: " + String.valueOf(dollar.getScore()));
////                    if (dollar.getName() == "v") {
//////                        mode = 1;
//////                        setContentView(paintView);
//////                        paintView.setBackgroundColor(Color.WHITE);
//////                        mode_one = true;
//////                        mode_two = false;
//////                        paintView.isSlider(false);
//////                        mode_three = false;
//////                        mode_four = false;
//////                        ismodeChange = false;
////                        startActivity(new Intent(MainActivity.this,STB.class));
////                    } else if (dollar.getName() == "circle CCW") {
////                        //mode = 2;
//////                        //setContentView(paintView);
//////                        paintView.setBackgroundColor(Color.YELLOW);
//////                        mode_one = false;
//////                        mode_two = true;
//////                        paintView.modeChange();
//////                        //paintView.isSlider(true);
//////                        mode_three = false;
//////                        mode_four = false;
//////                        ismodeChange = false;
////                    } else if (dollar.getName() == "triangle") {
////                        startActivity(new Intent(MainActivity.this,BTS.class));
//////                        mode = 3;
//////                        paintView.setBackgroundColor(Color.GRAY);
//////                        mode_one = false;
//////                        paintView.isSlider(false);
//////                        mode_two = false;
//////                        mode_three = true;
//////                        mode_four = false;
//////                        ismodeChange = false;
////                    } else if (dollar.getName() == "x") {
////                        startActivity(new Intent(MainActivity.this,BTAP.class));
//////                        mode = 4;
//////                        mode_four = true;
//////                        mode_one = false;
//////                        paintView.setBackgroundColor(Color.BLUE);
//////                        paintView.isSlider(false);
//////                        mode_two = false;
//////                        mode_three = false;
//////                        ismodeChange = false;
////                    }
//////                       } else if (dollar.getName() == "arrow") {
//////                            paintView.prev_x = 200;
//////                            paintView.prev_y = 450;
//////                            ismodeChange=false;
//////                        }
////                }
//////                if(level_count<3){
//////                    new Thread(() -> {
//////                        sendData("lvl," + String.valueOf(level_count), 5566);
//////                        sendData("lvl," + String.valueOf(level_count), 5566);
//////                    }).start();
//////                }
//////                bezel_point = false;
////                dollar.setActive(false);
//
//                return true;
//
//        }
//
//        return true;
//    }

    public void onSensorChanged(SensorEvent event) {
            if (event.sensor.getType() == Sensor.TYPE_ACCELEROMETER) {
                    System.arraycopy(event.values, 0, accelerometerReading,
                            0, accelerometerReading.length);
            }
            else if (event.sensor.getType() == Sensor.TYPE_MAGNETIC_FIELD) {
                System.arraycopy(event.values, 0, magnetometerReading,
                        0, magnetometerReading.length);

            }
           updateOrientationAngles();
    }
    boolean isactivesent=false;

    public void updateOrientationAngles() {
        // Update rotation matrix, which is needed to update orientation angles.
        SensorManager.getRotationMatrix(mRotationMatrix, null,
                accelerometerReading, magnetometerReading);

        // "mRotationMatrix" now has up-to-date information.
        SensorManager.getOrientation(mRotationMatrix, orientation);
        watch_active = true;
/*
        //Z, X ,YMath.abs(prev_orientation[1]-orientation[1])>=10.0f
        if(Math.toDegrees(orientation[1])>(-200.0f) && Math.toDegrees(orientation[1])<30.0f){
            //Log.d("ANGLE", String.format("%.2f",Math.toDegrees(orientation[1])));
            //isactivesent=false;
            //setContentView(binding.getRoot());
            watch_active=true;
        }else{
            if(!isactivesent){
                new Thread(() -> {adb connect 192.168.137.25

                    sendData("off,",5555);
                }).start();
            }
            //setContentView(paintView);
            watch_active=false;
            //민주수정
        }*/
    }

    public static void sendData(String msg, int port){
        try {
            DatagramSocket udpsocket = new DatagramSocket();
            byte[] buffer = msg.getBytes();
            DatagramPacket packet = new DatagramPacket(buffer, buffer.length, InetAddress.getByName(IPAddr), port);
            udpsocket.send(packet);
            udpsocket.close();
        } catch (SocketException e) {
            System.out.println(e);
        } catch (IOException e) {
            System.out.println(e);
        }
    }
//    public void recvData(){
//        try {
//            DatagramSocket udpsocket = new DatagramSocket();
//            byte[] buffer=new byte[100];
//            DatagramPacket packet = new DatagramPacket(buffer,buffer.length, InetAddress.getByName(ip), 5555);
//            udpsocket.receive(packet);
//            recv_msg = new String(packet.getData(),0,packet.getLength());
//            //Log.d("recv",msg);
//        } catch (SocketException e) {
//            System.out.println(e);
//        } catch (IOException e) {
//            System.out.println(e);
//        }
//    }
    @Override
    public void onAccuracyChanged(Sensor sensor, int i) {

    }



}