// using static Google.Protobuf.Examples; //for person.proto
using Hr.Entities;

// See https://aka.ms/new-console-template for more information

Employee emp = new Employee()
{
    Id = 1,
    FirstName = "Mohamed",
    LastName = "Hassan"
};
EmployeeService empService = new EmployeeService(emp);
empService.SayHello();