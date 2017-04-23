public class Game
{
    private static Game instance;
    public int Level = 0;
    public int Seed { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int ScoreGoal { get; set; }
    public int MatchGoal { get; set; }
    public int MovesGoal { get; set; }

    public static Game Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new Game();
            }

            return instance;
        }
    }

    private Game()
    {
        Seed = 123;
        Width = 3;
        Height = 3;
        ScoreGoal = 0;
        MatchGoal = 0;
        this.MovesGoal = 10;
    }
}
