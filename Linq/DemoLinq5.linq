<Query Kind="Expression">
  <Connection>
    <ID>fdbde3a0-500e-4621-9e7f-6869263da26c</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//grouping of data within itself
//a) by column
//b) by multiple column
//c) by an entity
//
//grouping can be save temporarily into 
//	a dataset at the data set can be report on
//	
//the grouping attribute is referred to as the .Key
//multiple attributes or entity use .Key.attributes

from x in Albums
group x by x.ReleaseYear into gYear
select gYear

from x in Albums
group x by x.ReleaseYear into gYear
select new
{
	year = gYear.Key,
	numberofalbumsforyear = gYear.Count(),
	albumandartist = from y in gYear
					 select new 
					{
						title = y.Title,
						artist = y.Artist.Name,
						albumsongcount = y.Tracks.Count()
					}
}

//grouping of tracks of Genre name
//actions agianst your dat BEFORE grouping
//is done again the natural entity attribute
//actions done AFTER grouping MUST refer to the
// 	temporary group dataset
//grouping can be done against a complete Entity
//this type of grouping produces a KEY set of ALL 
//	Entity attributes
from t in Tracks
where t.Album.ReleaseYear > 2010 
group t by t.Genre into gTemp
select gTemp
orderby gTemp.Count() descending
select new
{
	genre = gTemp.Key.Name,
	numberof = gTemp.Count()
}
