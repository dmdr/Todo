using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Todo.Models;
using Todo.ViewModels;
using Microsoft.AspNet.Identity;


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