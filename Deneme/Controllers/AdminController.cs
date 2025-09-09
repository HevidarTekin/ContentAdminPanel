using Deneme.Context;
using Deneme.Models;
using Deneme.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Deneme.Controllers

{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly BlogDbContext _db;

        public AdminController(BlogDbContext db)
        {
            _db = db;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            base.OnActionExecuting(context);
        }
     
        public IActionResult Dashboard()
        {
            var model = new AdminDashboardViewModel
            {
                BlogCount = _db.Projects.Count(),
                UnreadMessageCount = _db.Messages.Count(m => !m.IsRead),
                UserCount = _db.Users.Count(), // Eğer IdentityDbContext kullanıyorsan
                LastMessage = _db.Messages.OrderByDescending(m => m.Id).FirstOrDefault()
            };

            return View(model);
        }

        public IActionResult Comments()
        {
            var comments = _db.Comments
                              .Include(c => c.Project)
                              .OrderByDescending(c => c.PublishDate)
                              .ToList();

            return View(comments);
        }

        [HttpPost]
        public IActionResult DeleteComment(int id)
        {
            var comment = _db.Comments.FirstOrDefault(c => c.CommentId == id);
            if (comment != null)
            {
                _db.Comments.Remove(comment);
                _db.SaveChanges();
                TempData["Success"] = "Yorum başarıyla silindi.";
            }
            else
            {
                TempData["Error"] = "Yorum bulunamadı veya zaten silinmiş.";
            }

            return RedirectToAction("Comments");
        }
        public IActionResult Users (){

            var users = _db.Users.OrderByDescending(P => P.CreatedDate).ToList();
            return View(users);
        }
        
        [HttpPost]
        public  IActionResult DeleteUser(string id)
        {
            var user = _db.Users.FirstOrDefault(x =>x.Id==id);
            if (user==null)
            {
                TempData["Error"] = "Kullanıcı bulunamadı!";
                return RedirectToAction("Users");
            }
            _db.Users.Remove(user);
            _db.SaveChanges();
            TempData["Success"] = "Kullanıcı başarıyal silindi";
            return RedirectToAction("Users");
      
        }

    }
}