namespace Recetas.Data.Model
{
    public class StepRequest
    {
        public int? recipe_id { get; set; }
        public int? step_number { get; set; }
        public string? step_description { get; set; }
    }
}
