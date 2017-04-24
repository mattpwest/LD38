using System.Collections.Generic;
using System.Linq;

public class Game
{
    public int Level = 0;
    public GameEvent LastEvent
    {
        get
        {
            return this.events.Last();
        }
    }

    public LevelConfig CurrentLevelConfig { get; private set; }

    private static Game instance;
    private List<GameEvent> events;

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
        events = new List<GameEvent>();

        AddEvent(new GameStartedEvent(new LevelConfig(0, 6, 6, 1000000, 1000, 1000, "TestPlace", "TestDescription", "TestGoals")));
    }

    public void AddEvent(GameEvent gameEvent)
    {
        events.Add(gameEvent);
    }

    public class GameEvent
    {
        public LevelConfig LevelConfig { get; private set; }

        public GameEvent(LevelConfig levelConfig)
        {
            this.LevelConfig = levelConfig;
        }
    }

    public class GameStartedEvent : GameEvent
    {
        public GameStartedEvent(LevelConfig levelConfig) : base(levelConfig) {
        }
    }

    public class GameLostEvent : GameEvent
    {
        public int Score { get; private set; }

        public GameLostEvent(LevelConfig leveConfig, int score) : base(leveConfig)
        {
            this.Score = score;
        }
    }

    public class GameWonEvent : GameEvent
    {
        public int Score { get; private set; }

        public GameWonEvent(LevelConfig leveConfig, int score) : base(leveConfig)
        {
            this.Score = score;
        }
    }
}