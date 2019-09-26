using System;

namespace TEST
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            HTTP_GET_POST.Program.SendMailRemind(HTTP_GET_POST.Program.PrepareMailBody(null, "", 20));
        }
    }
}
