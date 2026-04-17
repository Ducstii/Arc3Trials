using System;
using System.Collections.Generic;
using System.IO;
using LabApi.Features.Wrappers;
using LabApi.Loader.Features.Paths;

namespace Arc3Trials.NameHandler
{
    public class NameHandler
    {
        // Initialize 2 arrays for first names and last names.
        private static string[] _firstNames = Array.Empty<string>();
        private static string[] _lastNames = Array.Empty<string>();
        // makes the names dict, as a <int, string> int holding the playerid and string holding the name.
        private static readonly Dictionary<int, string> Names = new();
        
        // Set the names directory via pathmanager
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
            // Create the Names directory for the txt files.
            Directory.CreateDirectory(NamesDir);

            if (!File.Exists(FirstNamesPath))
                // Write the first names file if it does not exist
                File.WriteAllLines(FirstNamesPath, DefaultFirstNames);

            if (!File.Exists(LastNamesPath))
                // Write the last names file if it does not exist
                File.WriteAllLines(LastNamesPath, DefaultLastNames);
            // Read and load both files into the array
            _firstNames = File.ReadAllLines(FirstNamesPath);
            _lastNames = File.ReadAllLines(LastNamesPath);
        }

        // Method to call if assigning a random name to a player target
        public static void GiveRandomName(Player player)
        {
            // get a name as a string by calling randomfrom to just get random index from the array
            string name = $"{RandomFrom(_firstNames)} {RandomFrom(_lastNames)}";
            // makes a dict entry for the player receiving it 
            Names[player.PlayerId] = name;
            // set the players name to the random one
            player.DisplayName = name;
        }

        public static void GiveZombieName(Player player)
        {
            // lookup the player id in the dict to try and get the old name
            Names.TryGetValue(player.PlayerId, out string previousName);
            // grabs a random number from unityengine.random, 2-299
            int instanceId = UnityEngine.Random.Range(0,1000);
            // if the dict doesn't have the userid with  name it will return null, so if it is it becomes "Unknown".
            // And I set the instance ID after 049-2-[id] cause thats how it works.
            player.DisplayName = $"{previousName ?? "Unknown"} | SCP-049-2-{instanceId}";
        }

        // randomfrom method for the arrays, just checks if the array is empty, if it is it returns unknown.
        // if not, it just chooses a random index from the pool
        private static string RandomFrom(string[] pool) => pool.Length > 0 ? pool[UnityEngine.Random.Range(0, pool.Length)] : "Unknown";
    }
}
