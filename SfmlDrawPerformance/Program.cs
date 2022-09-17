// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Text;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

// this is just a quick and dirty test - you have to place the font manually
Font font = new Font(AppDomain.CurrentDomain.BaseDirectory + "/static/PressStart2P.ttf");
Texture letterFTexture = new Texture(AppDomain.CurrentDomain.BaseDirectory + "/static/ps2p-8-text-32bit_letter_f.png");
Texture letterGTexture = new Texture(AppDomain.CurrentDomain.BaseDirectory + "/static/ps2p-8-text-32bit_letter_g.png");

var textures = new List<Texture>() {letterFTexture, letterGTexture};
var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYabcdefghijklmnopqrstuvwxy";

var displayedSprites = new List<Sprite>();
var displayedText = new List<Text>();

var window = new RenderWindow(new VideoMode(960, 720), "PerformanceTest");
window.Closed += (sender, e) =>
{
    window.Close();
};
window.SetFramerateLimit(0);
Console.OutputEncoding = Encoding.Unicode;
var width = 100;
var height = 40;

var random = new Random();
var frameCount = 0;
var deltaTimes = new List<double>();
var stopwatch = new Stopwatch();
stopwatch.Start();
var pressedKeyList = new List<Keyboard.Key>();
window.KeyPressed += Window_KeyPressed;
window.KeyReleased += Window_KeyReleased;


void Init()
{
    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            var text = new Text(
                chars[random.Next() % chars.Length].ToString(),
                font)
            {
                Position = new Vector2f(
                    x * 8,
                    y * 8),
                CharacterSize = 8
            };
            displayedText.Add(text);

            var sprite = new Sprite(textures[random.Next() % textures.Count])
            {
                Position = new Vector2f(
                    x * 8,
                    y * 8
                )
            };

            displayedSprites.Add(sprite);
        }
    }
}
Init();

while (window.IsOpen)
{
    window.Clear();
    window.DispatchEvents();
    
    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            if (pressedKeyList.Contains(Keyboard.Key.W))
            {
                window.Draw(displayedText[y * width + x]);
            }
            if (pressedKeyList.Contains(Keyboard.Key.Q))
                displayedText[y * width + x].DisplayedString = chars[random.Next(chars.Length)].ToString();
            
            if (pressedKeyList.Contains(Keyboard.Key.E))
            {
                displayedSprites[y * width + x].Texture = textures[random.Next(textures.Count)];
                displayedSprites[y * width + x].Position = new Vector2f(random.Next((int) window.Size.X), random.Next((int) window.Size.Y));
                window.Draw(displayedSprites[y * width + x]);
            }
        }
    }

    frameCount++;
    deltaTimes.Add(1.0 / stopwatch.ElapsedMilliseconds * 1000);
    deltaTimes = deltaTimes.Skip(Math.Max(0, deltaTimes.Count - 100)).ToList();
    stopwatch.Restart();

    window.Draw(new Text($"FrameCount: {frameCount}, FPS: {deltaTimes.Average()}", font) 
    {
        Position = new Vector2f(40, 700),
        CharacterSize = 16
    });

    window.Display();
}


void Window_KeyPressed(object? sender, SFML.Window.KeyEventArgs e)
{
    if (! pressedKeyList.Contains(e.Code))
    {
        pressedKeyList.Add(e.Code);   
    }
}

void Window_KeyReleased(object? sender, SFML.Window.KeyEventArgs e) {
    if (pressedKeyList.Contains(e.Code))
    {
        pressedKeyList.Remove(e.Code);   
    }
}
