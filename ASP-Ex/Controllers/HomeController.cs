using ASP_Ex.Data;
using ASP_Ex.Models;
using ASP_Ex.Models.FrontendForm;
using ASP_Ex.Models.Home.Signup;
using ASP_Ex.Services.Hash;
using ASP_Ex.Services.Kdf;
using ASP_Ex.Services.RandomString;
using ASP_Ex.Data.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ASP_Ex.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly DataContext _dataContext;
		private readonly IKdfService _kdfService;
		private readonly DataAccessor _dataAccessor;

		private readonly IHashService _hashService;

		public HomeController(ILogger<HomeController> logger, DataContext dataContext, IKdfService kdfService, IHashService hashService, DataAccessor dataAccessor)
		{
			_logger = logger;
			_dataContext = dataContext;
			_kdfService = kdfService;
			_hashService = hashService;
			_dataAccessor = dataAccessor;
		}

		public IActionResult Index()
		{
            ViewData["users-count"] = _dataContext.Users.Count();
            return View();
		}
		
		public IActionResult Form(SingupFormModel? formModel)
		{
			SingupPageModel pageModel = new()
			{
				FormModel = formModel
			};
			if (formModel?.HasData ?? false)
			{
				pageModel.ValidationErrors = _ValidateSingupModel(formModel);
				if (pageModel.ValidationErrors.Count == 0)
				{
					String salt = RandomStringService.GenerateSalt(10);
					_dataAccessor.UserDao.Signup(new()
					{
						Name = formModel.UserName,
						Email = formModel.UserEmail,
						Salt = salt,
						Derivedkey = _kdfService.DerivedKey(salt, formModel.Password)
					});
				}
			}
			return View(pageModel);
		}

		public IActionResult Privacy()
		{
			return View();
		}

        public ViewResult Admin()
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

		private Dictionary<string, string> _ValidateSingupModel(SingupFormModel? model)
		{
			Dictionary<string, string> result = new();
			if (model == null)
			{
				result["model"] = "Model is Null";
			}
			else
			{
				if (String.IsNullOrEmpty(model.UserName))
				{
					result[nameof(model.UserName)] = "User Name should not be empty";
				}
				if (String.IsNullOrEmpty(model.UserEmail))
				{
					result[nameof(model.UserEmail)] = "User Email should not be empty";
				}
				if (!model.Agreement)
				{
					result[nameof(model.Agreement)] = "User Agreement must be checked";
				}
				if (String.IsNullOrEmpty(model.Password))
				{
					result[nameof(model.Password)] = "User Password should not be empty";
				}
				if (!String.IsNullOrEmpty(model.Password))
				{
					Regex regex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d).+$");
					if (!regex.IsMatch(model.Password))
						result[nameof(model.Password)] = "User Password should contain 1 symbol and 1 digit";
				}
				if (String.IsNullOrEmpty(model.UserRepeat))
				{
					result[nameof(model.UserRepeat)] = "User Repeat Password should not be empty";
				}
				if (!String.IsNullOrEmpty(model.Password))
				{
					if (!(model.UserRepeat == model.Password))
						result[nameof(model.UserRepeat)] = "User Passwords not the same";
				}
			}
			return result;
		}
	}
}
