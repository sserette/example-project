using System;
using MediatR;
using System.Web.Http;
using ExampleProject.Service.Interfaces;
using ExampleProject.Service.Dtos;
using ExampleProject.Command.Commands;
using ExampleProject.Query.Queries.Employees;

namespace ExampleProject.WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IMediator mediator, IEmployeeService employeeService)
        {
            _mediator = mediator;
            _employeeService = employeeService;
        }
        [HttpGet]
        [Route("api/employee/getById/{employeeId}")]
        public IHttpActionResult GetEmployeeById(Guid employeeId)
        {
            var query = new GetEmployeeById.Query()
            {
                Id = employeeId
            };
            var result = _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet]
        [Route("api/employee/search")]
        public IHttpActionResult GetEmployeeList()
        {
            var query = new GetEmployeeList.Query();
            var result = _mediator.Send(query);
            return Ok(result);
        }
        [HttpPost]
        [Route("api/employee/addEmployee")]
        public IHttpActionResult AddEmployee([FromBody] AddEmployeeDto request)
        {
            _employeeService.AddEmployee(request);
            return Ok();
        }
    }
}