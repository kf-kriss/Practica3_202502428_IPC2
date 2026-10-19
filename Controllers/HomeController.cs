using Microsoft.AspNetCore.Mvc;

namespace Practica3_V2._1_IPC2.Controllers;

public sealed class HomeController : Controller
{
    [Route("Home/Error")]
    public IActionResult Error()
    {
        return View();
    }
}
