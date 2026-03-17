using Microsoft.Extensions.Hosting;

namespace ClinicQueueFrontend.Models
{
    public class Appointment
    {
        public int id { get; set; }

        public string date { get; set; }

        public string slot { get; set; }

        public string status { get; set; }
      
        //public int PatientId { get; set; }
    }
}
