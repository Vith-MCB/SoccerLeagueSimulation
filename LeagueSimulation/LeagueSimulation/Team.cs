namespace LeagueSimulation;

public class Team
{
    #region Team Info
    public string Name { get; set; }
    public double Strength { get; set; }
    #endregion

    #region Simulated data
    public int points = 0, goalsScored = 0, goalsTaken = 0, win = 0, lost = 0, draws = 0, goalsDif = 0;
    #endregion
    
    //Default constructor
    public Team(){}
    
    //Team Constructor
    public Team(string name, double strength)
    {
        this.Name = name;
        this.Strength = strength;
    }

    #region Functions

    public void CalculateGoalsDifference()
    {
        this.goalsDif = this.goalsScored - this.goalsTaken;
    }

    public void CalculateNewStrength(double percentage){ Strength *= percentage; }

    #endregion
}