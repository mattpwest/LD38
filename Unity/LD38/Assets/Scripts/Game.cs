using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
    public int Level = 0;
    public int Seed { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    private static Game instance;

    private Game()
    {
        Seed = 123;
        Width = 3;
        Height = 3;
    }

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
}
