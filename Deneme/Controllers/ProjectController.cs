using Deneme.Context;
using Deneme.Models;
using Deneme.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Deneme.Controllers
{
    public class ProjectController : Controller
    {
        private readonly BlogDbContext _db;

        public ProjectController(BlogDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var projects = _db.Projects.ToList();

            var anaSayfaListesi = projects.Select(p => new AnaSayfa
            {
                Name = p.Name,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Tags = p.Tags,
                PublishDate = p.CreatedAt,
                ViewCount = p.ViewCount,
                LikeCount = p.LikeCount,
                CommentCount = _db.Comments.Count(c => c.ProjectId == p.Id),
                Status = 1,
                ProjectId = p.Id

            }).ToList();

            return View(anaSayfaListesi);
        }

        public IActionResult Details(int id)
        {
            var project = _db.Projects.FirstOrDefault(x => x.Id == id);
            if (project == null)
                return NotFound();
            project.ViewCount += 1;
            _db.SaveChanges();
            var comments = _db.Comments
                .Where(x => x.ProjectId == id)
                .OrderByDescending(c => c.PublishDate)
                .ToList();

            var anaSayfa = new AnaSayfa
            {
                Name = project.Name,
                Description = project.Description,
                ImageUrl = project.ImageUrl,
                Tags = project.Tags,
                PublishDate = project.CreatedAt,
                ViewCount = project.ViewCount,
                LikeCount = project.LikeCount,
                CommentCount = comments.Count,
                Status = 1,
                ProjectId = project.Id
            };

            var viewModel = new ViewDetailsModel
            {
                Project = project,
                Comments = comments,
                NewComment = new Comment()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddComment(Comment comment)
        {
            if (!ModelState.IsValid)
            {
                var project = _db.Projects.FirstOrDefault(x => x.Id == comment.ProjectId);
                var comments = _db.Comments
                                  .Where(c => c.ProjectId == comment.ProjectId)
                                  .OrderByDescending(c => c.PublishDate)
                                  .ToList();

                var anaSayfa = new AnaSayfa
                {
                    Name = project?.Name,
                    Description = project?.Description,
                    ImageUrl = project?.ImageUrl,
                    Tags = project?.Tags,
                    PublishDate = project?.CreatedAt ?? DateTime.Now,
                    ViewCount = 0,
                    LikeCount = 0,
                    CommentCount = comments.Count,
                    Status = 1,
                    ProjectId = comment.ProjectId
                };

                var viewModel = new ViewDetailsModel
                {
                    Project = project,
                    Comments = comments,
                    NewComment = comment
                };

                return View("Details", viewModel);
            }

            comment.PublishDate = DateTime.Now;
            _db.Comments.Add(comment);
            _db.SaveChanges();

            return RedirectToAction("Details", new { id = comment.ProjectId });
        }

        public IActionResult About()
        {

            return View();
        }

        
        public IActionResult List()
        {
            var project = _db.Projects.OrderByDescending(p => p.CreatedAt).ToList();
            return View(project);
        }

        public IActionResult Delete(int id)
        {
            var project = _db.Projects.FirstOrDefault(x => x.Id == id);
            if (project == null)
                return NotFound();

            _db.Projects.Remove(project);
            _db.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult Edit(int id)
        {
            var project = _db.Projects.FirstOrDefault(p => p.Id == id);
            if (project == null)
                return NotFound();

            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Project model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existing = _db.Projects.FirstOrDefault(x => x.Id == model.Id);
            if (existing == null)
                return NotFound();

            if (model.ImageFile != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageFile.CopyTo(stream);
                }
                model.ImageUrl = "/images/" + fileName;
            }

            existing.Name = model.Name;
            existing.Description = model.Description;
            existing.Tags = model.Tags;
            existing.ImageUrl = model.ImageUrl;
            existing.CreatedAt = DateTime.Now;
            existing.PublishToBlog = model.PublishToBlog;

            _db.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult Create()
        {

           return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Project model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.ImageFile != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

               
                var extension = Path.GetExtension(model.ImageFile.FileName).ToLower();

             
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("ImageFile", "Sadece resim dosyaları yükleyebilirsiniz.");
                    return View(model);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageFile.CopyTo(stream);
                }
                model.ImageUrl = "/images/" + fileName;
            }

            model.CreatedAt = DateTime.Now;
            _db.Projects.Add(model);
            _db.SaveChanges();

            return RedirectToAction("List");
        }
        [HttpPost]
        public IActionResult LikeProject(int id)
        {
            var project = _db.Projects.FirstOrDefault(p => p.Id == id);
            if (project == null)
                return NotFound();

            project.LikeCount += 1;
            _db.SaveChanges();

            return RedirectToAction("Details", new { id = id });
        }
    }
}