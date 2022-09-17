namespace Domain;

public class Board
{
    public int Width = 0;
    public int Height = 0;

    public List<CharInfo>? DrawArea = new List<CharInfo>();
    public double ElapsedTime = 0;
    public List<double> DeltaTimes = new List<double>();
    public int FrameCount = 0;
    
    
    public Input Input = Input.GetDefaultInput();
    
    public Board(int width, int height)
    {
        this.Width = width;
        this.Height = height;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                DrawArea.Add(new CharInfo()
                {
                    Char = " ",
                    ColorRGBr = "255", 
                    ColorRGBg = "255", 
                    ColorRGBb = "255"
                });
            }
        }
    }
}

public class CharInfo
{
    public string Char { get; set; }
    public string ColorRGBr { get; set; }
    
    public string ColorRGBg { get; set; }
    public string ColorRGBb { get; set; }
}