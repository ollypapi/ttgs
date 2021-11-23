using System.Collections.Generic;

namespace TTGS.Shared.Request
{
    public class CreateContractorRequest
    {
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        public List<string> TransportingCountries { get; set; }
        public string AverageTripWeight { get; set; }
        public string AverageTripLength { get; set; }
        public string NumberOfFleetVehicles { get; set; }
        public List<CreateContractorDocumentRequest> RegistrationDetails { get; set; }
        public List<CreateContractorDocumentRequest> Insurance { get; set; }
        public string AdditionalInformation { get; set; }
    }

    public class CreateContractorDocumentRequest
    {
        public string FileName { get; set; }
        public string Image { get; set; }
    }
}
