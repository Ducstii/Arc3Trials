using System;
using System.Collections.Generic;
using System.IO;
using LabApi.Features.Wrappers;
using LabApi.Loader.Features.Paths;

namespace Arc3Trials.NameHandler
{
    public class NameHandler
    {
        private static readonly Random Random = new Random();
        private static string[] _firstNames = Array.Empty<string>();
        private static string[] _lastNames = Array.Empty<string>();

        public static readonly Dictionary<string, string> Names = new Dictionary<string, string>();

        private static readonly string NamesDir = Path.Combine(PathManager.Configs.FullName, "Arc3Trials");
        private static readonly string FirstNamesPath = Path.Combine(NamesDir, "FirstNames.txt");
        private static readonly string LastNamesPath = Path.Combine(NamesDir, "LastNames.txt");

        private static readonly string[] DefaultFirstNames =
        {
            "James", "John", "Robert", "Michael", "William", "David", "Richard", "Joseph",
            "Thomas", "Charles", "Christopher", "Daniel", "Matthew", "Anthony", "Mark",
            "Donald", "Steven", "Paul", "Andrew", "Kenneth", "Joshua", "Kevin", "Brian",
            "George", "Timothy", "Ronald", "Edward", "Jason", "Jeffrey", "Ryan",
            "Jacob", "Gary", "Nicholas", "Eric", "Jonathan", "Stephen", "Larry",
            "Justin", "Scott", "Brandon", "Benjamin", "Samuel", "Raymond", "Gregory",
            "Frank", "Alexander", "Patrick", "Jack", "Dennis", "Jerry"
        };

        private static readonly string[] DefaultLastNames =
        {
            "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis",
            "Rodriguez", "Martinez", "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson",
            "Thomas", "Taylor", "Moore", "Jackson", "Martin", "Lee", "Perez", "Thompson",
            "White", "Harris", "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson",
            "Walker", "Young", "Allen", "King", "Wright", "Scott", "Torres", "Nguyen",
            "Hill", "Flores", "Green", "Adams", "Nelson", "Baker", "Hall", "Rivera",
            "Campbell", "Mitchell", "Carter", "Roberts"
        };

        public static void LoadNames()
        {
            Directory.CreateDirectory(NamesDir);

            if (!File.Exists(FirstNamesPath))
                File.WriteAllLines(FirstNamesPath, DefaultFirstNames);

            if (!File.Exists(LastNamesPath))
                File.WriteAllLines(LastNamesPath, DefaultLastNames);

            _firstNames = File.ReadAllLines(FirstNamesPath);
            _lastNames = File.ReadAllLines(LastNamesPath);
        }

        public static void GiveRandomName(Player player)
        {
            string name = $"{RandomFrom(_firstNames)} {RandomFrom(_lastNames)}";
            Names[player.UserId] = name;
            player.DisplayName = name;
        }

        public static void GiveZombieName(Player player)
        {
            int instanceId = Random.Next(1000, 9999);
            Names.TryGetValue(player.UserId, out string previousName);
            player.DisplayName = $"Instance #{instanceId} | {previousName ?? "Unknown"}";
        }

        public static void ResetName(Player player)
        {
            player.DisplayName = player.Nickname;
        }

        private static string RandomFrom(string[] pool) => pool[Random.Next(pool.Length)];
    }
}
