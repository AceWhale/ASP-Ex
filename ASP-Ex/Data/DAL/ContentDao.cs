using ASP_Ex.Data.Entities;

namespace ASP_Ex.Data.DAL
{
	public class ContentDao
	{
		private readonly Object _dblocker;
		private readonly DataContext _context;
		public ContentDao(DataContext context, object dblocker)
		{
			_context = context;
			_dblocker = dblocker;
		}
		public void AddCategory(String name, String description, String? photoUrl, String? slug = null)
		{
            lock (_dblocker)
            {
                _context.Categories.Add(new()
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Description = description,
                    DeleteDt = null,
                    PhotoUrl = photoUrl,
                    Slug = slug ?? name
                });
                _context.SaveChanges();
            }
        }
		public List<Category> GetCategories()
		{
			List<Category> list;
			lock (_dblocker)
			{
				list = _context.Categories
				.Where(c => c.DeleteDt == null)
				.ToList();
			}
			return list;
		}
        public Category? GetCategoryBySlug(String slug)
        {
            Category? ctg;
            lock (_dblocker)
            {
                ctg = _context.Categories.FirstOrDefault(c => c.Slug == slug);
            }
            return ctg;
        }
        public void UpdateCategory(Category category)
		{
			var ctg = _context
				.Categories
				.Find(category.Id);
			if (ctg != null)
			{
				ctg.Name = category.Name;
				ctg.Description = category.Description;
				ctg.DeleteDt = category.DeleteDt;
				_context.SaveChanges();
			}
		}
		public void DeleteCategory(Guid id)
		{
			var ctg = _context
				.Categories
				.Find(id);
			if (ctg != null)
			{
				ctg.DeleteDt = DateTime.Now;
				_context.SaveChanges();
			}
		}
		public void DeleteCategory(Category category)
		{
			DeleteCategory(category.Id);
		}
		
	}
}
