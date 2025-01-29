using Raylib_cs;
using System.Numerics;
using System.Security.AccessControl;

public class SlidingBar
{
    private Rectangle rect;
    private Rectangle filledRect;
    private float minValue;
    private float maxValue;

    private int knobWidth = 10;   // Width of the knob
    private Rectangle knob;
    private float sliderValue;
    public float SliderValue {
        get => sliderValue;
        private set => sliderValue = Math.Clamp(value, minValue, maxValue);
        //private set => sliderValue = value;
    }

    private bool isMouseOverKnob;
    private bool isDragging;
    private Color emptyColor = Color.White;
    private Color filledColor = Color.DarkGray;
    private Color knobColor => isMouseOverKnob ? Color.DarkBlue: Color.SkyBlue;
    public SlidingBar(Vector2 position, int width, int height, float minValue, float maxValue, float sliderValue)
    {
        rect = new Rectangle(position, new Vector2(width, height));
        this.minValue = minValue;
        this.maxValue = maxValue;
        SliderValue = sliderValue;
        filledRect = new Rectangle(position, new Vector2(width * getPercentage(), height));
        knob = new Rectangle((int)rect.X + getPercentage()*(rect.Size.X-knobWidth), (int)rect.Y-2, knobWidth, (int)rect.Size.Y+4);
    }

    private float getPercentage()
    {
        return  (SliderValue - minValue) / (maxValue - minValue);
    }
    
    public void Update()
    {
        Vector2 MousePos = GameState.Instance.Mouse.MousePos;
        
        isMouseOverKnob = Raylib.CheckCollisionPointRec(MousePos, knob);
        if (isMouseOverKnob)
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                isDragging = true;
                
            }

        }
        if (isDragging & Raylib.IsMouseButtonDown(MouseButton.Left))
        {
            knob.X = Math.Clamp(MousePos.X - (knobWidth / 2), rect.X, rect.X + rect.Size.X - knobWidth);
            sliderValue = minValue + (knob.X - rect.X) / (rect.Size.X - knobWidth) * (maxValue - minValue);
        }
        else
        {
            isDragging = false;
        }


        filledRect.Size =  new Vector2(rect.Size.X * getPercentage(), rect.Size.Y);
    }

    public void Draw()
    {
        Raylib.DrawRectangleRec(rect, emptyColor);
        Raylib.DrawRectangleRec(filledRect, filledColor);
        Raylib.DrawRectangleRec(knob, knobColor);
        Raylib.DrawRectangleLinesEx(rect, 1, Color.Black);
    }
}