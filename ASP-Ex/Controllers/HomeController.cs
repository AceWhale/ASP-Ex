using ASP_Ex.Data;
using ASP_Ex.Models;
using ASP_Ex.Models.FrontendForm;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASP_Ex.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly DataContext _dataContext;

        public HomeController(ILogger<HomeController> logger, DataContext dataContext)
		{
			_logger = logger;
            _dataContext = dataContext;
        }

		public IActionResult Index()
		{
            ViewData["users-count"] = _dataContext.Users.Count();
            return View();
		}
		
		public IActionResult Form()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}


        [HttpPost]     
        public JsonResult FrontendFrom([FromBody] FrontendFormInput input)
        {
            FrontendFormOutput output = new()
            {
                Code = 200,
                Message = $"{input.UserName} -- {input.UserEmail}"
            };
            _logger.LogInformation(output.Message);
            return Json(output);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
