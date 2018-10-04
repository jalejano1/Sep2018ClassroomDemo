<Query Kind="Expression">
  <Connection>
    <ID>fdbde3a0-500e-4621-9e7f-6869263da26c</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//all US customers 
from x in Customers
where x.Country.Equals("USA") &&
    x.Email.Contains("yahoo")
select new 
{
	FullName = x.LastName + ", " + x.FirstName,
	Email = x.Email
}

from x in Albums 
orderby x.Title
select new
{
	Title = x.Title,
	Year = x.ReleaseYear,
	ArtistName = x.Artist.Name
}

Artists

from x in Albums
where x.Artist.Name.Contains("U2")
orderby x.ReleaseYear
select new 
{
	Title = x.Title,
	Year = x.ReleaseYear
}





//
Albums_
	.select (rowPlaceholder => rowPlaceholder)
	
Customers