namespace Recetas.Data.Model
{
    public class RecipeModel
    {
        public int recipe_id { get; set; }
        public int? maltid_id { get; set; }
        public int? category_id { get; set; }
        public string? recipe_name { get; set; }
        public string? recipe_description { get; set; }
        public string? prep_time { get; set; }
        public string? recipe_img { get; set; }
    }
}
