using ASP_Ex.Data.DAL;
using ASP_Ex.Data.Entities;
using ASP_Ex.Services.RandomString;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Ex.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataAccessor _dataAccessor;
        public ProductController(DataAccessor dataAccessor)
        {
            _dataAccessor = dataAccessor;
        }

        [HttpGet("{id}")]
        public List<Product> DoGet(String id)
        {
            return _dataAccessor.ContentDao.GetProducts(id);
        }

        [HttpPost]
        public String Post([FromForm] ProductPostModel model)
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
                        fileName = RandomStringService.GenerateFilename(10) + ext;
                        pathName = path + fileName;
                    }
                    while (System.IO.File.Exists(pathName));

                    using var steam = System.IO.File.OpenWrite(pathName);
                    model.Photo.CopyTo(steam);
                }
                _dataAccessor.ContentDao.AddProduct(
                    name: model.Name,
                    description: model.Description,
                    CategoryId: model.CategoryId,
                    CompanyId: model.CompnayId,
                    Stars: model.Stars,
                    count: model.Count,
                    price: model.Price,
                    PhotoUrl: fileName);
                Response.StatusCode = StatusCodes.Status201Created;
                return "OK";
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return "Error";
            }
        }
    }

    public class ProductPostModel
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public Guid CategoryId { get; set; }
        public Guid CompnayId { get; set; }
        public int Stars { get; set; }
        public Double Price { get; set; }
        public int Count { get; set; }
        public IFormFile Photo { get; set; }
    }
}
