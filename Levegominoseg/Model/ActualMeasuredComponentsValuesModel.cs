using System;
using System.Collections.Generic;
using System.Text;

namespace Levegominoseg.Model
{
    public class ActualMeasuredComponentsValuesModel
    {
        public StationInformationModel StationInformation { get; set; }

        public ColumnModel[] Columns { get; set; }

        public DataModel Data { get; set; }

        public DateTime DateOfLastMeasurement { get; set; }

        public double? GetMeasurement(string title)
        {
            for (int i = 0; i < Columns.Length; i++)
            {
                var column = Columns[i];
                if (column.Title == title)
                {
                    switch (column.Field)
                    {
                        case "Value1":
                            return Data.Value1?.Value;
                        case "Value2":
                            return Data.Value2?.Value;
                        case "Value3":
                            return Data.Value3?.Value;
                        case "Value4":
                            return Data.Value4?.Value;
                        case "Value5":
                            return Data.Value5?.Value;
                        case "Value7":
                            return Data.Value7?.Value;
                        case "Value12":
                            return Data.Value12?.Value;
                        case "Value17":
                            return Data.Value17?.Value;
                    }
                }
            }

            return null;
        }
    }
}
