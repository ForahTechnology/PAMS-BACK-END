using Microsoft.AspNetCore.Http;
using PAMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PAMS.DTOs.Request
{
    public class SamplingDTO
    {
        [Required]
        public string StaffName { get; set; }
        [Required]
        public Guid StaffId { get; set; }
        [Required]
        public string SamplingTime { get; set; }
        [Required]
        public string SamplingDate { get; set; }
        [Required]
        public Guid ClientId { get; set; }
        [Required]
        public decimal GPSLong { get; set; }
        public decimal GPSLat { get; set; }
        /// <summary>
        /// Convert image to base 64 string and pass to it.
        /// </summary>
        public string Picture { get; set; }
        public List<MicroBiologicalAnalysisDTO> MicroBiologicals { get; set; }
        public List<PhysicoChemicalAnalysisDTO>  PhysicoChemicals { get; set; }
    }
}
