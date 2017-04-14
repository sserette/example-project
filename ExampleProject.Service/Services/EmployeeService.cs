using System;
using MediatR;
using ExampleProject.Service.Interfaces;
using ExampleProject.Service.Dtos;
using ExampleProject.Command;
using ExampleProject.Command.Commands;

namespace ExampleProject.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMediator _mediator;
        private readonly ExampleProjectCommandContext _context;
        public EmployeeService(IMediator mediator, ExampleProjectCommandContext context)
        {
            _mediator = mediator;
            _context = context;
        }
        public void AddEmployee(AddEmployeeDto request)
        {
            _context.BeginTransaction();
            _mediator.Send(new AddEmployee.Command(request.Name));
            _context.CloseTransaction();
        }
    }
}
