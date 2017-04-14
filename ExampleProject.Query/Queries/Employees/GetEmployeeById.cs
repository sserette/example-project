using System;
using System.Data;
using System.Linq;
using MediatR;
using Dapper;

namespace ExampleProject.Query.Queries.Employees
{
    public class GetEmployeeById
    {
        public class EmployeeDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }
        public class Query : IRequest<EmployeeDto>
        {
            public Guid Id { get; set; }
        }
        public class Handler : IRequestHandler<Query, EmployeeDto>
        {
            private readonly IDbConnection _connection;
            public Handler(IDbConnection connection)
            {
                _connection = connection;
            }
            public EmployeeDto Handle(Query message)
            {
                var sql = CreateSql();
                return _connection.Query<EmployeeDto>(sql, new { @Id = message.Id }).FirstOrDefault();
            }
            private string CreateSql()
            {
                return @"SELECT * FROM [ExampleProject].[dbo].[Employee] WHERE Id = @Id";
            }
        }
    }
}
