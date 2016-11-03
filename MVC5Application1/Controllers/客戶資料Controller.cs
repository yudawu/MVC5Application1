using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Application1.Models;

namespace MVC5Application1.Controllers
{
    public class 客戶資料Controller : Controller
    {
        //private 客戶資料Entities db = new 客戶資料Entities();
        客戶資料Repository repo = RepositoryHelper.Get客戶資料Repository();

        // GET: 客戶資料
        public ActionResult Index(string search, string 客戶分類, string 排序)
        {
            //var 客戶資料 = db.客戶資料.Where(客 => 客.是否已刪除 != true);
            var 客戶資料 = repo.All().Where(客 => 客.是否已刪除 != true);
            var options = (from p in 客戶資料 select p.客戶分類).Distinct().ToList();
            ViewBag.客戶分類 = new SelectList(options);

            if (!string.IsNullOrEmpty(search))
            {
                客戶資料 = 客戶資料.Where(客 => 客.客戶名稱.Contains(search) || 客.Email.Contains(search) || 客.傳真.Contains(search) || 客.地址.Contains(search) || 客.統一編號.Contains(search) || 客.電話.Contains(search));
            }
            if (!string.IsNullOrEmpty(客戶分類))
            {
                客戶資料 = 客戶資料.Where(客 => 客.客戶分類.Contains(客戶分類));
            }

            if (!string.IsNullOrEmpty(排序))
            {
                switch (排序)
                {
                    case "客戶分類升冪":
                        客戶資料 = 客戶資料.OrderBy(客 => 客.客戶分類);
                        break;
                    case "客戶名稱升冪":
                        客戶資料 = 客戶資料.OrderBy(客 => 客.客戶名稱);
                        break;
                    case "統一編號升冪":
                        客戶資料 = 客戶資料.OrderBy(客 => 客.統一編號);
                        break;
                    case "電話升冪":
                        客戶資料 = 客戶資料.OrderBy(客 => 客.電話);
                        break;
                    case "傳真升冪":
                        客戶資料 = 客戶資料.OrderBy(客 => 客.傳真);
                        break;
                    case "地址名稱升冪":
                        客戶資料 = 客戶資料.OrderBy(客 => 客.地址);
                        break;
                    case "Email升冪":
                        客戶資料 = 客戶資料.OrderBy(客 => 客.Email);
                        break;
                    case "客戶分類降冪":
                        客戶資料 = 客戶資料.OrderByDescending(客 => 客.客戶分類);
                        break;
                    case "客戶名稱降冪":
                        客戶資料 = 客戶資料.OrderByDescending(客 => 客.客戶名稱);
                        break;
                    case "統一編號降冪":
                        客戶資料 = 客戶資料.OrderByDescending(客 => 客.統一編號);
                        break;
                    case "電話降冪":
                        客戶資料 = 客戶資料.OrderByDescending(客 => 客.電話);
                        break;
                    case "傳真降冪":
                        客戶資料 = 客戶資料.OrderByDescending(客 => 客.傳真);
                        break;
                    case "地址降冪":
                        客戶資料 = 客戶資料.OrderByDescending(客 => 客.地址);
                        break;
                    case "Email降冪":
                        客戶資料 = 客戶資料.OrderByDescending(客 => 客.Email);
                        break;
                }
            }

            return View(客戶資料.ToList());
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶資料 客戶資料 = db.客戶資料.Find(id);
            客戶資料 客戶資料 = repo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                //db.客戶資料.Add(客戶資料);
                //db.SaveChanges();
                repo.Add(客戶資料);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶資料 客戶資料 = db.客戶資料.Find(id);
            客戶資料 客戶資料 = repo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                var db = repo.UnitOfWork.Context;
                db.Entry(客戶資料).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶資料 客戶資料 = db.客戶資料.Find(id);
            客戶資料 客戶資料 = repo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //客戶資料 客戶資料 = db.客戶資料.Find(id);
            客戶資料 客戶資料 = repo.Find(id);
            客戶資料.是否已刪除 = true;
            //db.客戶資料.Remove(客戶資料);
            //db.SaveChanges();
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
