
using HappyBDay.Models;
using HappyBDay.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HappyBDay.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HappyBDay.Controllers
{
    public class TesteEmailController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly HappyBDayContext _db;
        public TesteEmailController(IEmailSender emailSender, IWebHostEnvironment env, HappyBDayContext db)
        {
            _emailSender = emailSender;
            _db = db;
        }
        public IActionResult EnviaEmail()
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult EnviaEmail(EmailModel email)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            TesteEnvioEmail(email.Destino, email.Assunto, email.Mensagem).GetAwaiter();
        //            return RedirectToAction("EmailEnviado");
        //        }
        //        catch (Exception)
        //        {
        //            return RedirectToAction("EmailFalhou");
        //        }
        //    }
        //    return View(email);
        //}
        public async Task<IActionResult> TesteEnvioEmail()
        {

            string email; string assunto; string mensagem;
            
            DateTime today = DateTime.Today;

            List<Consultants> birthday = await _db.Consultants.Where(c => (c.DateOfBirth.Month == today.Month) && (c.DateOfBirth.Day == today.Day)).ToListAsync();
               
            foreach (var ser in birthday)
                {
                    var consultant = await _db.Consultants.FirstOrDefaultAsync(c=> c.Email==ser.Email);
                    var name = await _db.Consultants.FirstOrDefaultAsync(c => c.Name == ser.Name);
                    email = consultant.Email;
                    assunto = "Happy Birthday " + name.Name;
                    mensagem = "Happy Birthday and the best wishes from all of us. Enjoy your day.";

                    try
                    {
                        //email destino, assunto do email, mensagem a enviar
                        await _emailSender.SendEmailAsync(email, assunto, mensagem);
                    }
                    catch (Exception)
                    {
                        return RedirectToAction("EmailFalhou");
                    }
                   
                }
               
           
            return RedirectToAction("EmailEnviado");
        }
        public ActionResult EmailEnviado()
        {
            return View();
        }
        public ActionResult EmailFalhou()
        {
            return View();
        }
    }
}