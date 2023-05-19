using LeagueSimulation;

#region File paths

//Division files
const string firstDivFilepath =
    @"D:\Programação\GitHub\SoccerLeagueSimulation\LeagueSimulation\files\firstDivTeams.txt";
const string secondDivFilepath =
    @"D:\Programação\GitHub\SoccerLeagueSimulation\LeagueSimulation\files\secondDivTeams";

//Output Files
const string outputfile = 
    @"D:\Programação\GitHub\SoccerLeagueSimulation\LeagueSimulation\files\outputs\output.txt";
    
#endregion

#region Functions

string[] ReadTeamsFile(string filepath)
{
    string[] lines = File.ReadAllLines(filepath);
    return lines;
}

List<Team> CreateTeamsList(string[] teams)
{
    List<Team> teamsList = new List<Team>();
    
    foreach (string str in teams)
    {
        teamsList.Add(CreateTeam(str));
    }

    return teamsList;
}

double ParseToDouble(string str)
{
    double result;
    if (double.TryParse(str, out result)){ return result;}
    // Handle parsing failure
    else{ throw new ArgumentException("Invalid input. Unable to parse the string into a double."); }
}

Team CreateTeam(string str)
{
    string[] teamInfo = str.Split(',');
    return new Team(teamInfo[0]            //Team Name
        ,ParseToDouble(teamInfo[1]) // Team Strength
        );
}

void ResetBoard(List<Team> board)
{
    foreach (Team team in board)
    {
        team.points = 0;
        team.goalsMade = 0;
        team.goalsTaken = 0;
    }
}

void AccessAndRelegation(List<Team> firstDiv, List<Team> secondDiv)
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

List<Team> CreateDivision(string filepath)
{
    List<Team> division = new List<Team>();
    string[] teamArr = ReadTeamsFile(filepath);
    division = CreateTeamsList(teamArr);
    return division;
}

void PrintTable(List<Team> table)
{
    int pos = 0;
    foreach (Team team in table)
    {
        ++pos;
        Console.WriteLine($"{pos} | {team.Name} | {team.points}pts");
    }
}

#endregion

List<Team> firstDiv = new List<Team>();
List<Team> secondDiv = new List<Team>();

firstDiv = CreateDivision(firstDivFilepath);
secondDiv = CreateDivision(secondDivFilepath);

Console.WriteLine("PRIMEIRO TESTE:");

Console.WriteLine("First Div:");
PrintTable(firstDiv);

Console.WriteLine("\nSecond Div:");
PrintTable(secondDiv);

