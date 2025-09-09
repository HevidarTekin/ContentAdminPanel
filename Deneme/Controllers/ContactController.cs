using Deneme.Context;
using Deneme.Models;
using Microsoft.AspNetCore.Mvc;

namespace Deneme.Controllers
{
    public class ContactController : Controller
    {

        private readonly BlogDbContext _db;

        public ContactController(BlogDbContext db)
        {
            _db = db;
        }


        public IActionResult Message()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(Message message)
        {
            if (ModelState.IsValid)
            {
                message.CreatedAt = DateTime.Now;
                _db.Messages.Add(message);
                _db.SaveChanges();
                TempData["Succes"] = "Mesajınız başarıyla gönderildi. teşekkür ederiz.";
                return RedirectToAction("Message");
            }

            TempData["Error"] = "Lütfen tüm alanları eksiksiz doldurun!";
            return View("Message", message);
        }
        public IActionResult List()
        {
            var messages = _db.Messages.OrderByDescending(x => x.CreatedAt).ToList();
            return View(messages);


        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var message = _db.Messages.FirstOrDefault(P => P.Id == id);
            if (message == null)
            {
                TempData["Error"] = "MEsaj bulunamadı!";
                return RedirectToAction("List");
            }
            _db.Messages.Remove(message);
            _db.SaveChanges();
            TempData["Success"] = "Mesaj başarıyala silindi.";
            return RedirectToAction("List");

        }



    }
}
