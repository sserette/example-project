using ExampleProject.Shared.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ExampleProject.WebApi.Decorators
{
    public class LoggingHandlerDecorator<TRequest, TResponse>
        : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _inner;
        private ILogger _logger;
        public LoggingHandlerDecorator(IRequestHandler<TRequest, TResponse> inner, ILogger logger)
        {
            _inner = inner;
            _logger = logger;
        }

        public TResponse Handle(TRequest request)
        {
            var requestName = request.GetType().DeclaringType.Name;
            var assemblyName = request.GetType().Assembly.GetName().Name;

            var timespan = Stopwatch.StartNew();

            try
            {
                var result = _inner.Handle(request);
                timespan.Stop();
                _logger.TraceApi(assemblyName, requestName, timespan.Elapsed);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "{0} {1}", assemblyName, requestName);
                throw;
            }

        }
    }
}