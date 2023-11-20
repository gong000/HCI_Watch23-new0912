package com.example.samsungsample;

import static android.app.PendingIntent.getActivity;

import android.content.AttributionSource;
import android.content.Context;
import android.util.AttributeSet;
import android.util.Log;
import android.view.Display;
import android.view.View;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.widget.TextView;
import android.os.Vibrator;
import java.util.ArrayList;
import android.os.VibrationEffect;
public class PaintView extends View {

    Paint[] paint_arr;
    Canvas canvas;
    int[] mid_point= {225,225};
    public float bezel_X=0;
    public float bezel_Y=0;
    //public float[] pixel_infos=new float[100];
    //public double dist=0;
    boolean bezel_touched=false;
    boolean pos1_done=false;
    boolean modetoTwo=false;
    Paint paint_arc;
    boolean pos2_done=false;
    int area_num=-1;
    boolean slide_touched=false;
    boolean inner_touched=false;
    float  prev_x=200;
    float prev_y=450;
    static double prev_angle;
    Vibrator v;
    //Context pcontext;
    private void init(){
        paint_arr=new Paint[7];
        paint_arc=new Paint();
        paint_arc.setColor(Color.GREEN);
        paint_arc.setStrokeWidth(100);
        paint_arc.setAntiAlias(true);
        paint_arc.setStrokeCap(Paint.Cap.ROUND);
        paint_arc.setStyle(Paint.Style.STROKE);
        for(int i=0;i<7;i++){
            paint_arr[i]=new Paint();
        }
//        bezel_area= new ArrayList<>(6);
//        for(int i=0;i<6;i++){
//            bezel_area.add(i,new pixel_pos());
//        }
        Log.d("Inittd","IMITTTTsssssssssssTTTTTTT");
        //setBezelArea();
        paint_arr[0].setARGB(128,255,0,0);
        paint_arr[1].setARGB(128,0,255,0);
        paint_arr[2].setARGB(128,0,0,255);
        paint_arr[3].setARGB(128,255,255,0);
        paint_arr[4].setARGB(128,255,0,255);
        paint_arr[5].setARGB(128,0,255,255);
        paint_arr[6].setColor(Color.BLACK);
        for(int i=0;i<7;i++){
            paint_arr[i].setStrokeWidth(100);
            paint_arr[i].setAntiAlias(true);
            paint_arr[i].setStrokeCap(Paint.Cap.ROUND);
            paint_arr[i].setStyle(Paint.Style.STROKE);
        }
    }
    public PaintView(Context context) {
        super(context);
        v=(Vibrator)context.getSystemService(Context.VIBRATOR_SERVICE);
        canvas=new Canvas();
        init();
    }
    public PaintView(Context context, AttributeSet attrs){
        super(context,attrs);
        v=(Vibrator)context.getSystemService(Context.VIBRATOR_SERVICE);
        init();
    }
    public PaintView(Context context, AttributeSet attrs, int defStyle){
        super(context,attrs,defStyle);
        v=(Vibrator)context.getSystemService(Context.VIBRATOR_SERVICE);
        init();
    }
    /**
    float slide_x;
    float slide_y;
    public void isSlider(boolean isSlider){
        slide_touched=isSlider;
    }
    public void modeChange(){
        modetoTwo=true;
        slide_touched=true;
    }

    //이 함수는 슬라이더를 실행합니다.
    public void setSlider(float x, float y){
        Log.d("DRAWss",String.valueOf(slide_touched));
        this.slide_x=x;
        this.slide_y=y;
    }
//    public boolean is_bezel_point(float x, float y){
//        double ux=x-mid_point[0];
//        double uy=y-mid_point[1];
//        double dist=Math.sqrt(Math.pow(ux,2)+Math.pow(uy,2));
//        if(dist>170)return true;
//        else{
//            return false;
//        }
//    }
    //이 함수를 실행하게 되면 bezel to slide를 실행하게 됩니다.
    public void get_inner_point(float x, float y){
        this.bezel_X=x;
        this.bezel_Y=y;
        inner_touched=true;
    }

    //이 함수를 실행하게 되면 베젤 터치 여부를 확인 후, 베젤을 그립니다.
    public void get_bezel_point(float x, float y){
        this.bezel_X=x;
        this.bezel_Y=y;
        double ux=this.bezel_X-mid_point[0];
        double uy=this.bezel_Y-mid_point[1];
        double dist=Math.sqrt(Math.pow(ux,2)+Math.pow(uy,2));
        if(dist>170){
            double cos=(-uy)/dist;
            double angle=Math.acos(cos);
            angle=angle*(180.0f)/Math.PI;
            //Log.d("Angle",String.valueOf(angle));
            if(this.bezel_X<225){
                angle=360-angle;
            }
            int area=((int)(angle+30)/60)%6;
            //area_num=area;
            bezel_touched=true;
            Log.d("Area",String.valueOf(area));
        }
    }
    //현재 터치한 베젤의 area값을 가져옵니다. 12시방향부터 0,1,2,3,4,5 순입니다.
    public int getBezelArea(float x, float y){
        double ux=x-mid_point[0];
        double uy=y-mid_point[1];
        double dist=Math.sqrt(Math.pow(ux,2)+Math.pow(uy,2));
        double cos=(-uy)/dist;
        double angle=Math.acos(cos);
        angle=angle*(180.0f)/Math.PI;
        //Log.d("Angle",String.valueOf(angle));
        if(x<225){
            angle=360-angle;
        }
        if(dist>120){
            int area=((int)(angle+30)/60)%6;
            area_num=area;
            return area;
        }else{
            return -1;
        }
    }
    //현재 터치한 위치의 각도를 가져오는 함수입니다.
    public double getBezelAngle(float x, float y){
        double ux=x-mid_point[0];
        double uy=y-mid_point[1];
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
    public void radialPos(Canvas canvas,double target_angle){
        if(target_angle>=330){
            target_angle=330;
        }else if(target_angle<30){
            target_angle=30.5;
        }
        Log.d("ANGLEE",String.valueOf(target_angle));
        canvas.drawArc(0,0,450,450,120,(float)target_angle-30,false,paint_arc);
        //vibrate();
    }

    //Slide 관련 함수, 실시간으로 각도를 유니티에 전송합니다.
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
    //베젤을 그리는 함수입니다.
    public void drawBezel(Canvas canvas,float x,float y) {
        int area=getBezelArea(x,y);
        canvas.drawArc(0,0,450,450,-105+area*60,30,false,paint_arr[area]);
        vibrate();
    }
    //진동
    public void vibrate(){
        v.vibrate(VibrationEffect.createOneShot(100, VibrationEffect.DEFAULT_AMPLITUDE));
    }
    //모든 그리기는 이 함수로 실행이 됩니다. 계속 loop을 돌고있다고 생각하시면 됩니다.
    @Override
    public void onDraw(Canvas canvas){
        if(bezel_touched){
            drawBezel(canvas,this.bezel_X,this.bezel_Y);
            Log.d("Slide","Sliding Bezel"+String.valueOf(area_num));
            bezel_touched=false;
        }
        if(slide_touched && !modetoTwo){
            drawRadial(canvas,this.slide_x,this.slide_y);
            //Log.d("RADIAL", String.valueOf(slide_touched)+", "+String.valueOf(this.slide_x)+", "+String.valueOf(this.slide_y));
            prev_x=this.slide_x;
            prev_y=this.slide_y;
        }else if(modetoTwo){
            drawRadial(canvas,prev_x,prev_y);
            modetoTwo=false;
        }
        if(inner_touched){
            drawBezel(canvas,this.bezel_X,this.bezel_Y);
            //Log.d("SlideR","Sliding BezelR"+String.valueOf(area_num));
            inner_touched=false;
        }
    }**/
}
