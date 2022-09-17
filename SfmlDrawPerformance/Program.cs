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

while (window.IsOpen)
{
    window.DispatchEvents();
    window.Clear();

    
    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            if (frameCount == 0)
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
            if (SFML.Window.Keyboard.IsKeyPressed(Keyboard.Key.Q))
            {
                displayedText[y * width + x].DisplayedString = chars[random.Next(0, chars.Length)].ToString();
            }
            if (SFML.Window.Keyboard.IsKeyPressed(Keyboard.Key.W) || frameCount == 0)
            {
                window.Draw(displayedText[y * width + x]);
            }
            if (SFML.Window.Keyboard.IsKeyPressed(Keyboard.Key.E))
            {
                displayedSprites[y * width + x].Texture = textures[random.Next(0, textures.Count)];
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

