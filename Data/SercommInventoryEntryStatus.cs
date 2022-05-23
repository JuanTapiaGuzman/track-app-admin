using System;
using System.Collections.Generic;

#nullable disable

namespace track_app_admin.Data
{
    public partial class SercommInventoryEntryStatus
    {
        public SercommInventoryEntryStatus()
        {
            Sercomminventoryentries = new HashSet<SercommInventoryEntry>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual ICollection<SercommInventoryEntry> Sercomminventoryentries { get; set; }
    }
}
