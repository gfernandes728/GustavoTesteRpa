using Microsoft.AspNetCore.Mvc;
using RpaWorker.Services;

namespace RpaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScrappingController
(
    IScrapingService scrapingService
) : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(scrapingService.GetAll());
}