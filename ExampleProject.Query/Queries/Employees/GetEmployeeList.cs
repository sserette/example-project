using System;
using System.Data;
using System.Linq;
using MediatR;
using Dapper;
using System.Collections.Generic;

namespace ExampleProject.Query.Queries.Employees
{
    public class GetEmployeeList
    {
        public class EmployeeDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class Query : IRequest<IEnumerable<EmployeeDto>>
        {
        }

        public class Handler : IRequestHandler<Query, IEnumerable<EmployeeDto>>
        {
            private readonly IDbConnection _connection;
            public Handler(IDbConnection connection)
            {
                _connection = connection;
            }
            public IEnumerable<EmployeeDto> Handle(Query message)
            {
                var sql = CreateSql();
                return _connection.Query<EmployeeDto>(sql);
            }
            private string CreateSql()
            {
                return @"SELECT * FROM [ExampleProject].[dbo].[Employee]";
            }
        }
    }
}
