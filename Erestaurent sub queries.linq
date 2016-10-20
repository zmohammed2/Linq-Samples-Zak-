<Query Kind="Statements">
  <Connection>
    <ID>de726752-54de-42ee-b8a1-f46940af1bc3</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

var maxbills = (from x in Waiters
               select x.Bills.Count()).Max();
var BestWaiter = from x in Waiters
                 //where x.Bills.Count() == maxbills
				 select new{
				        Name = x.FirstName + "" + x.LastName,
						tbills = x.Bills.Count()};
BestWaiter.Dump();
	
//Create a dataset which contains the summary bills info by waiter
var WaiterBills = from x in Waiters
                  orderby x.LastName, x.FirstName
				  select new {
				       Name = x.LastName + "," + x.FirstName,
					   BillInfo = (from y in x.Bills 
					               where y.BillItems.Count() >0
					               select new{
								   BillID = y.BillID,
								   BillDate = y.BillDate,
								   TableID = y.TableID,
								   Total = y.BillItems.Sum(b => b.SalePrice * b.Quantity)
								             }   
					               )
				           };
WaiterBills.Dump();
