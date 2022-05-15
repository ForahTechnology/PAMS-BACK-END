using PAMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PAMS.DTOs.Request
{
    public class CreateClientDTO
    {
        public string Name { get; set; }
        [RegularExpression(@"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        [RegularExpression("[+]?[0-9]*", ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }
        public ICollection<ClientSampleTemplate> SampleTemplates { get; set; }
    }
    public class ClientSampleTemplate
    {
        public Guid TemplateId { get; set; }
        public PhysicoChemicalAnalysisType Type { get; set; }
    }
}
