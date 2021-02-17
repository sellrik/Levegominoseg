using System;
using System.Collections.Generic;
using System.Text;

namespace Levegominoseg.Model
{
    public class StationInformationModel
	{
        public int SerialCode { get; set; }

        public string StationName { get; set; }

        public string City { get; set; }

        public string Location { get; set; }

        public string CompetentsAreaName { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string PictureUrl { get; set; }

        public string PictureAlternateText { get; set; }
    }
}
