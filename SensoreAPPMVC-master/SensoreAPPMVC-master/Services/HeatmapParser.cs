using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Services
{
    public class HeatmapParser
    {
        private const int HeatmapSize = 32;

        public List<int[,]> ParseCsvToHeatmaps(string filePath)
        {
            var lines = File.ReadAllLines(filePath);

            // Convert to list of int arrays
            var rows = lines
                .Select(line => line.Split(',').Select(int.Parse).ToArray())
                .ToList();

            var heatmaps = new List<int[,]>();

            int totalRows = rows.Count;
            int totalHeatmaps = totalRows / HeatmapSize;

            for (int h = 0; h < totalHeatmaps; h++)
            {
                var matrix = new int[HeatmapSize, HeatmapSize];

                for (int r = 0; r < HeatmapSize; r++)
                {
                    for (int c = 0; c < HeatmapSize; c++)
                    {
                        matrix[r, c] = rows[(h * HeatmapSize) + r][c];
                    }
                }

                heatmaps.Add(matrix);
            }

            return heatmaps;
        }

        // Extract only non-zero values from a matrix
        public List<(int Row, int Col, int Value)> ExtractNonZeroValues(int[,] matrix)
        {
            var result = new List<(int Row, int Col, int Value)>();

            for (int r = 0; r < HeatmapSize; r++)
            {
                for (int c = 0; c < HeatmapSize; c++)
                {
                    int value = matrix[r, c];
                    if (value != 0)
                    {
                        result.Add((r, c, value));
                    }
                }
            }

            return result;
        }
    }
}
