﻿namespace NZWalks_Api.Models.DTO
{
    public class AddRegionRequest
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public double Area { get; set; }

        public double Lat { get; set; }

        public double Long { get; set; }

        public long population { get; set; }

    }



}
