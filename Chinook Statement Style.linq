<Query Kind="Statements">
  <Connection>
    <ID>fb7101e8-3e7e-455d-b0c3-79cd0b19ecb0</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

//use of the statement environment allows for C# type commands
//you can have local variables
//you can have multiple statements in your execution
//to display the contents of a variable you will use
//      the LinqPad method .Dump()
var theresults = from x in Albums
where x.ReleaseYear == 2008
orderby x.Artist.Name, x.Title
select new{
			x.Artist.Name,
			x.Title
			};
theresults.Dump();

//list all albums which contains the string "son"
//consider using the method .Contains()
var theList = from x in Albums
where x.Title.Contains("son")
select x;
theList.Dump();