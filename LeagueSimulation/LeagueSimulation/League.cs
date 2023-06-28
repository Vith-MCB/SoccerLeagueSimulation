namespace LeagueSimulation;


//This object will be used to simulate the league and organize the code
public class League
{
    //By standard, the league name is "Brasileirao", it can be changed
    public string name = "Brasileirao";

    public List<Team> FirstDiv { get; set; }
    
    public List<Team> SecondDiv { get; set; }
    
    //League constructor
    public League(List<Team> firstDiv, List<Team> secondDiv)
    {
        this.FirstDiv = firstDiv;
        this.SecondDiv = secondDiv;
    }

    #region Functions
    public void AccessAndRelegation(List<Team> firstDiv, List<Team> secondDiv, int relegatedZoneLength)
    {
        List<Team> relegateds = new List<Team>(4); // List to store relegated teams
    
        // Retrieve the last 'relegatedZoneLength' teams from the first division
        for (int i = firstDiv.Count - relegatedZoneLength; i <= firstDiv.Count - 1; i++)
        {
            relegateds.Add(firstDiv[i]);
        }

        // Remove the relegated teams from the first division
        foreach (Team team in relegateds)
        {
            firstDiv.Remove(team);
        }

        // Promote teams from the second division to fill the vacant spots in the first division
        for (int i = 0; i <= relegatedZoneLength - 1; i++)
        {
            firstDiv.Add(secondDiv[0]); // Add the first team from the second division
            secondDiv.Remove(secondDiv[0]); // Remove the added team from the second division
        }

        // Move the relegated teams to the second division
        foreach (Team team in relegateds)
        {
            secondDiv.Add(team);
        }

        relegateds.Clear(); // Clear the relegated teams list
    }

    public void PrintTable(List<Team> table)
    {
        int pos = 0;
        foreach (Team team in table)
        {
            ++pos;
            Console.WriteLine($"{pos} | {team.Name} | {team.points}pts"); // Print team position, name, and points
        }
    }

    public void ResetBoard(List<Team> board)
    {
        // Reset the points, goals made, and goals taken for each team in the board
        foreach (Team team in board)
        {
            team.points = 0;
            team.goalsMade = 0;
            team.goalsTaken = 0;
        }
    }

    public string SimulateMatchWithGoals(Team team1, Team team2)
    {
        Random random = new Random();

        // Calculate the probability of each team winning based on their adjusted strengths
        double team1Probability = (team1.Strength * 0.9) / 100.0;
        double team2Probability = (team2.Strength * 0.9) / 100.0;

        // Calculate the number of goals made by each team based on their probabilities
        int team1Goals = CalculateGoals(team1Probability, random);
        int team2Goals = CalculateGoals(team2Probability, random);

        // Compare the number of goals made to determine the winner
        if (team1Goals > team2Goals)
        {
            return $"Team 1 ({team1Goals} goals) x Team 2 ({team2Goals} goals)";
        }
        else if (team2Goals > team1Goals)
        {
            return $"Team 2 ({team2Goals} goals) x Team 1 ({team1Goals} goals)";
        }
        else
        {
            return $"Draw ({team1Goals} goals each)"; // In case both teams score the same number of goals
        }
    }

    private static int CalculateGoals(double probability, Random random)
    {
        int goals = 0;

        // Generate a random value between 0 and 1
        double randomValue = random.NextDouble();

        // Determine the number of goals based on the probability and random value
        while (randomValue < probability)
        {
            goals++;

            // Adjust the probability based on team strength
            probability -= 0.05;
            randomValue = random.NextDouble();
        }

        return goals;
    }

    #endregion
}