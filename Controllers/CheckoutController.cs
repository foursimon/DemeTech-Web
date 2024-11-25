using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemeTech.Controllers
{
    public class CheckoutController : Controller
    {
        [Authorize]
        public IActionResult Pagamento()
        {
            return View();
        }
    }
}
