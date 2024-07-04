using CRUDMVC.Data;
using CRUDMVC.Models;
using CRUDMVC.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDMVC.Controllers
{
    public class StudentsController : Controller
    {

        public StudentsController(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public ApplicationDbContext DbContext { get; }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    Name = viewModel.Name,
                    Email = viewModel.Email,
                    Phone = viewModel.Phone,
                    Subscribed = viewModel.Subscribed,
                };
                await DbContext.Students.AddAsync(student);
                await DbContext.SaveChangesAsync();
                return RedirectToAction("List", "Students");
            }
            return View();
		}

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await DbContext.Students.ToListAsync();
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var students = await DbContext.Students.FindAsync(Id);

            return View(students);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
          var students =   await DbContext.Students.FindAsync(viewModel.Id);
            if(students is not null )
            {
                if (ModelState.IsValid)
                {
                    students.Name = viewModel.Name;
                    students.Email = viewModel.Email;
                    students.Phone = viewModel.Phone;
                    students.Subscribed = viewModel.Subscribed;

                    await DbContext.SaveChangesAsync();
                }
               
        
            }
            return RedirectToAction("List", "Students");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Student viewModel)
        {
            var student = await DbContext.Students.AsNoTracking()
                .FirstOrDefaultAsync(x=>x.Id == viewModel.Id);
            if(student is not null)
            {
                DbContext.Students.Remove(viewModel);
                await DbContext.SaveChangesAsync();
            }
			return RedirectToAction("List", "Students");

		}

	}
}
