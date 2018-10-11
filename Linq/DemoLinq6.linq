<Query Kind="Expression" />

//Joins

//www.dotnetlearners.com/linq

//Rules:
//1. if you have a navigational property between tables
//	this should be your first choice pf attack
//2. if you do not have navigational property






from x in Albums 
join y in Artists on x.ArtistId equals y.ArtistId
select new 
{
	Title = x.Title,
	Year = x.ReleaseYear,
	Label = x.ReleaseLabel == null? "Unknown" : x.ReleaseLabel,
	Artist = y.Name,
	
}