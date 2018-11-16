using System;
using System.Collections.Generic;
using System.IO;

namespace TODO_Application
{
    class Program
    {
        List<Task> fileText = new List<Task>();

        static void Main(string[] args)
        {
            Program program = new Program();
            program.LoadTask();
            program.CommandEntry(args);
            
        }

        private void CommandEntry(string[] args)
        {
            if (args.Length == 0)
            {
                PrintUsage();
            }
            else if (args.Length >= 1)
            {
                switch (args[0])
                {
                    case "-l":
                        if (args.Length == 1)
                        {
                            ListTask();
                        }
                        else
                        {
                            Console.WriteLine("\"-l\" command doesn`t have argments");
                        }; break;
                    case "-a":
                        try
                        {
                            AddTask(args[1]);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Unable to add: no task provided");
                        };
                        break;
                    case "-r":
                        try
                        {
                            RemoveTask(args[1]);
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("Unable to remove: no index provided");
                        }
                        ; break;
                    case "-c":
                        try
                        {
                            CheckTask(args[1]);
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("Unable to check: no index provided");
                        }; break;
                    default:
                        {
                            Console.WriteLine("Unsupported command");
                            PrintUsage();
                        }
                        ; break;
                }
            }
        }

        private void CheckTask(string index)
        {
            try
            {
                LoadTask();
                int linesOfNum = int.Parse(index);
                if (linesOfNum > fileText.Count)
                {
                    Console.WriteLine("Unable to check: index is out of bound");
                    return;
                }
                fileText[linesOfNum - 1].setComplete = "t";

                WriteToFile();
                ListTask();
            }
            catch (FormatException)
            {
                Console.WriteLine("Unable to remove: index is not a number");
            }
        }

        private void RemoveTask(string line)
        {            
            try
            {
                LoadTask();

                int linesOfNum = int.Parse(line);
                if (linesOfNum > fileText.Count)
                {
                    Console.WriteLine("Unable to remove: index is out of bound");
                    return;
                }
                fileText.Remove(fileText[linesOfNum - 1]);

                WriteToFile();
                ListTask();
            }
            catch (FormatException)
            {
                Console.WriteLine("Unable to remove: index is not a number");
            }
            
        }

        private void WriteToFile()
        {
            using (StreamWriter writer = new StreamWriter("Task.txt"))
            {
                foreach (var item in fileText)
                {
                    writer.WriteLine(item.taskName + "|" + item.setComplete);
                }
            }
            ListTask();
        }

        private void AddTask(string task)
        {
            using (StreamWriter writer = new StreamWriter("Task.txt", true))
            {
                writer.WriteLine(task + "|" + "f");
            }
            ListTask();
        }
        private void ListTask()
        {
            LoadTask();
            int i = 1;
            foreach(var elem in fileText)
            {
                string completed = elem.setComplete;
                if (completed == "f")
                {
                    completed = "[ ] ";
                }
                else
                {
                    completed = "[X] ";
                }
                Console.WriteLine(i++ + " - " + completed + elem.taskName);
            }
        }

        private void LoadTask()
        {
            fileText = new List<Task>();
            using (FileStream fileStream = File.Open("Task.txt", FileMode.OpenOrCreate))
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    string readLine = reader.ReadLine();
                    while (readLine != null)
                    {
                        fileText.Add(new Task(readLine.Split("|")[0], readLine.Split("|")[1]));
                        readLine = reader.ReadLine();
                    }                    
                }
            }
        }

        private void PrintUsage()
        {
            Console.WriteLine("Command Line Todo application\n" +
                                "=============================\n" +
                                "\n" +
                                "Command line arguments:\n" +
                                "-l   Lists all the tasks\n" +
                                "-a   Adds a new task\n" +
                                "-r   Removes an task\n" +
                                "-c   Completes an task\n"
                                );
        }
    }
}
