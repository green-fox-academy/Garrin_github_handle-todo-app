using System;
using System.Collections.Generic;
using System.Text;

namespace TODO_Application
{
    class Task
    {
        public readonly string taskName;
        string taskCompelet;
        public string setComplete{
            get{
                return taskCompelet;
            }
            set
            {
                taskCompelet = value;
            }

        }

        public Task(string taskName, string taskCompelet)
        {
            this.taskName = taskName;
            this.taskCompelet = taskCompelet;
        }
    }
}
