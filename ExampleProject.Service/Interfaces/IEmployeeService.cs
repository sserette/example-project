using System;
using ExampleProject.Service.Dtos;
using ExampleProject.Command.Commands;

namespace ExampleProject.Service.Interfaces
{
    public interface IEmployeeService
    {
        void AddEmployee(AddEmployeeDto request);
    }
}
