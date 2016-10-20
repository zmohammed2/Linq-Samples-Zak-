<Query Kind="Statements">
  <Connection>
    <ID>fb7101e8-3e7e-455d-b0c3-79cd0b19ecb0</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

// Sample for entity subset
//Sample of entity navigation from child to Parent on Where
//Remainder that code is C3 and thus appropiate methods can be used .Equals()
from x in Customers
where x.SupportRepIdEmployee.FirstName.Equals("Jane") && 
x.SupportRepIdEmployee.LastName.Equals("Peacock")
select new{
        Name = x.LastName + "," + x.FirstName,
		City = x.City,
		State = x.State,
		Phone = x. Phone,
		Email = x. Email
}

//use of Aggregiates in queries
//count() count the number of instances of the collection refrence
//Sum() totals a specific field/expression, thus you will likely need to use a delegate
// to indicate the collection instance attribute to be used.
//Average() Averages a specific field/expression, thus you will likely need to use a delegate
// to indicate the collection instance attribute to be used.
from x in Albums
orderby x.Title 
where x.Tracks.Count() > 0 
select new{
        Title = x.Title,
		NumberOfAlbumTracks = x.Tracks.Count(),
		TotalAlbumPrice = x.Tracks.Sum(y => y.UnitPrice),
		//does overall avg and divides the average
		AverageTrackLengthInSecondsA = (x.Tracks.Average(y => y.Milliseconds))/1000,
		//dividing each instance
		AverageTrackLengthInSecondsB = x.Tracks.Average(y => y.Milliseconds/1000)
}

//when you need to use multiple steps 
//to solve the problem, sw
var maxcount =(from x in MediaTypes
select x.Tracks.Count()).max();

//to display the contents of variable in LinqPad
//you use the method.Dump()

maxcount.Dump();
//use a value in a preceeding create variable
var popularMediaType = from x in MediaTypes
where x.Tracks.Count() == maxcount
select new {
            Type = x.Name,
			Tcount = x.Tracks.Count()
};
PopularMediaType.Dump();

//Can this set of statements be been as one complete query
//the answer is possibly, and in this case yes
//in this example maxcoun could be exchange for the query that 
//actually cfeated the val

//using the method syntax to determine the count value for the where expression
//this demonstrate that que
var popularMediaTypeSubMethod = from x in MediaTypes
where x.Tracks.Count() == MediaTypes.Select (mt => mt.Tracks.Count()).Max()
select new{
            Type = x.Name,
			TCount = x.Tracks.Count()
};
PopularMediaTypeSubMethod.Dump();

