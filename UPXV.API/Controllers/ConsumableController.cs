using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using UPXV.Common.Page;
using UPXV.DTOs;
using UPXV.DTOs.Consumables;
using UPXV.Models;
using UPXV.Services;
using UPXV.Services.Exceptions;

namespace UPXV_API.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class ConsumableController : ControllerBase
{
   private ConsumableService _service;
   public ConsumableController (ConsumableService service)
   {
      _service = service;
   }

   [HttpGet]
   public IActionResult List (int page = 0, int size = 5) => Ok(_service.List(page, size));

   [HttpGet("{nid}")]
   public IActionResult Get (int nid) => _service.Get(nid).Either( Ok,
      problem => problem switch
      {
         EntityNotFoundException<Consumable> e => NotFound(e.Message),
         Exception e => Problem(e.Message)
      });

   [HttpPost]
   public IActionResult Create (ConsumableCreateDTO dto) => _service.Create(dto).Either( Ok,
      problem => problem switch
      {
         ValidationException e => BadRequest(e.Errors),
         Exception e => Problem(e.Message)
      });

   [HttpPut]
   public IActionResult Update (ConsumableUpdateDTO dto) => _service.Update(dto).Either( Ok,
      problem => problem switch
      {
         ValidationException e => BadRequest(e.Errors),
         EntityNotFoundException<Consumable> e => NotFound(e.Message),
         Exception e => Problem(e.Message),
      });

   [HttpDelete("{nid}")]
   public IActionResult Delete (int nid) => _service.Delete(nid).Either( Ok,
      problem => problem switch
      {
         EntityNotFoundException<Consumable> e => NotFound(e.Message),
         Exception e => Problem(e.Message)
      });
}