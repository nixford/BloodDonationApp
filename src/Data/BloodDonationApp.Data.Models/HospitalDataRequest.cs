namespace BloodDonationApp.Data.Models
{
    using System;

    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonationApp.Data.Common.Models;

    public class HospitalDataRequest : BaseDeletableModel<string>
    {
        public HospitalDataRequest()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string HospitalDataId { get; set; }

        public HospitalData HospitalData { get; set; }

        public string RequestId { get; set; }

        public Request Request { get; set; }
    }
}
