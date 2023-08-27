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

List<Team> CreateDivision(string filepath)
{
    List<Team> division = new List<Team>();
    string[] teamArr = ReadTeamsFile(filepath);
    division = CreateTeamsList(teamArr);
    return division;
}

#endregion

League BrazilianLeague = new League(CreateDivision(firstDivFilepath), CreateDivision(secondDivFilepath));

Console.WriteLine("PRIMEIRO TESTE:");

BrazilianLeague.SimulateMatchWithGoals(BrazilianLeague.FirstDiv[16], BrazilianLeague.SecondDiv[0]);
BrazilianLeague.SimulateMatchWithGoals(BrazilianLeague.FirstDiv[16], BrazilianLeague.SecondDiv[0]);
BrazilianLeague.SimulateMatchWithGoals(BrazilianLeague.FirstDiv[16], BrazilianLeague.SecondDiv[0]);
BrazilianLeague.SimulateMatchWithGoals(BrazilianLeague.FirstDiv[16], BrazilianLeague.SecondDiv[0]);
BrazilianLeague.SimulateMatchWithGoals(BrazilianLeague.FirstDiv[16], BrazilianLeague.SecondDiv[0]);


Console.WriteLine(BrazilianLeague.FirstDiv[16].points);


//static List<Team> SimulateLeague()


//BrazilianLeague.AccessAndRelegation(BrazilianLeague.FirstDiv,BrazilianLeague.SecondDiv,4);
// Console.WriteLine("First Div:");
// BrazilianLeague.PrintTable(BrazilianLeague.FirstDiv);
//
// Console.WriteLine("\nSecond Div:");
// BrazilianLeague.PrintTable(BrazilianLeague.SecondDiv);



