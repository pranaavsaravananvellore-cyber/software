namespace SensoreAPPMVC.Models
{
    
        public class Heatmap
        {
            public int Id { get; set; }

            // Foreign key to Patient
            public int PatientId { get; set; }
            public Patient Patient { get; set; }

            // Order of heatmaps in the file
            public int SequenceNumber { get; set; }

            // When it was recorded or parsed
            public DateTime Timestamp { get; set; }

            // Navigation property for values
            public List<HeatmapValue> Values { get; set; }
        }

    }

