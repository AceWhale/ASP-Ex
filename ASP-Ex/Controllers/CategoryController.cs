using ASP_Ex.Data.DAL;
using ASP_Ex.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Ex.Controllers
{
	[Route("api/category")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly DataAccessor _dataAccessor;
		public CategoryController(DataAccessor dataAccessor)
		{
			_dataAccessor = dataAccessor;
		}

		[HttpGet]
		public List<Category> DoGet()
		{
			return _dataAccessor.ContentDao.GetCategories();
		}

		[HttpPost]
		public String DoPost([FromForm] CategoryPostModel model)
		{
			try
			{
				String? fileName = null;
				if (model.Photo != null)
				{
					string ext = Path.GetExtension(model.Photo.FileName);
					String path = Directory.GetCurrentDirectory() + "/wwwroot/images/content/";
					String pathName;
					do
					{
						fileName = Guid.NewGuid() + ext;
						pathName = path + fileName;
					}
					while (System.IO.File.Exists(pathName));
					using var steam = System.IO.File.OpenWrite(pathName);
					model.Photo.CopyTo(steam);
				}
				_dataAccessor.ContentDao.AddCategory(model.Name, model.Description, fileName);
				Response.StatusCode = StatusCodes.Status201Created;
				return "OK";
			}
			catch (Exception ex)
			{
				Response.StatusCode = StatusCodes.Status400BadRequest;
				return "ERROR";
			}

		}

        [HttpDelete("{id}")]
        public String DoDelete(Guid id)
        {
            _dataAccessor.ContentDao.DeleteCategory(id);
            Response.StatusCode = StatusCodes.Status202Accepted;
            return "OK";
        }

        public String DoOther()
        {
            // дані про метод запиту - у Request. Method
            if (Request.Method == "RESTORE")
            {
                return DoRestore();
            }
            Response.StatusCode = StatusCodes.Status405MethodNotAllowed;
            return "Method not Allowed";
        }
        // Другий НЕ позначений метод має бути private щоб не було конфлікту
        private String DoRestore()
        {
            // Через відсутність атрибутів, автоматичного зв'язування параметрі
            // немає, параметри дістаємо з колекцій Request
            String? id = Request.Query["id"].FirstOrDefault();
            try
            {
                _dataAccessor.ContentDao.RestoreCategory(Guid.Parse(id!));
            }
            catch
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return "Empty or invalid id";
            }
            Response.StatusCode = StatusCodes.Status202Accepted;
            return "RESTORE works with id: " + id;
        }
    }
	public class CategoryPostModel
	{
        [FromForm(Name = "category-name")]
        public String Name { get; set; }


        [FromForm(Name = "category-description")]
        public String Description { get; set; }


        [FromForm(Name = "category-slug")]
        public String Slug { get; set; }


        [FromForm(Name = "category-photo")]
        public IFormFile? Photo { get; set; }
    }
}
