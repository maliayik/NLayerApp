﻿using Core;
using Core.DTOs;
using Core.Services;
using Core.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web
{
    public class NotFoundFilter<T>:IAsyncActionFilter where T :BaseEntity
    {
        private readonly IService<T> _service;

        public NotFoundFilter(IService<T> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault();
            if (idValue == null)
            {
                await next.Invoke();
                return;
            }

            var id = (int)idValue;
            var anyEntity = await _service.AnyAsync(x => x.Id == id);

            if (anyEntity)
            {
                await next.Invoke();
                return;
            }
            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404, $"{typeof(T).Name}({id}) not found"));


            var errorViewModel = new ErrorViewModel();
            errorViewModel.Errors.Add($" {typeof(T).Name}({id}) not found");
            context.Result = new RedirectToActionResult("error", "Home", errorViewModel);

        }
    }
}
