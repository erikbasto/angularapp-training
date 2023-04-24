﻿using System;
using System.Threading.Tasks;
using Api.Extensions;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

            var userId = resultContext.HttpContext.User.GetUserId();
            var unitOfWork = resultContext.HttpContext.RequestServices.GetRequiredService<IUnitOfWork>();
            var user = await unitOfWork.UserRepository.GetUserByIdAsync(userId);
            user.LastActive = DateTime.UtcNow;

            await unitOfWork.Complete();
        }
    }
}

