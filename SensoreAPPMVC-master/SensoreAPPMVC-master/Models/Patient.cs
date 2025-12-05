using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SensoreAPPMVC.Models;

public class Patient : Models.User
{
    public int clinicianId { get; set; }

    public List<Heatmap> Heatmaps { get; set; } = new List<Heatmap>();
    public bool CompletedRegistration { get; set;}
}