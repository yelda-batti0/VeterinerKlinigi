namespace VeterinerKlinigi.Dtos.MusaitlikDtos
{
    public class DeleteMusaitlikDto
    {
        public int MusaitlikId { get; set; }
        public int VeterinerId { get; set; }
        public DayOfWeek Gun { get; set; }
        public TimeSpan BaslangicSaati { get; set; }
        public TimeSpan BitisSaati { get; set; }
    }
}