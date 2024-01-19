using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bullseye
{
    public class TransactionClass
    {
        public int TxnID { get; set; }
        public int SiteIDTo { get; set; }
        public int SiteIDFrom { get; set; }
        public string Status { get; set; }
        public DateTime ShipDate { get; set; }
        public string TxnType { get; set; }
        public string BarCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public int DeliveryID { get; set; }
        public int EmergencyDelivery { get; set; }
        public string Notes { get; set; }

        public TransactionClass(int txnID, int siteIDTo, int siteIDFrom, string status,DateTime shipDate, string txnType,
            string barCode, DateTime createdDate, int deliveryID, int emergencyDelivery, string notes)
        {
            TxnID = txnID;
            SiteIDTo = siteIDTo;
            SiteIDFrom = siteIDFrom;
            Status = status;   
            ShipDate = shipDate;
            TxnType = txnType;
            BarCode = barCode;
            CreatedDate = createdDate;
            DeliveryID = deliveryID;
            EmergencyDelivery = emergencyDelivery;
            Notes = notes;
        }




    }//end of class 
}
