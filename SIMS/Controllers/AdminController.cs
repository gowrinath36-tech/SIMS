using Microsoft.AspNetCore.Mvc;
using SIMS.Data;
using SIMS.Attributes;

namespace SIMS.Controllers
{
    [RoleAuthorize(1)]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext db;
        public AdminController(ApplicationDbContext _db)
        {
            db = _db;
        }
        public IActionResult Dashboard(string statusFilter)
        {
            var incidents = db.GetAllIncidents();

            ViewBag.PendingCount = incidents.Count(i => i.Status == "Pending");

            ViewBag.InProgressCount = incidents.Count(i =>
                i.Status == "Assigned" || i.Status == "Investigating" || i.Status == "On Hold");

            ViewBag.ResolvedCount = incidents.Count(i =>
                i.Status == "Resolved" || i.Status == "Archived-Resolved");

            var displayList = incidents.Where(i => !i.Status.StartsWith("Archived"))
                                       .OrderBy(i => i.Status == "Resolved" || i.Status == "Denied")
                                       .ThenByDescending(i => i.CreatedAt).ToList();

            if (!string.IsNullOrEmpty(statusFilter))
            {
                if (statusFilter == "Unassigned")
                    displayList = displayList.Where(i => i.Status == "Pending" && (string.IsNullOrEmpty(i.InvestigatorName) || i.InvestigatorName == "Not Assigned")).ToList();
                else
                    displayList = displayList.Where(i => i.Status == statusFilter).ToList();
            }

            ViewBag.SelectedFilter = statusFilter;
            ViewBag.Investigators = db.GetAllInvestigators();
            return View(displayList);
        }

        [HttpGet]
        public JsonResult GetChartData()
        {
            var all = db.GetAllIncidents();
            var data = new[]
            {
        new { label = "Pending", count = all.Count(i => i.Status == "Pending") },

        new { label = "In Progress", count = all.Count(i =>
            i.Status == "Assigned" || i.Status == "Investigating" || i.Status == "On Hold") },

        new { label = "Resolved", count = all.Count(i =>
            i.Status == "Resolved" || i.Status == "Archived-Resolved") }
    };
            return Json(data);
        }

        public IActionResult Deny(int id)
        {
            db.UpdateIncidentStatus(id, "Denied");
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            bool success = db.UpdateIncidentStatus(id, "Archived");
            return Json(new { success = success });
        }

        [HttpPost]
        public IActionResult Cancel(int id)
        {
            bool success = db.CancelAssignment(id);
            return Json(new { success = success });
        }

        public IActionResult Archives()
        {
            var allData = db.GetAllIncidents();
            var archivedReports = allData.Where(i => i.Status.StartsWith("Archived")).ToList();
            return View(archivedReports);
        }
        [HttpPost]
        public IActionResult Restore(int id)
        {
            bool success = db.UpdateIncidentStatus(id, "Restore");
            return Json(new { success = success });
        }
        [HttpPost]
        public IActionResult Assign(int incidentId, int investigatorId)
        {
            db.AssignIncident(incidentId, investigatorId);
            return RedirectToAction("Dashboard");
        }
    }
}