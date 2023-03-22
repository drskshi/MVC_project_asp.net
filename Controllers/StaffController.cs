using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffRegistration.Data;
using StaffRegistration.Models;

namespace StaffRegistration.Controllers
{
    public class StaffController : Controller
    {
        private readonly StaffRegistrationDbContext StaffRegistrationDbContext;

        public StaffController(StaffRegistrationDbContext StaffRegistrationDbContext)
        {
            this.StaffRegistrationDbContext = StaffRegistrationDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Staff = await StaffRegistrationDbContext.Staff.ToListAsync();
            return View(Staff);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddStaffViewModel addStaffRequest)
        {
            var staff = new Models.Domain.Staff()
            {
                Id = Guid.NewGuid(),
                Name = addStaffRequest.Name,
                Email = addStaffRequest.Email,
                Address = addStaffRequest.Address,
                ContactNumber = addStaffRequest.ContactNumber,
                DateOfBirth = addStaffRequest.DateOfBirth,

            };
            await StaffRegistrationDbContext.Staff.AddAsync(staff);
            await StaffRegistrationDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]

        public async Task<IActionResult> View(Guid id)
        {
            var staff = await StaffRegistrationDbContext.Staff.FirstOrDefaultAsync(x => x.Id == id);
            if (staff != null)
            {
                var viewModel = new UpdateStaffViewModel
                {
                    Id = staff.Id,
                    Name = staff.Name,
                    Email = staff.Email,
                    Address = staff.Address,
                    ContactNumber = staff.ContactNumber,
                    DateOfBirth = staff.DateOfBirth,
                };
                return await Task.Run(() =>View("View",viewModel));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateStaffViewModel model)
        {
            var staff = await StaffRegistrationDbContext.Staff.FindAsync(model.Id);
            if (staff != null)
            {
                staff.Name = model.Name;
                staff.Email = model.Email;
                staff.Address = model.Address;  
                staff.ContactNumber = model.ContactNumber;  
                staff.DateOfBirth = model.DateOfBirth;

                await StaffRegistrationDbContext.SaveChangesAsync();

                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete (UpdateStaffViewModel model)
        {
            var staff = await StaffRegistrationDbContext.Staff.FindAsync(model.Id);
            if (staff != null)
            {
                StaffRegistrationDbContext.Staff.Remove(staff);
                await StaffRegistrationDbContext.SaveChangesAsync();

                return RedirectToAction("Index");

            };
            return RedirectToAction("Index");
        }
    }
}
