using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using portfolio_annette_arrigucci.Models;
using System.IO;
using PagedList;
using PagedList.Mvc;

namespace portfolio_annette_arrigucci.Controllers
{
    [RequireHttps]
    public class BlogPostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BlogPosts
        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var newPost = db.Posts.Where(r => r.Published == true).OrderByDescending(p => p.Created).ToPagedList(pageNumber, pageSize);
            return View(newPost);
        }

        // GET: BlogPosts/Details/5 - returns the Details view using the ID number
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    BlogPost blogPost = db.Posts.Find(id);
        //    if (blogPost == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(blogPost);
        //}

        //GET: BlogPosts/Details/{slug} - returns the Details view using the Slug
        public ActionResult Details(string Slug)
        {
            if (String.IsNullOrWhiteSpace(Slug))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost post = db.Posts.FirstOrDefault(p => p.Slug == Slug);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: BlogPosts/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            BlogPost model = new BlogPost();
            model.Created = DateTimeOffset.Now; //setting a value as a hidden input to the Create view, this is to prevent a null value when we do model binding
            return View("Create", model);
        }

        // POST: BlogPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Created,Updated,Title,Slug,Preview,Body,MediaURL,Published")] BlogPost blogPost, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {  //check the file name to make sure its an image                 
                var ext = Path.GetExtension(image.FileName).ToLower();
                if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".gif" && ext != ".bmp") 
                ModelState.AddModelError("image", "Invalid Format.");
            }

            if (ModelState.IsValid)
            {
                blogPost.Created = DateTimeOffset.Now; //now we overwrite the Created property with the actual time of publication

                //create the blog post preview for the index page
                if (blogPost.Body.Length < 500)
                {
                    blogPost.Preview = blogPost.Body;
                }
                else
                {
                    blogPost.Preview = blogPost.Body.Substring(0, 500) + "...";
                }

                if (image != null)
                {                          
                    var filePath = "/Uploads/";      //relative server path  - to where my machine is                   
                    var absPath = Server.MapPath("~" + filePath);    // path on physical drive on server                      
                    blogPost.MediaURL = filePath + image.FileName;    // media url for relative path                         
                    image.SaveAs(Path.Combine(absPath, image.FileName)); //save image
                }
                //create the slug for the friendly URL
                var Slug = StringUtilities.URLFriendly(blogPost.Title);
                if (String.IsNullOrWhiteSpace(Slug))
                {
                    ModelState.AddModelError("Title", "Invalid title");
                    return View(blogPost);
                }
                if(db.Posts.Any(p => p.Slug == Slug))
                {
                    ModelState.AddModelError("Title", "Title must be unique");
                    return View(blogPost);
                }
                blogPost.Slug = Slug;

                db.Posts.Add(blogPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogPost);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Search(string searchStr, int? page)
        {
            if (string.IsNullOrEmpty(searchStr))
            {
                ModelState.AddModelError("Search term", "No search term entered");
            }
            if (ModelState.IsValid)
            {
                var results = db.Posts.Where(p => p.Body.Contains(searchStr))
                    .Union(db.Posts.Where(p => p.Title.Contains(searchStr)))
                    .Union(db.Posts.Where(p => p.Comments.Any(c => c.Body.Contains(searchStr))))
                    .Union(db.Posts.Where(p => p.Comments.Any(c => c.Author.DisplayName.Contains(searchStr))))
                    .Union(db.Posts.Where(p => p.Comments.Any(c => c.Author.FirstName.Contains(searchStr))))
                    .Union(db.Posts.Where(p => p.Comments.Any(c => c.Author.LastName.Contains(searchStr))))
                    .Union(db.Posts.Where(p => p.Comments.Any(c => c.Author.UserName.Contains(searchStr))))
                    .Union(db.Posts.Where(p => p.Comments.Any(c => c.Author.Email.Contains(searchStr))))
                    .Union(db.Posts.Where(p => p.Comments.Any(c => c.UpdateReason.Contains(searchStr))));

                int pageSize = 5;
                int pageNumber = (page ?? 1);

                return View(results.Where(r => r.Published == true).OrderByDescending(p => p.Created).ToPagedList(pageNumber, pageSize));
            }
            return RedirectToAction("Index");
        }


        // GET: BlogPosts/Edit/5
        //[Authorize(Roles = "Admin")]
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    BlogPost blogPost = db.Posts.Find(id);
        //    if (blogPost == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(blogPost);
        //}


        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string Slug)
        {
            if (String.IsNullOrWhiteSpace(Slug))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost post = db.Posts.FirstOrDefault(p => p.Slug == Slug);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: BlogPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Created,Title,Body,Slug,MediaURL,Published")] BlogPost blogPost, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {  //check the file name to make sure its an image                 
                var ext = Path.GetExtension(image.FileName).ToLower();
                if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".gif" && ext != ".bmp")
                    ModelState.AddModelError("image", "Invalid Format.");
            }
            if (ModelState.IsValid)
            {
                db.Entry(blogPost).State = EntityState.Modified;
                blogPost.Updated = DateTimeOffset.Now;
                //updating the preview to reflect any changes to the Body
                if (blogPost.Body.Length < 500)
                {
                    blogPost.Preview = blogPost.Body;
                }
                else
                {
                    blogPost.Preview = blogPost.Body.Substring(0, 500) + "...";
                }
                if (image != null)
                {
                    var filePath = "/Uploads/";      //relative server path  - to where my machine is                   
                    var absPath = Server.MapPath("~" + filePath);    // path on physical drive on server                      
                    blogPost.MediaURL = filePath + image.FileName;    // media url for relative path                         
                    image.SaveAs(Path.Combine(absPath, image.FileName)); //save image
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Delete/5
        //[Authorize(Roles = "Admin")]
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    BlogPost blogPost = db.Posts.Find(id);
        //    if (blogPost == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(blogPost);
        //}

        //GET: BlogPosts/Delete/{slug}
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string Slug)
        {
            if (String.IsNullOrWhiteSpace(Slug))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost post = db.Posts.FirstOrDefault(p => p.Slug == Slug);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: BlogPosts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    BlogPost blogPost = db.Posts.Find(id);
        //    db.Posts.Remove(blogPost);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        // POST: BlogPosts/Delete/{slug}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string Slug)
        {
            BlogPost blogPost = db.Posts.FirstOrDefault(p => p.Slug == Slug);
            db.Posts.Remove(blogPost);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
