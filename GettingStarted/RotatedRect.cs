using Microsoft.Xna.Framework;
using System;
public class RotatedRect
{
    private Rectangle rect;
    private Vector2 tl;
    private Vector2 tr;
    private Vector2 bl;
    private Vector2 br;
    private float angle;

    public RotatedRect(int x, int y, int width, int height, float ang) {
        rect = new Rectangle (x,y,width,height);
        angle = ang;
        calculateCorners();
    }

    private void calculateCorners() {
        tl = new Vector2(-rect.Width/2, -rect.Height/2);
        tr = new Vector2(rect.Width/2, -rect.Height/2);
        bl = new Vector2(-rect.Width/2, rect.Height/2);
        br = new Vector2(rect.Width/2, rect.Height/2);

        Matrix rotation =  Matrix.CreateRotationZ(angle) * Matrix.CreateTranslation(rect.X, rect.Y, 0f);
        tl = Vector2.Transform(tl, rotation);
        tr = Vector2.Transform(tr, rotation);
        bl = Vector2.Transform(bl, rotation);
        br = Vector2.Transform(br, rotation);
    }

    public Vector2 getPosition() {
        return new Vector2(rect.X, rect.Y);
    }
    public float getAngle() {
        return angle;
    }
    public void rotate(float ang) {
        angle += ang;
    }
    public void setPosition(float x, float y) {
        rect.X = (int)x;
        rect.Y = (int)y;
        calculateCorners();
    }
    public void setPosition(Vector2 v) {
        setPosition(v.X, v.Y);
    }
    public void move(Vector2 v) {
        setPosition(new Vector2(rect.X, rect.Y) + v);
    }

    public float getBoundingRight() {
        return max4(tl.X, tr.X, bl.X, br.X);
    }
    public float getBoundingLeft() {
        return min4(tl.X, tr.X, bl.X, br.X);
    }
    public float getBoundingTop() {
        return min4(tl.Y, tr.Y, bl.Y, br.Y);
    }
    public float getBoundingBottom() {
        return max4(tl.Y, tr.Y, bl.Y, br.Y);
    }


    private float max4(float a, float b, float c, float d) {
        return Math.Max(a, Math.Max(b, Math.Max(c, d)));
    }
    private float min4(float a, float b, float c, float d) {
        return Math.Min(a, Math.Min(b, Math.Min(c, d)));
    }
}