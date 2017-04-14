using System;
using MediatR;
using ExampleProject.Command.Models;

namespace ExampleProject.Command.Commands
{
    public class AddEmployee
    {
        public class Command : IRequest<Unit>
        {
            public string Name { get; set; }
            public Command(string name)
            {
                Name = name;
            }
        }
        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly ExampleProjectCommandContext _context;
            public Handler(ExampleProjectCommandContext context)
            {
                _context = context;
            }
            public Unit Handle(Command message)
            {
                var employee = new Employee();
                employee.Add(message.Name);
                _context.Employees.Add(employee);
                return Unit.Value;
            }
        }
    }
}
