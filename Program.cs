using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Net;

namespace timer1
{
    class Gender
    {
        public static int male, female;
        public Gender(int male, int female)
        {
            Gender.male = male;
            Gender.female = female;
        }
        public static int Male
        {
            get { return male; }
            set { male = value; }
        }
        public static int Feale
        {
            get { return female; }
            set { female = value; }
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
            if (data == null) Console.WriteLine("no changes");
            else
            //Console.Write("{0}\n", data);
            try
            {
                string[] person = (data.Trim(new Char[] { '"' })).Split(new Char[] { ';' });
                foreach (string x in person)
                {
                    //Console.WriteLine(x);
                    string[] st = x.Split(new Char[] { ':' });
                    if (st[0] == "Male" & st[1] == "Born") Gender.male += 1;
                    if (st[0] == "Male" & st[1] == "Died") Gender.male -= 1;
                    if (st[0] == "Female" & st[1] == "Born") Gender.female += 1;
                    if (st[0] == "Female" & st[1] == "Died") Gender.female -= 1;
                }
                    Console.Clear();
                Console.Write("{0}: {1}; {2}: {3}", "Male", Gender.male.ToString("+#;-#;0"), "Female", Gender.female.ToString("+#;-#;0"));
                    //Console.Write("{0}: {1}\n {2}: {3}", "Male", Gender.male.ToString("+#;-#;0"), "Female", Gender.female.ToString("+#;-#;0"));
                }
            catch (Exception)
            {
                    //Console.WriteLine("No changes");
                    //Console.Write("{0}: {1}\n {2}: {3}", "Male", Gender.male.ToString("+#;-#;0"), "Female", Gender.female.ToString("+#;-#;0"));
                    Console.Clear();
                    Console.Write("{0}: {1}; {2}: {3}", "Male", Gender.male.ToString("+#;-#;0"), "Female", Gender.female.ToString("+#;-#;0"));
            }
        }
    }
}
