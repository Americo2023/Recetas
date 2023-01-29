namespace Recetas.Data.Model
{
    public class AmountModel
    {
        public int amount_id { get; set; }
        public int recipe_id { get; set; }
        public int ingredient_id { get; set; }
        public int ingredient_measurement_id { get; set; }
        public float ingredient_amount { get; set; }
    }
}
