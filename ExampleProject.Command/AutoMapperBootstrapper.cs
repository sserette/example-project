using ExampleProject.Command.Commands;
using ExampleProject.Command.Models;

namespace ExampleProject.Command
{
    public static class AutoMapperBootstrapper
    {
        public static void Configure()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
            });
        }
    }
}
