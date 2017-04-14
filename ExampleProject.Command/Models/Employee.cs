using System;
using System.ComponentModel.DataAnnotations;

namespace ExampleProject.Command.Models
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Employee()
        {
            Id = Guid.NewGuid();
        }
        public void Add(string name)
        {
            Name = name;
        }
    }
}
