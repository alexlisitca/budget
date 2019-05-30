using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Budget.Domain.Core.Entity;
using Budget.Infrastructure.Data;

namespace Budget.Web.Mvc.Areas.Entities.Controllers
{
    public class ScoresController : Controller
    {
        private BudgetDbContext db = new BudgetDbContext();

        // GET: Entities/Scores
        public async Task<ActionResult> Index()
        {
            return View(await db.Scores.ToListAsync());
        }

        // GET: Entities/Scores/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Score score = await db.Scores.FindAsync(id);
            if (score == null)
            {
                return HttpNotFound();
            }
            return View(score);
        }

        // GET: Entities/Scores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Entities/Scores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,OrderId,Name,Description,ScoreType,Balance,InitBalance,CreditDebts,CreditLimit,CreditMinPayment,CreditPaymentDate")] Score score)
        {
            if (ModelState.IsValid)
            {
                score.Id = Guid.NewGuid();
                db.Scores.Add(score);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(score);
        }

        // GET: Entities/Scores/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Score score = await db.Scores.FindAsync(id);
            if (score == null)
            {
                return HttpNotFound();
            }
            return View(score);
        }

        // POST: Entities/Scores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,OrderId,Name,Description,ScoreType,Balance,InitBalance,CreditDebts,CreditLimit,CreditMinPayment,CreditPaymentDate")] Score score)
        {
            if (ModelState.IsValid)
            {
                db.Entry(score).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(score);
        }

        // GET: Entities/Scores/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Score score = await db.Scores.FindAsync(id);
            if (score == null)
            {
                return HttpNotFound();
            }
            return View(score);
        }

        // POST: Entities/Scores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Score score = await db.Scores.FindAsync(id);
            db.Scores.Remove(score);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
