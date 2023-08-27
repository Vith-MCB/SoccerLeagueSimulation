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
            team.draws = 0;
            team.lost = 0;
            team.win = 0;
        }
    }
    
    static double CalculateWinProbability(int teamStrengthA, int teamStrengthB)
    {
        const double K = 0.01; // Adjust this constant for sensitivity
        return 1.0 / (1.0 + Math.Pow(10, (teamStrengthB - teamStrengthA) * K)); //Win probability
    }
    
    private static int CalculateGoals(double winProbability, int teamStrengthScoring, int teamStrengthConceding)
    {
        Random random = new Random();

        // Adjust lambda based on the strengths of teams
        double lambda = 0.009 * (teamStrengthScoring - teamStrengthConceding) + 1.5;
        //Console.WriteLine("Lambda: {0}",lambda);
            
        int goals = 0;
        double p = 1.0;

        do
        {
            p *= random.NextDouble();
            goals++;
        } while (p > Math.Exp(-lambda));

        return goals - 1;
    }
    
    public void SimulateMatchWithGoals(Team team1, Team team2)
    {
        // Calculate the probability of each team winning based on their adjusted strengths
        double team1Probability = CalculateWinProbability((int)team1.Strength,(int)team2.Strength);
        double team2Probability = 1 - team1Probability;

        // Calculate the number of goals made by each team based on their probabilities
        int team1Goals = CalculateGoals(team1Probability, (int)team1.Strength, (int)team2.Strength);
        int team2Goals = CalculateGoals(team2Probability,(int)team2.Strength, (int)team1.Strength);

        //Sum of match goals for each team
        //Team 1
        team1.goalsMade += team1Goals;
        team1.goalsTaken += team2Goals;

        //team2
        team2.goalsMade += team2Goals;
        team2.goalsTaken += team1Goals;
        
        //Points system
        if (team1Goals == team2Goals) //Draw
        {
            team1.points += 1;
            team2.points += 1;

            team1.draws += 1;
            team2.draws += 1;
        }
        else if (team1Goals > team2Goals)
        {
            team1.points += 3;
            team1.win += 1;

            team2.lost += 1;
        }
        else
        {
            team2.points += 3;
            team2.win += 1;

            team1.lost += 1;
        }
            
    }

    #endregion
    
}