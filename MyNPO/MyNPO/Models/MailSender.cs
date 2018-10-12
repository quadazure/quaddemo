﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace MyNPO.Models
{
    public class MailSender
    {
        public void Send()
        {
            SmtpClient sc = new SmtpClient("smtp.1and1.com", 587);
            sc.Credentials = new System.Net.NetworkCredential("admin@saibabaseattle.com", "SaiBaba@123");

            sc.DeliveryMethod = SmtpDeliveryMethod.Network;
            sc.UseDefaultCredentials = false;
            sc.EnableSsl = true;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("admin@saibabaseattle.com", "Temple Email");
            msg.To.Add(new MailAddress("senbagakumars@gmail.com", "Senbaga"));
            msg.Subject = "Sai Baba Temple Event";
            msg.Body = "Sai Baba Temple Event Invitation";

            StringBuilder str = new StringBuilder();
            str.AppendLine("BEGIN:VCALENDAR");
            str.AppendLine("PRODID:-//Seattle SaiBaba");
            str.AppendLine("VERSION:2.0");
            str.AppendLine("METHOD:REQUEST");
            str.AppendLine("BEGIN:VEVENT");
            str.AppendLine(string.Format("DTSTART:{0:yyyyMMddTHHmmssZ}", "20181005100000"));
            str.AppendLine(string.Format("DTSTAMP:{0:yyyyMMddTHHmmssZ}", DateTime.UtcNow));
            str.AppendLine(string.Format("DTEND:{0:yyyyMMddTHHmmssZ}", "20181005110000"));
            str.AppendLine("LOCATION: Redmond");
            str.AppendLine(string.Format("UID:{0}", Guid.NewGuid()));
            str.AppendLine(string.Format("DESCRIPTION:{0}", msg.Body));
            str.AppendLine(string.Format("X-ALT-DESC;FMTTYPE=text/html:{0}", msg.Body));
            str.AppendLine(string.Format("SUMMARY:{0}", msg.Subject));
            str.AppendLine(string.Format("ORGANIZER:MAILTO:{0}", msg.From.Address));

            str.AppendLine(string.Format("ATTENDEE;CN=\"{0}\";RSVP=TRUE:mailto:{1}", msg.To[0].DisplayName, msg.To[0].Address));

            str.AppendLine("BEGIN:VALARM");
            str.AppendLine("TRIGGER:-PT15M");
            str.AppendLine("ACTION:DISPLAY");
            str.AppendLine("DESCRIPTION:Reminder");
            str.AppendLine("END:VALARM");
            str.AppendLine("END:VEVENT");
            str.AppendLine("END:VCALENDAR");
            System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType("text/calendar");
            ct.Parameters.Add("method", "REQUEST");
            AlternateView avCal = AlternateView.CreateAlternateViewFromString(str.ToString(), ct);
            msg.AlternateViews.Add(avCal);

            sc.Send(msg);
        }

        public void SendMail()
        {
            string senderID = "shenbakumar24@gmail.com";
            string senderPassword = "sen_senba24";
            string hostName = "smtp.gmail.com";

            senderID = "admin@saibabaseattle.com";
            senderPassword = "SaiBaba@123";
            hostName = "smtp.1and1.com";
            string body = "test";
           
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add("senbagakumars@gmail.com");
                mail.From = new MailAddress(senderID);
                mail.Subject = "My Test Email!";
                mail.Body = body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = hostName; //Or Your SMTP Server Address
                smtp.Credentials = new System.Net.NetworkCredential(senderID, senderPassword);
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
               
            }

        }

        public void SendMailWithInvite()
        {
            string senderID = "shenbakumar24@gmail.com";
            string senderPassword = "sen_senba24";
            string hostName = "smtp.gmail.com";

            senderID = "admin@saibabaseattle.com";
            senderPassword = "SaiBaba@123";
            hostName = "smtp.1and1.com";
            string body = "Event";


            MailMessage msg = new MailMessage();
            msg.Body = body;
            msg.Subject = "EventMail";
            msg.From = new MailAddress(senderID, "SaiBaba");
            msg.To.Add(new MailAddress("senbagakumars@gmail.com", "Senbaga"));
            msg.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient(hostName, 587);

            smtp.Credentials = new NetworkCredential(senderID,senderPassword);

            msg = GetCalanderInviteMsg3(msg);

            smtp.EnableSsl = true;

            smtp.Send(msg);


        }

        public MailMessage GetCalanderInviteMsg3(MailMessage msg)
        {

            string TimeFormat = "yyyyMMdd\\THHmmss\\Z";

            string start = DateTime.Now.AddDays(1).AddHours(1).ToString(TimeFormat);
            string end = DateTime.Now.AddDays(1).AddHours(2).ToString(TimeFormat);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("BEGIN:VCALENDAR");
            sb.AppendLine("PRODID:-//Google Inc//Google Calendar 70.9054//EN");
            sb.AppendLine("VERSION:2.0");
            sb.AppendLine("CALSCALE:GREGORIAN");
            sb.AppendLine("METHOD:PUBLISH");
            sb.AppendLine("X-WR-CALNAME:Come to the SaiBaba Redmond temple");
            sb.AppendLine("X-WR-TIMEZONE:America/Redmond");
            sb.AppendLine("X-WR-CALDESC:");
            sb.AppendLine("TZID:America/Redmond");
            sb.AppendLine("BEGIN:VEVENT");

            //sb.AppendLine("DTSTART;VALUE=DATE:"+ start);
            //sb.AppendLine("DTEND;VALUE=DATE:"+ end);

            sb.AppendLine("DTSTART;TZID=America/Redmond:" + start);
            sb.AppendLine("DTEND;TZID=America/Redmond:" + end);

            sb.AppendLine("DTSTAMP:"+ DateTime.UtcNow.AddDays(-1).ToString(TimeFormat));
            sb.AppendLine("UID:" + Guid.NewGuid());
            sb.AppendLine("CREATED:" + DateTime.UtcNow.AddDays(-1).ToString(TimeFormat));
            sb.AppendLine("DESCRIPTION:Pradishta Event");
            sb.AppendLine(string.Format("ORGANIZER:MAILTO:{0}", msg.From.Address));
            sb.AppendLine(string.Format("ATTENDEE;CN=\"{0}\";RSVP=TRUE:mailto:{1}", msg.To[0].DisplayName, msg.To[0].Address));
            sb.AppendLine("LAST-MODIFIED:" + DateTime.UtcNow.AddDays(-1).ToString(TimeFormat));
            sb.AppendLine("LOCATION:RedMond");
            sb.AppendLine("SEQUENCE:0");
            sb.AppendLine("STATUS:CONFIRMED");
            sb.AppendLine("SUMMARY:SaiBaba Pradishta Event");
            sb.AppendLine("TRANSP:TRANSPARENT");

            sb.AppendLine("BEGIN:VALARM");
            sb.AppendLine("TRIGGER:-PT15M");
            sb.AppendLine("ACTION:DISPLAY");
            sb.AppendLine("DESCRIPTION:Reminder");
            sb.AppendLine("END:VALARM");
            sb.AppendLine("END:VEVENT");

            //sb.AppendLine("BEGIN:VEVENT");
            //sb.AppendLine("DTSTART;VALUE=DATE:20130703");
            //sb.AppendLine("DTEND;VALUE=DATE:20130704");
            //sb.AppendLine("DTSTAMP:20130127T040730Z");
            //sb.AppendLine("UID:" + "84395bf9-875e-4925-864f-910309e0a355");
            //sb.AppendLine("CREATED:20080624T082556Z");
            //sb.AppendLine("DESCRIPTION:");
            //sb.AppendLine("LAST-MODIFIED:" + "2013127T054320Z");
            //sb.AppendLine("LOCATION:");
            //sb.AppendLine("SEQUENCE:0");
            //sb.AppendLine("STATUS:CONFIRMED");
            //sb.AppendLine("SUMMARY:event2");
            //sb.AppendLine("TRANSP:TRANSPARENT");
            //sb.AppendLine("END:VEVENT");
            sb.AppendLine("END:VCALENDAR");
            System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType("text/calendar");
            ct.Parameters.Add("method", "REQUEST");
            AlternateView avCal = AlternateView.CreateAlternateViewFromString(sb.ToString(), ct);
            msg.AlternateViews.Add(avCal);

            return msg;
        }
    }
}