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
    public void AccessAndRelegation(List<Team> firstDiv, List<Team> secondDiv)
    {
        List<Team> relegateds = new List<Team>(4);
        for (int i = 16; i <= 19; i++)
        {
            relegateds.Add(firstDiv[i]);
        }

        foreach (Team team in relegateds)
        {
            firstDiv.Remove(team);
        }

        for (int i = 0; i <= 3; i++)
        {
            firstDiv.Add(secondDiv[0]);
            secondDiv.Remove(secondDiv[0]);
        }
    
        foreach (Team team in relegateds)
        {
            secondDiv.Add(team);
        }
    
        relegateds.Clear();
    }
    
    public void PrintTable(List<Team> table)
    {
        int pos = 0;
        foreach (Team team in table)
        {
            ++pos;
            Console.WriteLine($"{pos} | {team.Name} | {team.points}pts");
        }
    }
    
    public void ResetBoard(List<Team> board)
    {
        foreach (Team team in board)
        {
            team.points = 0;
            team.goalsMade = 0;
            team.goalsTaken = 0;
        }
    }

    #endregion
}