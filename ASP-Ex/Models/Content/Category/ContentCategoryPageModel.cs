namespace ASP_Ex.Models.Content.Category
{
    public class ContentCategoryPageModel
    {
        public Data.Entities.Category Category { get; set; } = null!;
        public List<Data.Entities.Product> Products { get; set; } = [];

    }
}
