namespace Recetas.Data.Model
{
    public class StepModel
    {
        public int step_id { get; set; }
        public int recipe_id { get; set; }
        public int step_number { get; set; }
        public string? step_description { get; set; }
    }
}
