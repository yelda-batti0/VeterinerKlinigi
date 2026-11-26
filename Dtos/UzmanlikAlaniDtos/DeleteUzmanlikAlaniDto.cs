namespace VeterinerKlinigi.Dtos.UzmanlikAlaniDtos
{
    public class DeleteUzmanlikAlaniDto
    {
        public int UzmanlikAlaniId { get; set; }
        public string Ad { get; set; } = string.Empty;
        public int VeterinerSayisi { get; set; }
    }
}