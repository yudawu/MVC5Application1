﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Application1.Models;
using System.Data.Entity.Validation;
using MVC5Course.Controllers;
using PagedList;

namespace MVC5Application1.Controllers
{
    [LocalDebugOnly]
    [ExecutingTime]
    [HandleError(ExceptionType = typeof(DbEntityValidationException), View = "Error_DbEntityValidationException")]
    public class 客戶聯絡人Controller : Controller
    {
        //private 客戶資料Entities db = new 客戶資料Entities();
        客戶聯絡人Repository repo = RepositoryHelper.Get客戶聯絡人Repository();
        客戶資料Repository 客戶資料repo = RepositoryHelper.Get客戶資料Repository();
        private int pageSize = 3;

        // GET: 客戶聯絡人
        public ActionResult Index(string search, string 職稱, string 排序, int? page)
        {
            //var 客戶聯絡人 = db.客戶聯絡人.Include(客 => 客.客戶資料).Where(客 => 客.是否已刪除 != true);
            var 客戶聯絡人 = repo.All().Include(客 => 客.客戶資料).Where(客 => 客.是否已刪除 != true);
            var options = (from p in 客戶聯絡人 select p.職稱).Distinct().ToList();
            ViewBag.職稱 = new SelectList(options);

            if (!string.IsNullOrEmpty(search))
            {
                客戶聯絡人 = 客戶聯絡人.Where(客 => 客.姓名.Contains(search) || 客.手機.Contains(search) || 客.職稱.Contains(search) || 客.電話.Contains(search) || 客.Email.Contains(search));
            }
            if (!string.IsNullOrEmpty(職稱))
            {
                客戶聯絡人 = 客戶聯絡人.Where(客 => 客.職稱.Contains(職稱));
            }

            if (!string.IsNullOrEmpty(排序))
            {
                switch (排序)
                {
                    case "職稱升冪":
                        客戶聯絡人 = 客戶聯絡人.OrderBy(客 => 客.職稱);
                        break;
                    case "姓名升冪":
                        客戶聯絡人 = 客戶聯絡人.OrderBy(客 => 客.姓名);
                        break;
                    case "Email升冪":
                        客戶聯絡人 = 客戶聯絡人.OrderBy(客 => 客.Email);
                        break;
                    case "手機升冪":
                        客戶聯絡人 = 客戶聯絡人.OrderBy(客 => 客.手機);
                        break;
                    case "電話升冪":
                        客戶聯絡人 = 客戶聯絡人.OrderBy(客 => 客.電話);
                        break;
                    case "客戶名稱升冪":
                        客戶聯絡人 = 客戶聯絡人.OrderBy(客 => 客.客戶資料.客戶名稱);
                        break;
                    case "職稱降冪":
                        客戶聯絡人 = 客戶聯絡人.OrderByDescending(客 => 客.職稱);
                        break;
                    case "姓名降冪":
                        客戶聯絡人 = 客戶聯絡人.OrderByDescending(客 => 客.姓名);
                        break;
                    case "Email降冪":
                        客戶聯絡人 = 客戶聯絡人.OrderByDescending(客 => 客.Email);
                        break;
                    case "手機降冪":
                        客戶聯絡人 = 客戶聯絡人.OrderByDescending(客 => 客.手機);
                        break;
                    case "電話降冪":
                        客戶聯絡人 = 客戶聯絡人.OrderByDescending(客 => 客.電話);
                        break;
                    case "客戶名稱降冪":
                        客戶聯絡人 = 客戶聯絡人.OrderByDescending(客 => 客.客戶資料.客戶名稱);
                        break;
                }
            }
            else
            {
                客戶聯絡人 = 客戶聯絡人.OrderBy(客 => 客.Id);
            }

            var pageNumeber = page ?? 1;
            return View(客戶聯絡人.ToPagedList(pageNumeber, pageSize));
        }

        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            客戶聯絡人 客戶聯絡人 = repo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(客戶資料repo.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                var customer = repo.All().Where(客 => 客.客戶Id == 客戶聯絡人.客戶Id && 客.Email == 客戶聯絡人.Email).ToList();
                if (customer.Count == 0)
                {
                    //db.客戶聯絡人.Add(客戶聯絡人);
                    //db.SaveChanges();
                    repo.Add(客戶聯絡人);
                    repo.UnitOfWork.Commit();
                }

                //db.客戶聯絡人.Add(客戶聯絡人);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(客戶資料repo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            客戶聯絡人 客戶聯絡人 = repo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(客戶資料repo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            //if (ModelState.IsValid)
            {
                //var customer = repo.All().Where(客 => 客.Id != 客戶聯絡人.Id && 客.客戶Id == 客戶聯絡人.客戶Id && 客.Email == 客戶聯絡人.Email).ToList();
                //if (customer.Count == 0)
                {
                    var db = repo.UnitOfWork.Context;
                    db.Entry(客戶聯絡人).State = EntityState.Modified;
                    db.SaveChanges();
                }

                //db.Entry(客戶聯絡人).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(客戶資料repo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            //return View(客戶聯絡人);
            return RedirectToAction("Index");
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            客戶聯絡人 客戶聯絡人 = repo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            客戶聯絡人 客戶聯絡人 = repo.Find(id);
            客戶聯絡人.是否已刪除 = true;
            //db.客戶聯絡人.Remove(客戶聯絡人);
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
