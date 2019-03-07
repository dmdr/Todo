using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Todo.Models;

namespace Todo.ViewModels
{
    public class UserTasksViewModel
    {
        public UserTasksViewModel(string userName)
        {
            this.UserName = userName;
        }

        public string UserName { get; set; }
        public List<Todotask> Todotasks;
    }
}