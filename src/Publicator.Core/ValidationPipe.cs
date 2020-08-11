using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Publicator.Core
{
    class ValidationPipe<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private IEnumerable<IValidator<TRequest>> _validators { get; set; }
        private ILogger<ValidationPipe<TRequest, TResponse>> _logger{ get; set; }
        public ValidationPipe(
            IEnumerable<IValidator<TRequest>> validators, 
            ILogger<ValidationPipe<TRequest, TResponse>> logger
            )
        {
            _logger = logger;
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (!(request is IBaseRequest))
                return await next();

            var context = new ValidationContext(request);
            
            var errors = _validators
                .Select(x => x.Validate(request))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .ToList();

            if (errors.Count != 0)
                throw new FluentValidation.ValidationException(errors);

            return await next();
        }
    }
}
