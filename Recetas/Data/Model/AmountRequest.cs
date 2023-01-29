namespace Recetas.Data.Model
{
    public class AmountRequest
    {
        public int? recipe_id { get; set; }
        public int? ingredient_id { get; set; }
        public int? ingredient_measurement_id { get; set; }
        public float? ingredient_amount { get; set; }
    }
}
