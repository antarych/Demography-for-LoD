using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Net;

namespace Demography
{
    class Gender
    {
        public static int male, female;
        public Gender(int male, int female)
        {
            Gender.male = male;
            Gender.female = female;
        }
    }
    class Program
    {
    public static void Main()
    {
            TimerCallback time = new TimerCallback(Statistics);
            Timer MyTimer = new Timer(time, null, 0, 60000);
            Console.ReadLine();
        }
        public static void Statistics()
        {
            Statistics(null);
        }
        Gender def = new Gender(0, 0);
        private static void Statistics(object state)
        {
            
            WebClient MyClient = new WebClient();
            Stream MyStream = MyClient.OpenRead("http://api.lod-misis.ru/testassignment");
            StreamReader reader = new StreamReader(MyStream);
            string data = reader.ReadToEnd();
            MyStream.Close();
            reader.Close();
            try
            {
                string[] person = (data.Trim(new Char[] { '"' })).Split(new Char[] { ';' });
                foreach (string x in person)
                {
                    string[] st = x.Split(new Char[] { ':' });
                    if (st[0] == "Male")
                        {
                            if (st[1] == "Born") Gender.male += 1;
                            else Gender.male -= 1;
                        }
                    else
                        {
                            if (st[1] == "Born") Gender.female += 1;
                            else Gender.female -= 1;
                        }
                    }
                    Console.Clear();
                Console.Write("{0}: {1}; {2}: {3}", "Male", Gender.male.ToString("+#;-#;0"), "Female", Gender.female.ToString("+#;-#;0"));                   
                }
            catch (Exception)
            {
                    Console.Clear();
                    Console.Write("{0}: {1}; {2}: {3}", "Male", Gender.male.ToString("+#;-#;0"), "Female", Gender.female.ToString("+#;-#;0"));
            }
        }
    }
}
