﻿using System.Linq;
using MediatR;
using FluentValidation;

namespace ExampleProject.WebApi.Decorators
{
    public class ValidationHandlerDecorator<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _inner;
        private readonly IValidator<TRequest>[] _validators;
        public ValidationHandlerDecorator(IRequestHandler<TRequest, TResponse> inner, IValidator<TRequest>[] validators)
        {
            _inner = inner;
            _validators = validators;
        }
        public TResponse Handle(TRequest request)
        {
            var context = new ValidationContext(request);
            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();
            if(failures.Any())
            {
                throw new ValidationException(failures);
            }
            return _inner.Handle(request);
        }
    }
}