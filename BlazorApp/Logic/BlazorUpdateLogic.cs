using System.Globalization;
using Domain;

namespace BlazorApp;

public class BlazorUpdateLogic
{
    private readonly Input Input;

    public BlazorUpdateLogic(Input input)
    {
        Input = input;
    }
    
    public bool Update(double deltaTime, Board board)
    {
        board.ElapsedTime += deltaTime;
        board.DeltaTimes.Add(deltaTime);
        if (board.DeltaTimes.Count > 100)
        {
            board.DeltaTimes = board.DeltaTimes.Skip(Math.Max(0, board.DeltaTimes.Count - 100)).ToList();
        }

        // Draw transformed elements
        double dFps = 1.0d / board.DeltaTimes.Average();
        string sFps = Math.Floor(dFps).ToString(CultureInfo.InvariantCulture);
        
        board.FrameCount++;
        
        
        
        board.Input = Input;
        
        if (board.Input.Keyboard.KeyboardState.FirstOrDefault(x => x.Identifier.Key == Input.KeyboardInput.KeyboardIdentifierList.KeyS.Key).IsPressed())
        {
            return true;
        }
        
        
        var chars = "zxcasdqwe";
        var random = new Random();


        var fpsCounter = "FPS: " + 1 / board.DeltaTimes.Average();
        for (int y = 0; y < board.Height; y++)
        {
            for (int x = 0; x < board.Width; x++)
            {
                var text = chars[random.Next() % chars.Length].ToString();
                if (y == board.Height - 1)
                {
                    text = fpsCounter.Length > x ? fpsCounter.Substring(x, 1) : " ";
                }
                board.DrawArea[y * board.Width + x] = new CharInfo() {
                    Char = text,
                };
            }
        }
        

        
        return false;
    }
}