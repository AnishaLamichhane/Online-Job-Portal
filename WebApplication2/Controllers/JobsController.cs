using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DataJob;
using WebApplication2.Models;
using WebApplication2.Models.Domain;


namespace WebApplication2.Controllers
{
    public class JobsController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;

        public JobsController(MVCDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext=mvcDemoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var jobs = await mvcDemoDbContext.Jobs.ToListAsync();
            return View(jobs);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddJobViewModel addJobRequest)
        {
            var job = new Job()
            {
                Id = Guid.NewGuid(),
                Title = addJobRequest.Title,
                Description = addJobRequest.Description,
                RequiredSkills = addJobRequest.RequiredSkills,
                PostedDate = addJobRequest.PostedDate,
                Salary = addJobRequest.Salary
            };

            await mvcDemoDbContext.Jobs.AddAsync(job);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var job = await mvcDemoDbContext.Jobs.FirstOrDefaultAsync(x => x.Id == id);
            if (job != null)
            {
                var viewModel = new UpdateJobViewModel()
                {
                    Id = job.Id,
                    Title = job.Title,
                    Description = job.Description,
                    RequiredSkills = job.RequiredSkills,
                    PostedDate = job.PostedDate,
                    Salary = job.Salary
                };
                return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateJobViewModel model)
        {
            var job = await mvcDemoDbContext.Jobs.FindAsync(model.Id);
            if (job != null)
            {
                job.Title = model.Title;

                job.Description = model.Description;
                job.RequiredSkills = model.RequiredSkills;
                job.PostedDate = model.PostedDate;
                job.Salary = model.Salary;

                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateJobViewModel model)
        {
            var job = await mvcDemoDbContext.Jobs.FindAsync(model.Id);
            if (job != null)
            {
                mvcDemoDbContext.Jobs.Remove(job);
                await mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
