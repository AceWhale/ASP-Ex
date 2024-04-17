using ASP_Ex.Data.DAL;
using ASP_Ex.Models.Content.Index;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Ex.Controllers
{
	public class ContentController(DataAccessor dataAccessor) : Controller
	{
		private readonly DataAccessor _dataAccessor = dataAccessor;

		public IActionResult Index()
		{
			ContentIndexPageModel model = new()
			{
				Categories = _dataAccessor.ContentDao.GetCategories()
			};
			return View(model);
		}

		public IActionResult Category([FromRoute] Guid id)
		{
			return View();
		}
	}
}
