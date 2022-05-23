using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using track_app_admin.Data;

namespace track_app_admin.Pages.Maintenance
{
    public class SercommInventoryEntryModel : PageModel
    {
        SercommContext _context = new SercommContext();
        public List<track_app_admin.Data.SercommInventoryEntry> Entries { get; set; }
        public List<SercommInventoryEntryStatus> StatusList { get; set; }

        public void OnGet()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
            Entries = _context.Sercomminventoryentries.ToList();
            Entries.ForEach(entry => entry.SercommInventoryEntryStatus = _context.Sercomminventoryentrystatus.Where(status => status.Id == entry.SercommInventoryEntryStatusId).FirstOrDefault());
            StatusList = _context.Sercomminventoryentrystatus.ToList();
        }
    }
}
