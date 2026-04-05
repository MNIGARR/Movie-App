using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace MovieApp.API.Controllers;

public abstract class BaseController : ControllerBase
{
    protected int GetUserId() =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
}
