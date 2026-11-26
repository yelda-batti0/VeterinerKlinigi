namespace VeterinerKlinigi.Entities
{
    public class UzmanlikAlani
    {
        public int UzmanlikAlaniId { get; set; }
        public string Ad { get; set; } = string.Empty;

        public List<Veteriner> Veterinerler { get; set; } = new();
    }
}