using System;

namespace MSISDNWebClient.Models
{
    public class MSISDNRecord
    {
        public string MSISDN { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public MSISDNRecord(string msisdn, string status)
        {
            MSISDN = msisdn;
            Status = status;
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }

        public void UpdateStatus(string newStatus)
        {
            Status = newStatus;
            UpdatedDate = DateTime.Now;
        }
    }
}