<Query Kind="Statements">
  <Connection>
    <ID>dc24ce84-1f58-4693-8ef1-f3d8a00d9708</ID>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//The retaurant Host (who is in charge of the waiters, seats people and takes payments) needs the following information:
//Show me the reservations slated for a given Year, Month and Day (ignoring the cancelled ones). 
//(For sample dates, choose May 29, 2014 and September 20, 1014). - Advanced: Group the reservations by hour of the day
from x in Reservations
//group Reserve by new {Reserve.ReservationDate} into tempdataset
where x. ReservationDate.Day == 20
&& x.ReservationDate.Year == 2014 
&& x.ReservationDate.Month == 03
&& x.ReservationStatus != 'C'
select new {
//ReservationID = tempdataset.Key.ReservationID,
//ReservationDate = tempdataset.Key.ReservationDate,
ReservationId = x.ReservationID,
CustomerName = x.CustomerName,
ReservationDate = x.ReservationDate,
NumberInParty = x.NumberInParty,
ContactPhone = x.ContactPhone,
ReservationStatus = x.ReservationStatus,
EventCode = x.EventCode,
SpecialEvents = x.SpecialEvents,
Bills = x.Bills,
ReservationTables = x.ReservationTables

//reser = tempdataset.ToList()
}

//DON'S ANSWER
from booking in Reservations
where booking.ReservationDate.Month == 9
   && booking.ReservationDate.Year == 2016
   & booking.ReservationDate.Day == 20
select booking

from booking in Reservations
where booking.ReservationDate.Month == 9
   && booking.ReservationDate.Year == 2016
   & booking.ReservationDate.Day == 20
group booking by booking.ReservationDate.Hour into block
select block


//The retaurant Host (who is in charge of the waiters, seats people and takes payments) needs the following information:
//Waiters with active customers (bills not paid) ordered by waiter.
from x in Bills
where x.PaidStatus.Equals("False")
select new {
             BillId = x.BillID,
			 Name = x.Waiter.FirstName + " " + x.Waiter.LastName,
			 Placed = x.OrderPlaced,
			 Ready = x.OrderReady,
			 Served = x.OrderServed
           }
//DON'S ANSWER
from mealBill in Bills
where !mealBill.PaidStatus
orderby mealBill.Waiter.LastName
select new
{
	BillId = mealBill.BillID,
	Name = mealBill.Waiter.FirstName + " " + mealBill.Waiter.LastName,
	Placed = mealBill.OrderPaid,
	Ready = mealBill.OrderReady,
	Served = mealBill.OrderServed
}
		   

//The waiters need the following information:
//Orders waiting to be served (sorted by table and showing the items on the order)

from x in BillItems
orderby x.Bill.TableID
where x.Bill.TableID > 0
     && x.Item.Active.Equals("True")
select new {
             TableId = x.Bill.TableID,
			 Decription = x.Item.Description,
			 Quantity = x.Quantity,
			 Notes = x.Notes
}

Tables
Items
MenuCategories
//The Waiters need the following information:
//A list of Active tables waiting to place an order
//Waiter and customer table(s) who have not yet ordered.
Bills
Tables
from x in Bills
where x.Table.Available.Equals("False")
select new {
            BillID = x.BillID,
			Name = x.Waiter.LastName + " " + x.Waiter.FirstName,
			TableID = x.TableID,
			NumberInParty = x.NumberInParty
}

//DON'S ANSWER
from customer in Bills
where !customer.OrderPlaced.HasValue
select new
{
	BillId = customer.BillID,
	Name = customer.Waiter.FirstName + " " + customer.Waiter.LastName,
	TableId = customer.TableID,
	NumberInParth = customer.NumberInParty
}

//The Kitchen Staff needs the following information:
//Items to prepare for orders that have been placed but are not ready, grouped by the table number(s) 
//and menu category description. Omit beverage items as the kitchen staff do not handle these items.

from x in BillItems
where x.Item.MenuCategory != "Beverage"
group x by new {x.Bill.TableID, x.Item.Description} //into tempdataset
//select new {