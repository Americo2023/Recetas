namespace Recetas.Data.Model
{
    public class AmountResponse
    {
        public string? recipe_name { get; set; }
        public string? ingerdient_name { get; set; }
        public string? measurement_name { get; set; }
        public float ingredient_amount { get; set; }
    }
}
