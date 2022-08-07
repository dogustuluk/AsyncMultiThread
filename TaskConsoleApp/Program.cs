using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TaskConsoleApp
{
    internal class Program
    {
        //Task >>>>>> ContinueWith Metodu
        //bu metot; task sınıfından sonra bu metodu kullanırsak task'in içerisindeki işlem tamamlandıktan sonra çalışacak kodları ekleyebiliriz. Mesela Task'te verilerimiz okunurken, bu işlem bittikten sonra continueWith metodu içerindekiler çalışır.

        public static void calis(Task<string> data)
        {
            //100 satırlık kod olduğunu varsay burada.
            Console.WriteLine("ayrı metot çalışıyor");
            Console.WriteLine("ayrı metot data uzunluğu : " + data.Result.Length);
        }

        private async static Task Main(string[] args)
        {
            Console.WriteLine("Hello World!"); 



            var mytask = new HttpClient().GetStringAsync("https://www.google.com").ContinueWith((data) =>
            {
                //Result propertysi >>>> bu property'i, gelen daata bir task olduğundan dolayı sonucunu almak için kullanırız
                Console.WriteLine("data uzunluğu : " + data.Result.Length);

                //Buradaki kodların sonucu console ekranında en son çalışacaktır. çünkü await ile en son çekiyoruz, burada sadece taahüt oluşturuyoruz.

                //Eğer continueWidth içerisindeki kodlar çok uzun ise lambda ile girmek yerine ayrı bir metot tanımla.
            });

            var mytask3 = new HttpClient().GetStringAsync("https://www.google.com").ContinueWith(calis);

            Console.WriteLine("Arada yapılacak işler");

            await mytask;


            //Eğer ContinueWith metodunu kullanmak istemezsek ki bu şekilde kod okunabilirliği de artacaktır. Aşağıdaki şekilde yapılır.
            var mytask2 = new HttpClient().GetStringAsync("https://www.google.com");

            Console.WriteLine("Arada yapılacak olan işler 2");
            
            var data = await mytask2;

            Console.WriteLine("datanın uzunluğu 2 : " + data.Length);

        }
    }
}
