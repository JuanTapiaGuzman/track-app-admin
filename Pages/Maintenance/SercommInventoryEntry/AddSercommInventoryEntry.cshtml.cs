using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using track_app_admin.ViewModels;
using track_app_admin.Data;
using System.IO;
using Microsoft.Web.Helpers;

namespace track_app_admin.Pages.Maintenance
{
    public class AddSercommInventoryEntryModel : PageModel
    {
        SercommContext _context = new SercommContext();
        public List<SercommInventoryItem> Items { get; set; }

        public void OnGet()
        {
            Items = _context.Sercomminventoryitems.ToList();
        }
    }
}
