using System;
using System.Collections.Generic;

#nullable disable

namespace track_app_admin.Data
{
    public partial class SercommInventoryEntry
    {
        public SercommInventoryEntry()
        {
            Sercomminventoryitementries = new HashSet<SercommInventoryItemEntry>();
        }

        public int Id { get; set; }
        public int? ReservationNumber { get; set; }
        public int? EmployeeId { get; set; }
        public string ReservationPdfFilePath { get; set; }
        public DateTime CreationDate { get; set; }
        public int? SercommInventoryEntryStatusId { get; set; }

        public virtual SercommInventoryEntryStatus SercommInventoryEntryStatus { get; set; }
        public virtual ICollection<SercommInventoryItemEntry> Sercomminventoryitementries { get; set; }
    }
}
