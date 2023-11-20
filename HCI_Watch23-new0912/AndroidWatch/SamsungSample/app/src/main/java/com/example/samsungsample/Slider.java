//package com.example.samsungsample;
//import android.content.AttributionSource;
//import android.content.Context;
//import android.util.AttributeSet;
//import android.util.Log;
//import android.view.Display;
//import android.view.MotionEvent;
//import android.view.View;
//import android.graphics.Canvas;
//import android.graphics.Color;
//import android.graphics.Paint;
//import android.widget.TextView;
//import android.os.Vibrator;
//
//import java.io.IOException;
//import java.net.DatagramPacket;
//import java.net.DatagramSocket;
//import java.net.InetAddress;
//import java.net.SocketException;
//import java.util.ArrayList;
//import android.os.VibrationEffect;
//import com.example.samsungsample.dollar.Dollar;
//import com.example.samsungsample.dollar.DollarListener;
//public class Slider extends View{
//    Vibrator v;
//    float start_x;
//    float start_y;
//    Paint paint;
//    Dollar dollar=new Dollar(1);
//    int[] mid_point= {225,225};
//    Canvas canvas;
//    boolean silde_touched=false;
//    float x;
//    float y;
//    public Slider(Context context) {
//        super(context);
//        v=(Vibrator)context.getSystemService(Context.VIBRATOR_SERVICE);
//        canvas=new Canvas();
//        paint=new Paint();
//        paint.setColor(Color.BLUE);
//        dollar.setListener(new DollarListener() {
//            @Override
//            public void dollarDetected(Dollar dollar) {
//            }
//        });
//    }
//    public Slider(Context context, AttributeSet attrs){
//        super(context,attrs);
//        v=(Vibrator)context.getSystemService(Context.VIBRATOR_SERVICE);
//    }
//    public Slider(Context context, AttributeSet attrs, int defStyle){
//        super(context,attrs,defStyle);
//        v=(Vibrator)context.getSystemService(Context.VIBRATOR_SERVICE);
//    }
//    public void setSlider(float x, float y){
//        silde_touched=true;
//        this.x=x;
//        this.y=y;
//    }
//    public double getBezelAngle(float x, float y){
//        double ux=x-mid_point[0];
//        double uy=y-mid_point[1];
//        double dist=Math.sqrt(Math.pow(ux,2)+Math.pow(uy,2));
//        double cos=(uy)/dist;
//        double angle=Math.acos(cos);
//        angle=angle*(180.0f)/Math.PI;
//        //Log.d("Angle",String.valueOf(angle));
//        if(x<225){
//            angle=360-angle;
//        }
//        if(dist>170){
//           return angle;
//        }else{
//            return 0;
//        }
//    }
//    public void drawBezel(Canvas canvas) {
//        double ux=x-mid_point[0];
//        double uy=y-mid_point[1];
//        double dist=Math.sqrt(Math.pow(ux,2)+Math.pow(uy,2));
//        double cos=(uy)/dist;
//        double angle=Math.acos(cos);
//        for (int i = 0; i < 460; i++) {
//            for (int j = 0; j < 460; j++) {
//                if((getBezelAngle(x,y)>getBezelAngle(i,j))&&dist>170){
//                    canvas.drawPoint(i,j,paint);
//                }
//            }
//        }
//        vibrate();
//    }
//    public void vibrate(){
//        v.vibrate(VibrationEffect.createOneShot(100, VibrationEffect.DEFAULT_AMPLITUDE));
//    }
//    public void onDraw(Canvas canvas){
//        if(silde_touched){
//            drawBezel(canvas);
//            silde_touched=false;
//        }
//    }
//}
