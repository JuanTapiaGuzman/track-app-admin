using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace track_app_admin.ViewModels
{
    public class SercommInventoryItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Make { get; set; }
        public DateTime CreationDate { get; set; }
        public SercommInventoryCategoryViewModel Categoria { get; set; }
    }
}
