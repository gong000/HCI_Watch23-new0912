package com.example.samsungsample;

import android.content.Context;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.os.VibrationEffect;
import android.os.Vibrator;
import android.util.AttributeSet;
import android.util.Log;
import android.view.View;

import androidx.annotation.Nullable;

public class Radial_View extends View {
    float bezel_X=0;
    float bezel_Y=0;
    float  prev_x=200;
    float prev_y=450;
    float slide_x;
    float slide_y;
    double prev_angle;
    int area_num=0;
    Vibrator v;
    Paint paint;
    public Radial_View(Context context) {
        super(context);
        v=(Vibrator)context.getSystemService(Context.VIBRATOR_SERVICE);
        init();
    }

    public Radial_View(Context context, @Nullable AttributeSet attrs) {
        super(context, attrs);
        v=(Vibrator)context.getSystemService(Context.VIBRATOR_SERVICE);
        init();
    }
    private void init(){
        paint=new Paint();
        paint.setARGB(128,0,128,128);
        this.setBackgroundColor(Color.YELLOW);
    }
    public void setSlider(float x, float y){
        //Log.d("DRAWss",String.valueOf(slide_touched));
        this.slide_x=x;
        this.slide_y=y;
    }
    public double getBezelAngle(float x, float y){
        double ux=x-MainActivity.mid_point[0];
        double uy=y-MainActivity.mid_point[1];
        double dist=Math.sqrt(Math.pow(ux,2)+Math.pow(uy,2));
        double cos=(uy)/dist;
        double angle=Math.acos(cos);
        angle=angle*(180.0f)/Math.PI;
        //Log.d("Angle",String.valueOf(angle));
        if(x<225){
            angle=360-angle;
        }
        if(dist>120){
            //Log.d("Angle",String.valueOf(angle));
            return 360-angle;
        }else{
            return -999;
        }
    }
    public void drawRadial(Canvas canvas,float x, float y){
        double target_angle=getBezelAngle(x,y);
        if(target_angle!=-999){
            prev_angle=target_angle;
            radialPos(canvas,target_angle);
            new Thread(() -> {
                MainActivity.sendData("sl,"+String.valueOf((int)((target_angle/330)*100)),5555);
            }).start();
        }else{
            radialPos(canvas,prev_angle);
        }
        vibrate();
    }
    public void radialPos(Canvas canvas,double target_angle){
        if(target_angle>=330){
            target_angle=330;
        }else if(target_angle<30){
            target_angle=30.5;
        }
        Log.d("ANGLEE",String.valueOf(target_angle));
        canvas.drawArc(0,0,450,450,120,(float)target_angle-30,false,paint);
        //vibrate();
    }
    public void vibrate(){
        v.vibrate(VibrationEffect.createOneShot(100, VibrationEffect.DEFAULT_AMPLITUDE));
    }
    public void onDraw(Canvas canvas){
        drawRadial(canvas,this.slide_x,this.slide_y);
        //Log.d("RADIAL", String.valueOf(slide_touched)+", "+String.valueOf(this.slide_x)+", "+String.valueOf(this.slide_y));
        prev_x=this.slide_x;
        prev_y=this.slide_y;
    }
}
