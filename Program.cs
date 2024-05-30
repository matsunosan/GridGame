using System;
using System.IO;
using System.Text.Json;

namespace GridGame
{
    class Program
    {
        static void Main(string[] args)
        {
            GameState gameState = LoadGameState();

            if (gameState == null)
            {
                gameState = new GameState();
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"You are at position ({gameState.PlayerX}, {gameState.PlayerY})");
                Console.WriteLine("Enter a command (up, down, left, right, save, exit):");
                string command = Console.ReadLine().ToLower();

                switch (command)
                {
                    case "up":
                        if (gameState.PlayerY > 0) gameState.PlayerY--;
                        break;
                    case "down":
                        if (gameState.PlayerY < 4) gameState.PlayerY++;
                        break;
                    case "left":
                        if (gameState.PlayerX > 0) gameState.PlayerX--;
                        break;
                    case "right":
                        if (gameState.PlayerX < 4) gameState.PlayerX++;
                        break;
                    case "save":
                        SaveGameState(gameState);
                        Console.WriteLine("Game saved!");
                        break;
                    case "exit":
                        SaveGameState(gameState);
                        Console.WriteLine("Game saved! Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid command.");
                        break;
                }
            }
        }

        static void SaveGameState(GameState gameState)
        {
            string json = JsonSerializer.Serialize(gameState);
            File.WriteAllText("gamestate.json", json);
        }

        static GameState LoadGameState()
        {
            if (File.Exists("gamestate.json"))
            {
                string json = File.ReadAllText("gamestate.json");
                return JsonSerializer.Deserialize<GameState>(json);
            }
            return null;
        }
    }

    public class GameState
    {
        public int PlayerX { get; set; } = 0;
        public int PlayerY { get; set; } = 0;
    }
}
