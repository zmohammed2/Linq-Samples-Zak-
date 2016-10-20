<Query Kind="Program" />

void Main()
{
//List of bill counts for all waiters
//This query will create a flat dataset
//The columns are native data types (ie int, string ,---)
//one is not concered with repeated data in a column
//Instead of using an annoymous datatype(new{...})
//we wish to use  adefined class definition
	 var BestWaiter = from x in Waiters
               
			   select new WaiterBillCounts{
				        Name = x.FirstName + "" + x.LastName,
						TCount = x.Bills.Count()};
BestWaiter.Dump();

var paramMonth = 4;
var paramYear = 2014;
var waiterBills = from x in Waiters
                  where x.LastName.Contains("K")
                  orderby x.LastName, x.FirstName
				  select new WaiterBills {
				       Name = x.LastName + "," + x.FirstName,
					   BillInfo = (from y in x.Bills 
					               where y.BillItems.Count() >0
								   && y.BillDate.Month ==DateTime.Today.Month - paramMonth
								   && y.BillDate.Year == paramYear
					               select new BillItemSummary{
								   BillID = y.BillID,
								   BillDate = y.BillDate,
								   TableID = y.TableID,
								   Total = y.BillItems.Sum(b => b.SalePrice * b.Quantity)
								             }   
					               ).ToList()
				           };
waiterBills.Dump();
}

// Define other methods and classes here
// an example of a POCO class
public class WaiterBillCounts
{
   //Whatever receiving field on your query in your select 
   //appears as a property in this class
   public string Name{get;set;}
   public int TCount{get;set;}
}

public class BillItemSummary
{
   public int BillID{get;set;}
   public DateTime BillDate{get;set;}
   public int? TableID {get;set;}
   public decimal Total{get;set;}
}

//An example of DTO class 
public class WaiterBills
{
  public string Name{get;set;}
  public int TotalBillCount{get;set;}
  public List<BillItemSummary> BillInfo{get;set;}
}

