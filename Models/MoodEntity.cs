public class MoodEntity
{
    public int Id { get; set; }
    public required string Feeling { get; set; }
    public int Intensity { get; set; }
    public string? Description { get; set; }
}