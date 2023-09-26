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

BrazilianLeague.SimulateMatches(BrazilianLeague.FirstDiv);

BrazilianLeague.PrintTable(BrazilianLeague.FirstDiv);

//static List<Team> SimulateLeague()


//BrazilianLeague.AccessAndRelegation(BrazilianLeague.FirstDiv,BrazilianLeague.SecondDiv,4);
// Console.WriteLine("First Div:");
// BrazilianLeague.PrintTable(BrazilianLeague.FirstDiv);
//
// Console.WriteLine("\nSecond Div:");
// BrazilianLeague.PrintTable(BrazilianLeague.SecondDiv);

double CalculatePercentageBasedOnPosition(int position)
{
    // Ensure position is within a reasonable range (e.g., 1 to 100)
    position = Math.Max(1, Math.Min(100, position));

    // Calculate the percentage based on position
    // Adjust these values to fit your simulation's dynamics
    double minPercentage = 0.1; // Minimum percentage increase
    double maxPercentage = 1.0; // Maximum percentage increase

    // Linearly map position to a percentage between min and max
    double percentage = minPercentage + (maxPercentage - minPercentage) * (position - 1) / 99;

    if (position == 1) {return 1.02;}
    else if (position >= 2 && position <= 4){return (percentage / 10) + 1;}
    else if(position > 4 && position <= 12){return (percentage/10.3)+1;}
    else if(position > 12 && position <= 16){return (percentage/10)+0.9;}
    else{return (percentage/10)+0.8;}
    
}

Console.WriteLine(CalculatePercentageBasedOnPosition(4));


