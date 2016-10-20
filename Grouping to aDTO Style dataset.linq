<Query Kind="Expression">
  <Connection>
    <ID>ca73f730-358e-46b1-a79a-0a7c92a74dd4</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//multi Column group
//grouping data placed in a local temp dataset for further processing 
//.Key Allows you to have access to the values in your group Keys
//if you have multiple group columns they must be in an anonymous datatype
//to create a DTO type collection you can use .ToList() on the tempdata set
//you can have a custom anonymous data collection by using a nested query

//step A
from food in Items
  group food by new { food.MenuCategoryID, food.CurrentPrice} 
  
  //Step b
  from food in Items
  group food by new { food.MenuCategoryID, food.CurrentPrice} into tempdataset
  select new{
             MenuCategoryID = tempdataset.Key.MenuCategoryID,
			 CurrentPrice = tempdataset.Key.CurrentPrice,
			 FoodItems = tempdataset.ToList()
						 
  }
  
  //Step C
  from food in Items
  group food by new { food.MenuCategoryID, food.CurrentPrice} into tempdataset
  select new{
             MenuCategoryID = tempdataset.Key.MenuCategoryID,
			 CurrentPrice = tempdataset.Key.CurrentPrice,
			 FoodItems = from x in tempdataset
			             select new {
						           ItemID = x.ItemID,
								   FoodDescription = x.Description,
								   TimesServed = x.BillItems.Count()
						 }
  }