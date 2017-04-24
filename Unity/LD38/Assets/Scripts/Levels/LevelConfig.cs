public class LevelConfig
{
    public int Seed { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }

    public int ScoreGoal { get; private set; }
    public int MatchGoal { get; private set; }
    public int MoveGoal { get; private set; }

    public string Location { get; private set; }
    public string Description { get; private set; }
    public string Goals { get; private set; }
    
    public int Level { get; set; }
    public int ScoreAchieved { get; set; }

    public LevelConfig(
        int seed,
        int width,
        int height,
        int scoreGoal,
        int matchGoal,
        int moveGoal,
        string location,
        string description,
        string goals
    )
    {
        this.Seed = seed;
        this.Width = width;
        this.Height = height;
        this.ScoreGoal = scoreGoal;
        this.MatchGoal = matchGoal;
        this.MoveGoal = moveGoal;
        this.Location = location;
        this.Description = description;
        this.Goals = goals;

        this.Level = 0;
        this.ScoreAchieved = 0;
    }
}