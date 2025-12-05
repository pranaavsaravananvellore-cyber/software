using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SensoreAPPMVC.Data;
using SensoreAPPMVC.Models;

namespace SensoreAPPMVC.Services
{
    public class HeatmapStorageService
    {
        private readonly AppDBContext _context;

        public HeatmapStorageService(AppDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Parses, splits, and stores all 32×32 heatmaps in a CSV.
        /// </summary>
        public async Task StoreHeatmapsFromCsvAsync(int patientId, Stream csvStream)
        {
            using var reader = new StreamReader(csvStream);

            var allRows = new List<int[]>();

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var values = ParseCsvRow(line);
                if (values != null)
                {
                    allRows.Add(values);
                }
            }

            // Number of full heatmaps inside the file
            int totalHeatmaps = allRows.Count / 32;

            for (int index = 0; index < totalHeatmaps; index++)
            {
                int offset = index * 32;
                var grid = allRows.GetRange(offset, 32);

                var heatmap = new Heatmap
                {
                    PatientId = patientId,
                    Timestamp = DateTime.UtcNow.AddSeconds(index), // chronological ordering
                    Values = new List<HeatmapValue>()
                };

                // Store only non-zero values
                for (int row = 0; row < 32; row++)
                {
                    for (int col = 0; col < 32; col++)
                    {
                        int value = grid[row][col];
                        if (value != 0)
                        {
                            heatmap.Values.Add(new HeatmapValue
                            {
                                Row = row,
                                Col = col, // <-- Changed from Column = col to Col = col
                                Value = value
                            });
                        }
                    }
                }

                _context.Heatmaps.Add(heatmap);
            }

            await _context.SaveChangesAsync();
        }


        /// <summary>
        /// Converts a single CSV row to an int array (32 expected).
        /// </summary>
        private int[] ParseCsvRow(string line)
        {
            var parts = line.Split(',');

            if (parts.Length < 32)
                return null;

            var result = new int[32];

            for (int i = 0; i < 32; i++)
            {
                if (int.TryParse(parts[i], NumberStyles.Any, CultureInfo.InvariantCulture, out int value))
                {
                    result[i] = value;
                }
                else
                {
                    result[i] = 0; // default fallback
                }
            }

            return result;
        }
    }
}
