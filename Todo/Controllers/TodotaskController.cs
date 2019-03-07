using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Todo.Models;
using Todo.ViewModels;
using Microsoft.AspNet.Identity;

/* paste into the AddSampleuser migration Up method
           Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ecae62a3-c461-46b2-9e6f-7166badd68b5', N'Test2@Todo.com', 0, N'ABi2S+L1/Dn2oJ+x6U+N7anmgob6mZ+7E6U38JcI8nmeaguE5zCxruE9T1epf2Bfbw==', N'a0e14f87-5a5b-4dc7-aa1b-7f724fcdcadf', NULL, 0, 0, NULL, 1, 0, N'Test2')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'f16e0ad5-2326-45b8-9d25-efb524d0f1f9', N'Test@Todo.com', 0, N'APUjwvMCLjjWpQhdRfCMdr/OtIEPpSkumX5ivjG4DfJzYYidipKUyCgwkp2D8lk9VA==', N'9f23a751-8c60-4e88-8cf9-263056b0b2ab', NULL, 0, 0, NULL, 1, 0, N'Test')
            ");
 */

namespace Todo.Controllers
{
    public class TodotaskController : Controller
    {
        private ApplicationDbContext dbTodo;

        public TodotaskController()
        {
            dbTodo = new ApplicationDbContext();
        }

        //
        //
        protected override void Dispose(bool disposing)
        {
            dbTodo.Dispose();
            base.Dispose(disposing);
        }

        // GET: /Todotask/TodoList
        //public ActionResult TodoList(int? pageIndex, string soryBy)
        [HttpGet]
        public ActionResult TodoList()
        {
            var userId = User.Identity.GetUserId();
            var userName = User.Identity.Name;
            var userTasks = dbTodo.Todotasks.Where(m => m.UserId == userId).ToList();

            var userTaskViewModel = new UserTasksViewModel(userName)
            {
                Todotasks = userTasks
            };

            return View( "TodoList", userTaskViewModel);
        }

        // GET: /Todotask/NewTask
        public ActionResult NewTask()
        {
            var userId = User.Identity.GetUserId();
            var newTask = new Todotask( userId );

            return View("TodoForm", newTask);
        }

        
        // GET: /Todotask/EditTask
        public ActionResult EditTask( int id )
        {
            var editTask = dbTodo.Todotasks.SingleOrDefault(m => m.Id == id);
            if(editTask == null)
                return HttpNotFound();

            return View("TodoForm", editTask);
        }

        //
        //
        // POST: /Todotask/SaveTask
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveTask( Todotask todotask)
        {
            if (!ModelState.IsValid)
            {
                return View("TodoForm", todotask);
            }

            if ( todotask.Id == 0 )
            { // New Task
                todotask.UpdatedDT = DateTime.Now;
                dbTodo.Todotasks.Add(todotask);
            }
            else
            { // Edit Task
                var editTask = dbTodo.Todotasks.SingleOrDefault(m => m.Id == todotask.Id);
                if (editTask == null)
                    return HttpNotFound();

                editTask.Name = todotask.Name;
                editTask.Description = todotask.Description;
                editTask.Done = todotask.Done;
                editTask.UpdatedDT = DateTime.Now;
            }
            dbTodo.SaveChanges();
            return RedirectToAction("TodoList", "Todotask");
        }


        // POST: /Todotask/ToggleDone
        [HttpPost]
  //      [ValidateAntiForgeryToken]
        public ActionResult ToggleDone(int id)
        {
            var togDoneTask = dbTodo.Todotasks.SingleOrDefault(m => m.Id == id);
            if (togDoneTask == null)
                return HttpNotFound();

            togDoneTask.Done        = !togDoneTask.Done;
            togDoneTask.UpdatedDT   = DateTime.Now;
            dbTodo.SaveChanges();
 
            return RedirectToAction("TodoList", "Todotask");
        }

        // POST: /Todotask/DeleteTask
        [HttpPost]
//      [ValidateAntiForgeryToken]
        public ActionResult DeleteTask(int id)
        {
            var delTask = dbTodo.Todotasks.SingleOrDefault(m => m.Id == id);
            if (delTask == null)
                return HttpNotFound();

            dbTodo.Todotasks.Remove(delTask);
            dbTodo.SaveChanges();
            return RedirectToAction("TodoList", "Todotask");
        }

        //
        //
        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Todo Taks List Manager.";

            return View();
        }

        //
        //
        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = $"info@{ApplicationUser.EmailCompanyName}.";

            return View();
        }
    }
}
