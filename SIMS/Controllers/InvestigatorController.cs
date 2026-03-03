using Microsoft.AspNetCore.Mvc;
using SIMS.Data;
using SIMS.Attributes;

namespace SIMS.Controllers
{
    [RoleAuthorize(3)]
    public class InvestigatorController : Controller
    {
        private readonly ApplicationDbContext db;

        public InvestigatorController(ApplicationDbContext _db)
        {
            db = _db;
        }

        public IActionResult MyTasks()
        {
            string uid = HttpContext.Session.GetString("UID");
            if (string.IsNullOrEmpty(uid)) return RedirectToAction("Index", "Home");

            var allTasks = db.GetAssignedIncidents(int.Parse(uid));

            var activeTasks = allTasks.Where(t => t.Status == "Assigned" ||
                                                  t.Status == "Investigating" ||
                                                  t.Status == "On Hold").ToList();

            var resolvedTasks = allTasks.Where(t => t.Status == "Resolved" ||
                                                    t.Status == "Archived-Resolved").ToList();

            ViewBag.ActiveCount = activeTasks.Count;
            ViewBag.ResolvedCount = resolvedTasks.Count;
            ViewBag.TotalCount = activeTasks.Count + resolvedTasks.Count;

            ViewBag.ResolvedTasks = resolvedTasks;
            return View(activeTasks);
        }

        [HttpPost]
        public IActionResult UpdateStatus(int incId, string status)
        {
            bool success = db.UpdateIncidentStatus(incId, status);
            if (success)
            {
                TempData["Message"] = "Status successfully changed!";
            }
            return RedirectToAction("MyTasks");
        }
    }
}