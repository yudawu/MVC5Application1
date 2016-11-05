using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Application1.Models;
using MVC5Application1.Models.ViewModels;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.HSSF.Util;
using NPOI.POIFS.FileSystem;
using NPOI.HPSF;
using System.IO;

namespace MVC5Application1.Controllers
{
    [Authorize]
    public class 客戶資料Controller : Controller
    {
        //private 客戶資料Entities db = new 客戶資料Entities();
        客戶資料Repository repo = RepositoryHelper.Get客戶資料Repository();
        客戶聯絡人Repository 客戶聯絡人repo = RepositoryHelper.Get客戶聯絡人Repository();

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

        public ActionResult NPOI()
        {
            var 客戶資料 = repo.All().Where(客 => 客.是否已刪除 != true).ToArray();

            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("Sheet1");
            var rowIndex = 0;
            var row = sheet.CreateRow(rowIndex);
            row.CreateCell(0).SetCellValue("客戶名稱");
            row.CreateCell(1).SetCellValue("客戶分類");
            row.CreateCell(2).SetCellValue("統一編號");
            row.CreateCell(3).SetCellValue("地址");
            row.CreateCell(4).SetCellValue("Email");
            row.CreateCell(5).SetCellValue("電話");
            row.CreateCell(6).SetCellValue("傳真");
            rowIndex++;

            foreach (客戶資料 item in 客戶資料)
            {
                row = sheet.CreateRow(rowIndex);
                row.CreateCell(0).SetCellValue(item.客戶名稱);
                row.CreateCell(1).SetCellValue(item.客戶分類);
                row.CreateCell(2).SetCellValue(item.統一編號);
                row.CreateCell(3).SetCellValue(item.地址);
                row.CreateCell(4).SetCellValue(item.Email);
                row.CreateCell(5).SetCellValue(item.電話);
                row.CreateCell(6).SetCellValue(item.傳真);
                rowIndex++;
            }

            MemoryStream files = new MemoryStream();
            workbook.Write(files);
            files.Close();

            return File(files.ToArray(), "application/vnd.ms-excel", "客戶資料.xls");
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
        public ActionResult 客戶聯絡人List(int id)
        {
            var 客戶聯絡人 = 客戶聯絡人repo.All().Where(客 => 客.客戶Id == id).ToList();
            return View(客戶聯絡人);
        }

        public ActionResult BatchUpdate(客戶聯絡人List[] items)
        {
            /*
             * 預設輸出的欄位名稱格式：item.ProductId
             * 要改成以下欄位格式：
             * items[0].ProductId
             * items[1].ProductId
             */
            if (ModelState.IsValid)
            {
                foreach (var item in items)
                {
                    var 客戶聯絡人 = 客戶聯絡人repo.Find(item.Id);
                    客戶聯絡人.職稱 = item.職稱;
                    客戶聯絡人.手機 = item.手機;
                    客戶聯絡人.電話 = item.電話;
                }

                客戶聯絡人repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }

            return View();
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

        public ActionResult Edit2()
        {            
            if (User.Identity == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var 客戶資料 = repo.All().Where(客 => 客.帳號 == User.Identity.Name).ToList();
            if (客戶資料.Count == 1)
            {
                return View(客戶資料.FirstOrDefault());
            }
            return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit2([Bind(Include = "Id,電話,傳真,地址,Email,密碼")] 客戶資料 客戶資料)
        {
            //if (ModelState.IsValid)
            {
                客戶資料 客戶資料2 = repo.Find(客戶資料.Id);
                客戶資料2.電話 = 客戶資料.電話;
                客戶資料2.傳真 = 客戶資料.傳真;
                客戶資料2.地址 = 客戶資料.地址;
                客戶資料2.Email = 客戶資料.Email;
                客戶資料2.密碼 = Hash.Encode(客戶資料.密碼);
                repo.UnitOfWork.Commit();
                //return RedirectToAction("Index");
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
