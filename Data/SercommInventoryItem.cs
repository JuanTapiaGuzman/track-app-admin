using System;
using System.Collections.Generic;

#nullable disable

namespace track_app_admin.Data
{
    public partial class SercommInventoryItem
    {
        public SercommInventoryItem()
        {
            Sercomminventoryitementries = new HashSet<SercommInventoryItemEntry>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Make { get; set; }
        public DateTime CreationDate { get; set; }
        public int? SercommInventoryCategoryId { get; set; }

        public virtual SercommInventoryCategory SercommInventoryCategory { get; set; }
        public virtual ICollection<SercommInventoryItemEntry> Sercomminventoryitementries { get; set; }
    }
}
