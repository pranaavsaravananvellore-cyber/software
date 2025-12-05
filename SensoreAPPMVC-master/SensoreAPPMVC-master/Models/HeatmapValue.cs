using SensoreAPPMVC.Models;

public class HeatmapValue
{
    public int Id { get; set; }

    public int HeatmapId { get; set; }
    public Heatmap Heatmap { get; set; }

    // Position in matrix
    public int Row { get; set; }
    public int Col { get; set; }

    // Only non-zero values stored
    public int Value { get; set; }
}
