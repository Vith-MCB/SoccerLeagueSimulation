namespace LeagueSimulation;


//This object will be used to simulate the league and organize the code
public class League
{
    //Here, you can define the amount of points according to your championship
    //By default (Brasileirão = Victory: 3 point / Draw: 1 point
    #region Constants 
    private const int DRAW = 0;
    private const int WIN = 3;
    #endregion
    
    
    
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

    public static void SortTeams(List<Team> teams)
    {
        foreach (Team team in teams)
        {
            team.CalculateGoalsDifference();
        }
        
        teams.Sort((team1, team2) =>
        {
            // Compare by points first
            int pointsComparison = team2.points.CompareTo(team1.points); // Higher points first
            if (pointsComparison != 0)
            {
                return pointsComparison;
            }

            // If points are the same, compare by goals scored
            return team2.goalsDif.CompareTo(team1.goalsDif); // More goals scored first
        });
    }
    public void PrintTable(List<Team> table)
    {
        SortTeams(table);
        int pos = 0;
        foreach (Team team in table)
        {
            ++pos;
            int matchesPlayed = team.CalculateMatchesPlayed();
            // Print team position, name, and points
            Console.WriteLine("{0} | {1} | {2}pts | {3}mp | {4}W | {5}D | {6}L | {7}Gd", pos,team.Name,team.points, 
                                                                                        matchesPlayed,team.win,team.draws,
                                                                                        team.lost,team.goalsDif);
        }
    }

    public void ResetBoard(List<Team> board)
    {
        // Reset the points, goals made, and goals taken for each team in the board
        foreach (Team team in board)
        {
            team.points = 0;
            team.goalsScored = 0;
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
        const double lambdaFactor = 0.009;
        int goals = 0;
        double p = 1.0;
            
        // Adjust lambda based on the strengths of teams
        double lambda = lambdaFactor * (teamStrengthScoring - teamStrengthConceding) + 1.5;

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
        team1.goalsScored += team1Goals;
        team1.goalsTaken += team2Goals;

        //team2
        team2.goalsScored += team2Goals;
        team2.goalsTaken += team1Goals;
        
        //Points system
        if (team1Goals == team2Goals) //Draw
        {
            team1.points += DRAW;
            team2.points += DRAW;

            team1.draws++;
            team2.draws++;
        }
        else if (team1Goals > team2Goals) //Team 1 Won
        {
            team1.points += WIN;
            
            team1.win++;
            team2.lost++;
        }
        else //Team 2 Won
        {
            team2.points += WIN;
            
            team2.win++;
            team1.lost++;
        }
            
    }

    public void SimulateMatches(List<Team> teams)
    {
        for(int firstTeam = 0; firstTeam < teams.Count; firstTeam++)
        {
            for (int secondTeam = 0; secondTeam < teams.Count; secondTeam++)
            {
                if(firstTeam == secondTeam) //Garanting that the team will not match up with itself
                { 
                    if (secondTeam < (teams.Count - 1)) { secondTeam++; }
                    else { return; }
                    
                } 
                
                SimulateMatchWithGoals(teams[firstTeam], teams[secondTeam]);
            }
        }
    }
    
    #endregion
    
}