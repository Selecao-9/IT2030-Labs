﻿using System.Reflection;

namespace Microsoft.AspNetCore.Builder
{

    public static class EndpointExtensions
    {

        public static IEndpointConventionBuilder MapEndpoint<T>(
                this IEndpointRouteBuilder app,
                string path, string methodName = "Endpoint")
        {

            MethodInfo? methodInfo = typeof(T).GetMethod(methodName);
            if (methodInfo?.ReturnType != typeof(Task))
            {
                throw new System.Exception("Method cannot be used");
            }

            ParameterInfo[] methodParams = methodInfo!.GetParameters();

            return app.MapGet(path, context => {
                T endpointInstance =
                    ActivatorUtilities.CreateInstance<T>
                        (context.RequestServices);
                return (Task)methodInfo.Invoke(endpointInstance!,
                    methodParams.Select(p =>
                    p.ParameterType == typeof(HttpContext)
                    ? context
                    : context.RequestServices.GetService(p.ParameterType))
                        .ToArray())!;
            });
        }
    }
}
