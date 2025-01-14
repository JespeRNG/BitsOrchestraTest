using System;
using CsvHelper;
using System.IO;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Globalization;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using BitsOrchestraTest.Models;
using BitsOrchestraTest.Contracts.DTO;
using BitsOrchestraTest.Core.Interfaces;
using BitsOrchestraTest.Core.Helpers;

namespace BitsOrchestraTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContactService _contactService;

        public HomeController(IContactService contactService)
        {
            _contactService = contactService ?? throw new ArgumentNullException(nameof(contactService));
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var contactDtos = await _contactService.GetContactsAsync();
            var contactModels = contactDtos.Select(dto => new ContactModel
            {
                EncryptedId = dto.Id,
                Name = dto.Name,
                DateOfBirth = dto.DateOfBirth,
                Married = dto.Married,
                Phone = dto.Phone,
                Salary = dto.Salary
            }).ToList();

            return View(contactModels);
        }

        [HttpPost]
        public async Task<ActionResult> UploadCSV(HttpPostedFileBase file)
        {
            if (file?.ContentLength == 0 || file == null) throw new ArgumentNullException(nameof(file));
            if (file.ContentType != "text/csv") throw new ArgumentException("Invalid file type. Please upload a CSV file.");

            var records = CsvProcessingHelper.ProcessCsvFile(file.InputStream);
            await _contactService.AddContactsAsync(records);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string encryptedId)
        {
            if (encryptedId == null)
            {
                throw new ArgumentNullException(nameof(encryptedId));
            }

            await _contactService.DeleteContactAsync(encryptedId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ContactModel contactModel)
        { 
            if (!ModelState.IsValid) 
            {
                return new HttpStatusCodeResult(400, "Invalid data");
            } 
            
            var contactUpdateDto = new ContactUpdateDto()
            { 
                Married = contactModel.Married,
                Phone = contactModel.Phone,
                Salary = contactModel.Salary
            };

            await _contactService.UpdateContactAsync(contactUpdateDto, contactModel.EncryptedId);
            return RedirectToAction("Index"); }
    }
}