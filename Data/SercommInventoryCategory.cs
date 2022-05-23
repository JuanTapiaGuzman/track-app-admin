using System;
using System.Collections.Generic;

#nullable disable

namespace track_app_admin.Data
{
    public partial class SercommInventoryCategory
    {
        public SercommInventoryCategory()
        {
            Sercomminventoryitems = new HashSet<SercommInventoryItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual ICollection<SercommInventoryItem> Sercomminventoryitems { get; set; }
    }
}
