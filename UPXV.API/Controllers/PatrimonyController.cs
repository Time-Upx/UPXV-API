﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using UPXV.DTOs.Patrimonies;
using UPXV.Models;
using UPXV.Services;
using UPXV.Services.Exceptions;

namespace UPXV_API;

[ApiController]
[Route("[controller]")]
public sealed class PatrimonyController : ControllerBase
{
   private PatrimonyService _service;
   public PatrimonyController (PatrimonyService service)
   {
      _service = service;
   }

   [HttpGet]
   public IActionResult List (int page, int size) => Ok(_service.List(page, size));

   [HttpGet("{nid}")]
   public IActionResult Get (int nid) => _service.Get(nid).Either(Ok,
      problem => problem switch
      {
         EntityNotFoundException<Patrimony> e => NotFound(e.Message),
         Exception e => Problem(e.Message)
      });

   [HttpPost]
   public IActionResult Create (PatrimonyCreateDTO dto) => _service.Create(dto).Either(Ok,
      problem => problem switch
      {
         ValidationException e => BadRequest(e.Errors),
         Exception e => Problem(e.Message)
      });

   [HttpPut]
   public IActionResult Update (PatrimonyUpdateDTO dto) => _service.Update(dto).Either(Ok,
      problem => problem switch
      {
         ValidationException e => BadRequest(e.Errors),
         EntityNotFoundException<Patrimony> e => NotFound(e.Message),
         Exception e => Problem(e.Message),
      });

   [HttpDelete("{nid}")]
   public IActionResult Delete (int nid) => _service.Delete(nid).Either(Ok,
      problem => problem switch
      {
         EntityNotFoundException<Patrimony> e => NotFound(e.Message),
         Exception e => Problem(e.Message)
      });
}