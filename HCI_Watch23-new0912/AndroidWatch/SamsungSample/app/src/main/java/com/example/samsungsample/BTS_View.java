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

public class BTS_View extends View {
    float bezel_X=0;
    float bezel_Y=0;
    int area_num=0;
    boolean inner_touched=false;
    Vibrator v;
    Paint paint;
    public BTS_View(Context context) {
        super(context);
        v=(Vibrator)context.getSystemService(Context.VIBRATOR_SERVICE);
        init();
    }

    public BTS_View(Context context, @Nullable AttributeSet attrs) {
        super(context, attrs);
        v=(Vibrator)context.getSystemService(Context.VIBRATOR_SERVICE);
        init();
    }
    private void init(){
        paint=new Paint();
        paint.setARGB(128,255,0,0);
        paint.setStrokeWidth(100);
        paint.setAntiAlias(true);
        paint.setStrokeCap(Paint.Cap.ROUND);
        paint.setStyle(Paint.Style.STROKE);
    }
    public int getBezelArea(float x, float y){
        double ux=x-MainActivity.mid_point[0];
        double uy=y-MainActivity.mid_point[1];
        double dist=Math.sqrt(Math.pow(ux,2)+Math.pow(uy,2));
        double cos=(-uy)/dist;
        double angle=Math.acos(cos);
        angle=angle*(180.0f)/Math.PI;
        //Log.d("Angle",String.valueOf(angle));
        if(x<225){
            angle=360-angle;
        }
        if(dist>120){
            int area=((int)(angle)/60)%6;
            area_num=area;
            return area;
        }else{
            return -1;
        }
    }
    public void get_inner_point(float x, float y){
        this.bezel_X=x;
        this.bezel_Y=y;
        inner_touched=true;

    }
    public void drawBezel(Canvas canvas,float x,float y) {
        int area=getBezelArea(x,y);
        canvas.drawArc(0,0,450,450,-75+area*60,30,false,paint);
        Log.d("INNNNNNN",String.valueOf(area));
        vibrate();
    }
    public void vibrate(){
        v.vibrate(VibrationEffect.createOneShot(100, VibrationEffect.DEFAULT_AMPLITUDE));
    }
    public void onDraw(Canvas canvas){
        Log.d("INNNNNNN",String.valueOf(inner_touched));
        if(inner_touched){
            drawBezel(canvas,this.bezel_X,this.bezel_Y);
            inner_touched=false;
        }
    }
}
