﻿using StudentMVC.StudentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace StudentMVC.Controllers
{
    public class StudentController : Controller
    {
        static Uri serviceUri = new Uri("http://w09.nguyenkhoat.com/odata/");
        static Default.Container container = new Default.Container(serviceUri);

        // GET: Student
        public ActionResult Index()
        {
            var students = container.Students.ToList();
            return View(students);
        }

        // GET: Student/Details/5
        public ActionResult Details(int id)
        {
            var student = container.Students.Where(s => s.StudentId == id).FirstOrDefault();
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(Student student)
        {
            try
            {
                // TODO: Add insert logic here
                Student s = new Student
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Major = student.Major
                };

                container.AddToStudents(s);
                container.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = container.Students.Where(s => s.StudentId == id).FirstOrDefault();
            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Student student)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    Student fstudent = container.Students.Where(s => s.StudentId == id).FirstOrDefault();
                    fstudent.FirstName = student.FirstName;
                    fstudent.LastName = student.LastName;
                    fstudent.Major = student.Major;

                    container.UpdateObject(fstudent);
                    container.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = container.Students.Where(s => s.StudentId == id).FirstOrDefault();
            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Student student = container.Students.Where(s => s.StudentId == id).FirstOrDefault();
                container.DeleteObject(student);
                container.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
