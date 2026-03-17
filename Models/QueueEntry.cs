namespace ClinicQueueFrontend.Models
{
    public class QueueEntry
    {
        public int id { get; set; }

        public string name { get; set; }
        public int tokenNumber { get; set; }
        public string status { get; set; }
    }
}
