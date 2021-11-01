using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Dommain
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("*** ПРОЦЕССЫ ***\n");
                string comm = Command();
                if (comm != "X") { UseMyCommand(comm); }
                Console.ReadLine();
            }
        }
        static string Command()
        {
            Console.WriteLine("Какую информацию нужно получить? \n" +
                " 1 - Список всех процессов\n 2 - Выбрать процесс по PID\n" +
                " 3 - Информация о потоках\n 4 - Информация о подключаемых модулях\n" +
                " 5 - Запуск процесса\n 6 - Останов процесса\n X - выход\n");
            Console.Write("Введите команду: ");
            string comm = Console.ReadLine();
            return comm;
        }
        static void UseMyCommand(string str)
        {
            switch (str)
            {
                case "X":
                    break;
                case "1":
                    AllInfoProcess();
                    break;
                case "2":
                    ProcInMyPid();
                    break;
                case "3":
                    Threads();
                    break;
                case "4":
                    InfoByModuleProc();
                    break;
                case "5":
                    StartProcess();
                    break;
                case "6":
                    StopProcess();
                    break;
                case "7":
                    FullNameProcess();
                        break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Команда на распознана!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
            }
            Console.WriteLine();
        }

        private static void FullNameProcess()
        {
            try
            {
                Process[] fullname = Process.GetProcessesByName("notepad");
                Console.WriteLine(fullname.Count());
            }
            catch (Exception)
            {

                throw;
            }
        }
        private static void InfoByModuleProc()
        {
            Console.Write("Введите PID-идентификатор: ");
            int pid = int.Parse(Console.ReadLine());
            try
            {
                var process = Process.GetProcessById(pid);
                Console.WriteLine("Подключаемые модули процесса {0}: \n", process.ProcessName);
                ProcessModuleCollection mds = process.Modules;
                foreach (ProcessModule item in mds)
                {
                    Console.WriteLine("--> Имя модуля: " + item.FileName);
                }
            }
            catch (Exception xe)
            {
                Console.WriteLine(xe.Message);
            }
        }

        private static void StopProcess()
        {
            Console.Write("Ввидите PID-идентификатор процесса, который нужно оставить: ");
            string pid = Console.ReadLine();
            try
            {
                int i = int.Parse(pid);
                var procc = Process.GetProcessById(i);
                procc.Kill();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void StartProcess()
        {
            try
            {
                _ = Process.Start("cmd.exe");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void Threads()
        {
            Console.Write("Введите PID-идентификатор");
            string pid = Console.ReadLine();
            Process myproc = null;
            try
            {
                int i = int.Parse(pid);
                myproc = Process.GetProcessById(i);
                ProcessThreadCollection collection = myproc.Threads;
                Console.WriteLine("Потоки процесса {0}: \n", myproc.ProcessName);
                foreach (ProcessThread item in collection)
                {
                    Console.WriteLine("--> Thread ID: {0}\tВремя: {1}\t Приоритет: {2}\t", item.Id, item.StartTime.ToShortDateString(), item.PriorityLevel);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ProcInMyPid()
        {
            Console.WriteLine("Введиет PID-процессора");
            string pid = Console.ReadLine();
            try
            {
                int a = int.Parse(pid);
                var v = Process.GetProcessById(a);
                Console.WriteLine("->PID {0} \tName: {1}",v.Id, v.ProcessName);
            }
            catch (Exception xe)
            {
                Console.WriteLine(xe.Message);
            }
        }

        static void AllInfoProcess()
        {
            var res = from a in Process.GetProcesses(".")
                      orderby a.Id
                      select a;
            Console.WriteLine("*** Текущие процессы *** ");
            foreach (var item in res)
            {
                Console.WriteLine("->PID {0} \tName: {1} \t", item.Id, item.ProcessName);
            }
        }
    }
}
