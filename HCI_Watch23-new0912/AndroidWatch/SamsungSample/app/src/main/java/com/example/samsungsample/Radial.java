package com.example.samsungsample;

import androidx.appcompat.app.AppCompatActivity;

import android.graphics.Color;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.os.Bundle;
import android.view.MotionEvent;

public class Radial extends AppCompatActivity {

    float start_x=0;
    float start_y=0;

    Radial_View rdl_view;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        //setContentView(R.layout.activity_stb);
        rdl_view=new Radial_View(this);
        rdl_view.setBackgroundColor(Color.WHITE);
        new Thread(() -> {
            MainActivity.sendData("rdlon", 5566);
        }).start();
    }
    public boolean onTouchEvent(MotionEvent event) {
        //setContentView(this);
        int x = (int) event.getX();
        int y = (int) event.getY();
        switch (event.getAction()) {
            case MotionEvent.ACTION_DOWN:
                new Thread(() -> {
                    MainActivity.sendData("np," + String.valueOf(225) + "," + String.valueOf(225), 5566);
                }).start();
                rdl_view.setSlider(x,y);
                start_x = x;
                start_y = y;
                return true;
            case MotionEvent.ACTION_MOVE:
                new Thread(() -> {
                    MainActivity.sendData("p," + String.valueOf(x) + "," + String.valueOf(y), 5566);
                }).start();
                rdl_view.setSlider(x,y);
                return true;
            case MotionEvent.ACTION_UP:
                //Log.d("INNER","GETINNERPOINT1");
                rdl_view.setSlider(x,y);
                return true;
        }

        return true;
    }

}