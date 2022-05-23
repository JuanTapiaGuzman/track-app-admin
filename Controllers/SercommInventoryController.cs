using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using track_app_admin.Data;
using track_app_admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace track_app_admin.Controllers
{
    public class SercommInventoryController : Controller
    {
        SercommContext _context = new SercommContext();

        private readonly IConfiguration Configuration;

        public SercommInventoryController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SaveSercommInventoryEntry([FromForm] SercommInventoryEntryViewModel viewModel)
        { 
            List<SercommInventoryItemEntryViewModel> items = JsonSerializer.Deserialize<List<SercommInventoryItemEntryViewModel>>(viewModel.Items);

            SercommInventoryEntry sercommInventoryEntry = new SercommInventoryEntry();
            sercommInventoryEntry.ReservationNumber = viewModel.ReservationNumber;
            sercommInventoryEntry.EmployeeId = viewModel.EmployeeId;
            sercommInventoryEntry.CreationDate = DateTime.Now;
            sercommInventoryEntry.ReservationPdfFilePath = Configuration["InventoryEntryReservationFilesPath"] + "//" + viewModel.ReservationNumber.ToString() + ".pdf";
            sercommInventoryEntry.SercommInventoryEntryStatusId = 1;
            items.ForEach(item => sercommInventoryEntry.Sercomminventoryitementries.Add(new SercommInventoryItemEntry { CreationDate = DateTime.Now, Quantity = item.Quantity, SercommInventoryItemId = item.Id }));


            var entry = _context.Add(new track_app_admin.Data.SercommInventoryEntry());
            entry.CurrentValues.SetValues(sercommInventoryEntry);
            _context.SaveChanges();
            foreach (var item in items)
            {
                var itemEntry = _context.Add(new track_app_admin.Data.SercommInventoryItemEntry());
                SercommInventoryItemEntry sercommInventoryItemEntry =
                        new SercommInventoryItemEntry {
                            SercommInventoryEntryId = _context.Sercomminventoryentries
                                                                .Where(entry => entry.ReservationNumber == viewModel.ReservationNumber)
                                                                .FirstOrDefault()
                                                                .Id,
                            CreationDate = DateTime.Now, 
                            Quantity = item.Quantity, 
                            SercommInventoryItemId = item.Id 
                        };
                itemEntry.CurrentValues.SetValues(sercommInventoryItemEntry);
            }
            await _context.SaveChangesAsync();
            return Json(viewModel);
        }


        [HttpDelete]
        public async void DeleteSercommInventoryEntry(string Id)
        {
            if (Id == null)
            {
                return;
            }
            SercommInventoryEntry sercommInventoryEntry = _context.Sercomminventoryentries.Where(x => x.Id == int.Parse(Id)).FirstOrDefault();
            List<SercommInventoryItemEntry> sercommInventoryItemEntries = _context.Sercomminventoryitementries.Where(x => x.SercommInventoryEntryId == int.Parse(Id)).ToList();
            _context.Sercomminventoryentries.Remove(sercommInventoryEntry);
            sercommInventoryItemEntries.ForEach(entry => _context.Sercomminventoryitementries.Remove(entry));
            await _context.SaveChangesAsync();
        }
    }
}
