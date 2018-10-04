<Query Kind="Program">
  <Connection>
    <ID>fdbde3a0-500e-4621-9e7f-6869263da26c</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

void Main()
{
	//Create a list of albums showing its title and artist.
	//Show albums with 15 or more tracks only.
	//For each album show the songs on the albums and their length 	
	var results = from x in Albums
				  where x.Tracks.Count() > 24
				  select new AnAlbum
				  {
				  	artist = x.Artist.Name,
					title = x.Title,
					songs = (from y in x.Tracks
							 select new Songs
							{
								songname = y.Name,
								length = y.Milliseconds/60000 + "," + (y.Milliseconds%60000)/1000
							}).ToList()
				  };
	results.Dump();
	
	//create a list of employees and the client they support 
	var employeelist = from x in Employees
						where x.Title.Contains("Support")
						orderby x.LastName, x.FirstName
						select new SupportEmployee
						{
							Name = x.LastName + ", " + x.FirstName,
							ClientCount = x.SupportRepIdCustomers.Count(),
							ClientList = (from y in x.SupportRepIdCustomers
										 orderby y.LastName, y.FirstName
										 select new PlaylistCustomer
										 {
										 	lastname = y.LastName,
											firstname = y.FirstName,
											phone = y.Phone
										 }).ToList()
						};
	employeelist.Dump();
}
// Define other methods and classes here
//Songs
public class Songs
{
	public string songname {get; set;}
	public string length {get; set;}
}
public class PlaylistCustomer
{
	public string lastname {get; set;}
	public string firstname {get; set;}
	public string phone {get; set;}
}
public class AnAlbum
{
	public string artist {get; set;}
	public string title {get; set;}
	public List<Songs> songs{get; set;}
}
public class SupportEmployee
{
	public string Name {get; set;}
	public int ClientCount {get; set;}
	public List<PlaylistCustomer> ClientList {get; set;}
}
