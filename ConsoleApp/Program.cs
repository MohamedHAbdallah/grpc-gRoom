
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;

using Grpc.Course.Protobuf.Test;

// See https://aka.ms/new-console-template for more information

DateTime birthDate = new DateTime(1992, 1, 10);
birthDate = DateTime.SpecifyKind(birthDate, DateTimeKind.Utc);
Employee emp = null;
/*new Employee()
{
    Id = 1,
    FirstName = "Mohamed",
    LastName = "Hassan",
    IsRetired = false,
    BirthDate = Timestamp.FromDateTime(birthDate),
    //MaritalStatus = Employee.Types.MaritalStatus.Married,
    CurrentAddress = new Address() { City = "Giza", HouseNumber = 282, StreetName = "Villa Shams", ZipCode = "11511" }
};
emp.PreviousEmployers.Add("Efinanace");
emp.PreviousEmployers.Add("Link Dev");
*/
EmployeeService empService = new EmployeeService(emp);
empService.SayHello();