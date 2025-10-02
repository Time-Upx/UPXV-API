using Microsoft.AspNetCore.Mvc;
using UPXV.Services;

namespace UPXV_API.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class ItemController : ControllerBase
{
   private ItemService _service;
   public ItemController (ItemService service)
   {
      _service = service;
   }

   [HttpGet]
   public IActionResult List (int page = 0, int size = 5) => Ok(_service.List(page, size));
}