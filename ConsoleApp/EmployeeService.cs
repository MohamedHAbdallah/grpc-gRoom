using System;
using Google.Protobuf;

using Grpc.Course.Protobuf.Test;

public class EmployeeService
{
    private Employee emp = new Employee();
    public EmployeeService(Employee emp)
    {
        if (emp is null)
            this.emp = ReadEmployee();
        else
        {
            this.emp = emp;
            WriteEmployee();
        }
    }

    private void WriteEmployee()
    {
        using (var output = File.Create("emp.dat"))
        {
            emp.WriteTo(output);
        }
    }

    private Employee ReadEmployee()
    {
        using (var input = File.OpenRead("emp.dat"))
        {
            emp = Employee.Parser.ParseFrom(input);
        }
        return emp;
    }

    private string GetFullName()
    {
        return string.Format("{0} {1}", emp.FirstName, emp.LastName);
    }

    public void SayHello()
    {
        string fullName = GetFullName();
        Console.WriteLine("Hello " + fullName);
    }
}
