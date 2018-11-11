using System;
using System.Linq;
using FluentAssertions;
using PipelineFun.Web.Contracts;
using PipelineFun.Web.Extensions;
using Xunit;

namespace PipelineFun.Web.Tests
{
    public class RequestHandlerTests
    {
        [Fact]
        public void All_requests_have_matching_handlers()
        {
            var assembly = typeof(Program).Assembly;
            var types = assembly.ExportedTypes.ToList();
            var requestTypes = types.Where(t => t.Implements<IRequestBase>() && t != typeof(IRequest<>) && t != typeof(IRequest));

            Type GetHandlerType(Type requestType)
            {
                if (requestType.Implements<IRequest>()) {
                    return typeof(IRequestHandler<>).MakeGenericType(requestType);
                }

                var genericRequestType = requestType.GetInterfaces()
                    .Single(i => i.Implements<IRequestBase>() && i.GetGenericArguments().Length == 1);
                var resultType = genericRequestType.GenericTypeArguments.Single();
                return typeof(IRequestHandler<,>).MakeGenericType(requestType, resultType);
            }

            foreach (var requestType in requestTypes) {
                var handlerType = GetHandlerType(requestType);
                var matchingTypes = types.Where(t => handlerType.IsAssignableFrom(t)).ToList();
                matchingTypes.Count.Should().Be(1, $"{requestType.Name} should have exactly one handler");
            }
        }
    }
}