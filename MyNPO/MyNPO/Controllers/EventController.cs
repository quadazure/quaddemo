﻿using MyNPO.DataAccess;
using MyNPO.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyNPO.Controllers
{
    public class EventController : BaseController
    {
        // GET: Event
        public ActionResult Index()
        {
            var entityContext = new EntityContext();
            var lEvents = entityContext.eventInfos.ToList();
            return View(lEvents);
        }

        public ActionResult Create()
        {
            return View();
        }

        public JsonResult GmailShare(int id)
        {
            var entityContext = new EntityContext();
            var eventInfo = entityContext.eventInfos.Where(q => q.Id == id).FirstOrDefault();
            var email = entityContext.familyInfos.Select(q => q.Email).ToList();

            foreach(string mail in email)
                MailSender.EventSendMail(new EventInfo() { Email = mail, Event = eventInfo });

            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase postedFile, FormCollection eventCreation)
        {
            string path = Server.MapPath("~/Images/Events/");
            string fileName = string.Empty;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (!string.IsNullOrEmpty(postedFile?.FileName))
            {
                fileName = Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(path + fileName);
            }

            var createEvent = new Event()
            {
                Name = eventCreation["EventName"],
                Location = eventCreation["EventLocation"],
                Details = eventCreation["EventDescription"],
                StartDate = Convert.ToDateTime(eventCreation["EventStartDate"]),
                EndDate = Convert.ToDateTime(eventCreation["EventEndDate"]),
                UploadFileName = fileName
            };
            var entityContext = new EntityContext();
            entityContext.eventInfos.Add(createEvent);
            entityContext.SaveChanges();

            ViewBag.Status = "Successfully Created";
            return View();
        }
        

    }
}