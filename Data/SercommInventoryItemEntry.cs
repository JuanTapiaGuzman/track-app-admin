using System;
using System.Collections.Generic;

#nullable disable

namespace track_app_admin.Data
{
    public partial class SercommInventoryItemEntry
    {
        public int SercommInventoryEntryId { get; set; }
        public int SercommInventoryItemId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual SercommInventoryEntry SercommInventoryEntry { get; set; }
        public virtual SercommInventoryItem SercommInventoryItem { get; set; }
    }
}
