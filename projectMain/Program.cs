
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters;
using System.IO;
using System.Diagnostics.Metrics;
using System.Text;

namespace programm
{
    class firstMain
    {
        static void Main(string[] args)
        {
            if (args.Length < 0)
            {
                Console.WriteLine("Args not found!");
            }
            else
            {
                Configuration conf = new Configuration();
                conf.strTreatment(args);
                
            }
        }
    }

    class Configuration
    {
        Params par = new Params();
        List<string> Comands = new List<string>() 
            {"-h", "--help", "-l", "--list", "-a", "--add",
            "--login", "-d", "--delete"};
        public void strTreatment(string[] str) 
        {
            int count = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (Comands.Contains(str[i]))
                {
                    count += 1;
                }
            }
            if (count != 1)
            {
                Console.WriteLine("invalid string input format");
            }
            else
            {
                switch (str[0]) 
                {
                    case "--help":
                    case "-h":
                        par._help();
                        break;
                    case "--list":
                    case "-l":
                        par._list();
                        break;
                    case "--add":
                    case "-a":
                        if ((str[1] == "-u") && (str[3] == "-p"))
                        {
                            par._add(str[2], str[4]);
                        }
                        else
                        {
                            par._add(str[4], str[2]);
                        }
                        break;
                    case "--login":
                        if ((str[1] == "-u") && (str[3] == "-p"))
                        {
                            par._login(str[2], str[4]);
                        }
                        else
                        {
                            par._login(str[4], str[2]);
                        }
                        break;
                    case "-d":
                    case "--delete":
                        par._delete(str[2]);
                        break;

                }
            }
            
        }

    }

    class Params
    {
        public void _help()
        {
            Console.WriteLine("-h, --help - выводит справочную информацию о работе программы");
            Console.WriteLine("-l, --list - выводит список всех зарегистрированных логинов");
            Console.WriteLine("--add, -a  - добавляет пару юзер/пароль");
            Console.WriteLine("    -u \"user\" -p \"password\"");
            Console.WriteLine("--login    - позволяет залогироваться в систему под введёным юзером");
            Console.WriteLine("    -u \"user\" -p \"password\"");
            Console.WriteLine("-d, --delete - удаляет юзера");
            Console.WriteLine("    -u \"user\"");
        }

        public void _list()
        {
            int count = 0;
            using (FileStream file = File.OpenRead("dataBase.txt"))
            {
                byte[] data = new byte[file.Length];
                file.Read(data, 0, data.Length);    

                string list = System.Text.Encoding.Default.GetString(data);
                if (list.Length <= 1)
                {
                    Console.WriteLine("none");
                }
                else
                {
                    Console.WriteLine(list);
                }
            }
        }
         
        public void _add(string login, string password)
        {
            string line = login + " " + password +"\n";
            using (FileStream file = new FileStream("dataBase.txt", FileMode.Append))
            {
                byte[] bytes = System.Text.Encoding.Default.GetBytes(line);
                file.Write(bytes, 0, bytes.Length);
                Console.WriteLine("Login added");
            }
        }

        public void _login(string login, string password)
        {
            bool flag = false;
            string[] lines = File.ReadAllLines("dataBase.txt");
            foreach (string s in lines)
            {
                string[] str = s.Split(' ');
                if ((str[0] == login) && (str[1] == password))
                {
                    flag = true;
                    Console.WriteLine("authorization was successful");
                    break;
                }
            }
            if (flag == false)
            {
                Console.WriteLine("Incorrect login or password");
            }
        }
        public void _delete(string login)
        {
            var file = File.ReadAllLines("dataBase.txt", Encoding.Default).Where(s => !s.Contains(login));
            File.WriteAllLines("dataBase.txt", file, Encoding.Default);
        }
    }

}

/*  
 *    case "-h":
      case "--help":

Console.WriteLine("-h - выводит справочную информацию о работе программы");
Console.WriteLine("-l - выводит список всех зарегистрированных логинов");
Console.WriteLine("--add  - добавляет пару юзер/пароль");
Console.WriteLine("--login    - позволяет залогироваться в систему под введёным юзером");
Console.WriteLine("-d, --delete - удаляет юзера");
Console.WriteLine("     -u |user|");
*/