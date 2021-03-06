<Query Kind="Statements">
  <Connection>
    <ID>fdbde3a0-500e-4621-9e7f-6869263da26c</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//using the statement Language environment, we code complete
//c# statement which include the linq query and a recieving variable.
//the statements will end with the semi-colon(;)
//to se the contents of the receiving variable, tou will use the 
//		LinqPad command .Dump()
//.Dump() is NOT a C# method. It is a LinqPad method.
var results = from x in Albums
			  where x.Artist.Name.Contains("U2")
			  orderby x.ReleaseYear
			select new 
			{
				Title = x.Title,
				Year = x.ReleaseYear
			};
results.Dump();


//ternary operator
//condition(s) ? true value : false value
var results2 = from x in Albums
			  orderby x.ReleaseLabel
			  select new 
			  {
			  	title = x.Title,
				label = x.ReleaseLabel == null ? "unknown" : x.ReleaseLabel
			  };
results2.Dump();


//a list of albums show the title and decade of release. Albums from
//1970 to 79 are 70s; 1980 to 89are 80s, 1990 to 99 are 90s, 2000 + are modern
var results3 = from x in Albums
			   orderby x.Title
			   select new
			   {
			   	 Title = x.Title,
				 Year = x.ReleaseYear >= 1970 && x.ReleaseYear <= 1979 ? "70s" : 
				 (x.ReleaseYear >= 1980 && x.ReleaseYear <= 1989 ? "80s" :
				 (x.ReleaseYear >= 1990 && x.ReleaseYear <= 1999 ? "90s" : "Modern" ))
			   };
results3.Dump();


//situation : i need a value from a query that will be used in another future query
//create a list tracks showing the track name and whether the trackis longer that track
//average length or shorter on of the average
var resultaverage = (from x in Tracks
					select x.Milliseconds).Average();
					
//this query is using a value created by a previous query					
var results4 = from x in Tracks
			select new
			{
				song = x.Name,
				time = x.Milliseconds,
				Length = x.Milliseconds > resultaverage ? "Long":
					(x.Milliseconds < resultaverage ? "Short" : "Average")
			};
					
resultaverage.Dump();
results4.Dump();

//Aggregates 
//.Sum(),.Count(),.Min(),.Max(),.Average()
//aggregates MUST be done against a collections (0,1 or more rows)


//list all albums with Title, Artist name , and the number of tracks on that album
var results5 = from x in Albums
				select new 
				{
					title = x.Title,
					artist = x.Artist.Name,
					trackcount = x.Tracks.Count() 
				};
results5.Dump();

//create a list of artists with their albumscounts 
//ordered by descending album count.
var results6 = from x in Artists
				orderby x.Albums.Count() descending
				select new
				{
					Artist = x.Name,
					Albums = x.Albums.Count()
				};
results6.Dump();

//find the maximum number of albums for all artists
//who has the most albums
var results7 = (from x in Artists
				select x.Albums.Count()).Max();
//who is taht artist
var results7b = from x in Artists
				where x.Albums.Count() == results7
				select x;
var results7c = from x in Artists
				where x.Albums.Count() == (from y in Artists select y.Albums.Count()).Max()
				select x;
				
results7.Dump();
results7b.Dump();
results7c.Dump();


//produce a list of albums which have tracks showing their
//title, artist, number of tracks on albums, total price
//of all tracks, longest, shortest and average track 
var results8 = from x in Albums
				where x.Tracks.Count() > 0
				select new 
				{
					title = x.Title,
					artist = x.Artist.Name,
					NumberOfTracks = (from y in x.Tracks select y).Count(),
					methodnumbersoftracks = x.Tracks.Count(),
					cost = x.Tracks.Sum(y => y.UnitPrice),
					longestitem = x.Tracks.Max(y => y.Milliseconds),
					shortestitem = x.Tracks.Min(y => y.Milliseconds),
					averageitem = x.Tracks.Average(y => y.Milliseconds)
				};
results8.Dump();

//list all the playlist which have at least a track.
//show the playlist name, number of playlist tracks,
//the cost of the playlist and the total storage size for
//the playlist.
var results9 = from x in  Playlists
				where x.PlaylistTracks.Count() > 0
				select new
				{
					Name = x.Name,
					numberofplaylisttracks = x.PlaylistTracks.Count(),
					cost = x.PlaylistTracks.Sum(plt => plt.Track.UnitPrice),
					total = x.PlaylistTracks.Sum(plt => plt.Track.Bytes/1000)
				};
results9.Dump();











