using BulkyWeb.Models;

namespace BulkyWeb.ViewModels
{
    public class CategoryViewData
    {
        public List<Category>? Categories { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
    }
}
