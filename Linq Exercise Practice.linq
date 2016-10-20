<Query Kind="Statements">
  <Connection>
    <ID>ecfd8a0a-5aed-4191-b5cb-8936bea417a1</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>WorkSchedule</Database>
  </Connection>
</Query>

//1.Show all skills requiring a ticket and which employees have those skills. Include all the data as seen in the following image.
from x in Skills
where x.RequiresTicket
group x by new {x.SkillID, x.Description} into tempdataset
select new {
            Description = tempdataset.Key.Description,
			Employees = from y in EmployeeSkills
						where y.SkillID == tempdataset.Key.SkillID
			  			select new {
			               Name = y.Employee.FirstName + ' ' + y.Employee.LastName,
						   level = y.Level,
						   YearsOfExperience = y.YearsOfExperience
			             }
			}
			
//2.List all skills, alphabetically, showing only the description of the skill.
from x in Skills
orderby x.Description
select new {
       Description = x.Description
}




//5. List all the employees with the most years of experience.
var maxYearsOfExp = (from x in EmployeeSkills
                      select x.YearsOfExperience).Max();
//maxYearsOfExp.Dump();

var BestEmployee = from x in EmployeeSkills
                   where x.YearsOfExperience == maxYearsOfExp
				   select new {
				               Name = x.Employee.FirstName + " " + x. Employee.LastName
				};
BestEmployee.Dump();




			 
//4. From the shifts scheduled for NAIT's placement contract, show the number of employees needed for each day 
//(ordered by day-of-week). Bonus: display the name of the day of week (first day being Monday).



var days = from x in Shifts
           where x.PlacementContract.Location.Name.Contains("NAIT")
           orderby x.DayOfWeek
           group x by new {x.DayOfWeek} into tempdataset
    select new {
             //Day = Enum.GetName(typeof(DayOfWeek),tempdataset.Key).Substring(0,3),
			 Day = tempdataset.K.ey.DayOfWeek,
			 NumberOfPeople = tempdataset.Sum( y=> y.NumberOfEmployees)
};
	
						 


days.Dump();


//3.List all the skills for which we do not have any qualfied employees.
from x in Skills
where x.EmployeeSkills.Count() == 0
select new {
     Description = x.Description,
	 Id = x.SkillID
}