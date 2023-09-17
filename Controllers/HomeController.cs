using Edukator_2.Data;
using Edukator_2.Models;
using Edukator_2.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Edukator_2.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplyDbContext applyDbContext;

        public HomeController(ApplyDbContext applyDbContext)
        {
            this.applyDbContext = applyDbContext;
        }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Coures()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> AlreadyApplied()
        {
            var applied = await applyDbContext.Applicants.ToListAsync();
            return View(applied);

        }

        [HttpGet]
        public IActionResult Apply()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Apply(ApplyHere applyHere)
        {
            var applier = new Applicant()
            {
                Id = Guid.NewGuid(),
                Name = applyHere.Name,
                Email = applyHere.Email
            };

            await applyDbContext.Applicants.AddAsync(applier);
            await applyDbContext.SaveChangesAsync();
            return RedirectToAction("Apply");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {

            var app = await applyDbContext.Applicants.FirstOrDefaultAsync(x => x.Id == id);

            if (app != null)
            {
                var viewModel = new UpdateApplicant()
                {

                    Id = app.Id,
                    Name = app.Name,
                    Email = app.Email
                };
                return await Task.Run(() => View("View", viewModel));

            }


            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateApplicant updateApplicant)
        {

            var app = await applyDbContext.Applicants.FindAsync(updateApplicant.Id);
            if (app != null)
            {
                app.Name = updateApplicant.Name;
                app.Email = updateApplicant.Email;

                await applyDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }



        [HttpPost]
        public async Task<IActionResult> Delete(UpdateApplicant updateApplicant)
        {
            var app = await applyDbContext.Applicants.FindAsync(updateApplicant.Id);

            if (app != null)
            {
                applyDbContext.Applicants.Remove(app);
                await applyDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}







