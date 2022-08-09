using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TaskConsoleApp.WhenAll
{
    public class Content
    {
        public string Site { get; set; }
        public int Len { get; set; }
    }

   

    internal class Program
    { //WhenAll(task1,task2,task3,....) metodu
      //bu metot; parametre olarak task array alır ve bu array içerisindeki task'lerin tamamı sonuçlanıncaya kadar bekler. Hepsi sonuçlanınca tüm dataları döner.

      //WhenAny(task1,task2,task3,...) metodu
      //-> Task array'i alır parametre olarak. Bu taskler içerisinde de ilk biteni alır, geriye text olarak döner.

      //WaitAll(task1,task2,task3,...) metodu
      //task array alır ve task'ler tamamlanana kadar bekler. WhenAll metodundan farkı ise; whenAll metodu ana thread'i bloklamıyorken, WaitAll metodu bloklama işlemini gerçekleştirmektedir. UI Thread'i(Main thread'i) bloklamaktadır. Kullanılması çok fazla tercih edilmez. whenAll metodundan farkı ise parametre olarak milisaniye cinsinden bir parametre alır. Vermiş olduğumuz görevler parametrede vermiş olduğumuz süre içerisinde yapılırsa true, yapılmazsa false dönmektedir.

      //WaitAny metodu
      //WaitAll metodu gibi bloklayan bir yapıdadır. Nerede çağırılıyor ise o thread'i bloklar. array almaktadır. bu array içerisinde herhangi birisi tamamlandığı zaman geriye bir integer değer döner. Bu integer değer ise tamamlanan task'in index numarasıdır. bu index numarası üzerinden tamamlanan task'i alabiliriz. milisaniye alır.
      
      //Delay metodu
      //Asenkron bir şekilde gecikme gerçekleştirir fakat güncel thread'i bu işlemi yaparken bloklamaz.

        private async static Task Main(string[] args)
        {
            Console.WriteLine("Main Thread : " + Thread.CurrentThread.ManagedThreadId);
            List<string> urlsList = new List<string>()
            {
               // "https://www.google.com",
               // "https://www.microsoft.com",
                "https://www.amazon.com",
                "https://www.n11.com"
             //   "https://www.udemy.com"
            };

            //Her bir istek için text geri döner. Dolayısıyla bir text dizin oluşturmamız gerekmektedir.
            List<Task<Content>> taskList = new List<Task<Content>>();
            urlsList.ToList().ForEach(x =>
            {
                taskList.Add(GetContentAsync(x));
            });

            //Delay start
            Console.WriteLine("delay metodundan önce");
            var contents = await Task.WhenAll(taskList.ToArray());
            contents.ToList().ForEach(x =>
            {
                Console.WriteLine(x.Site);
            });
            //Delay end

            //WaitAny start
            //Console.WriteLine("WaitAny metodundan önce");
            
            //var firstTaskIndex =Task.WaitAny(taskList.ToArray());

            //Console.WriteLine($"{taskList[firstTaskIndex].Result.Site} - {taskList[firstTaskIndex].Result.Len}");

            //Console.WriteLine("WaitAny metodundan sonra");
            //WaitAny end

            //WaitAll start
            //Console.WriteLine("WaitAll metodundan önce");
            ////1.örnek// Task.WaitAll(taskList.ToArray());
            ////2.örnek
            //bool result = Task.WaitAll(taskList.ToArray(), 3000);
            //Console.WriteLine("3 saniyede geldi mi:" + result);
            //Console.WriteLine("WaitAll metodundan sonra");
            //Console.WriteLine($"{taskList.First().Result.Site} - {taskList.First().Result.Len}");
            //WaitAll end


            //WhenAny start
            // var FirstData = await Task.WhenAny(taskList);
            // Console.WriteLine($"{FirstData.Result.Site} - {FirstData.Result.Len}");
            //WhenAny end


            //var contents = await Task.WhenAll(taskList.ToArray()); //await kullanmasaydık text Content alacaktık, fakat await kullandığımız için tüm data alım işlemi bittikten sonra tipi Content array olacak.

            //contents.ToList().ForEach (x =>
            //{
            //    Console.WriteLine($"Site ismi: {x.Site} __ Boyut: {x.Len}");
            //}) ;

            //await kullanmadan yapmak istersek >>>
            //   var contentsNoAwait = Task.WhenAll(taskList.ToArray());
            //   Console.WriteLine("WhenAll metodundan await çıkarıldı. Başka işler yapılıyor bu satırlarda");

            //   var data = await contentsNoAwait;
            ////   Console.WriteLine("await olmadan okunacak olan datalar listeleniyor");

            //   data.ToList().ForEach(x =>
            //  {
            //      Console.WriteLine($"site adı: {x.Site} ___ boyutu: {x.Len}");
            //  });

        }


        public static async Task<Content> GetContentAsync(string url)
        {
            Content c = new Content();

            var data = await new HttpClient().GetStringAsync(url);

            await Task.Delay(5000); //Delay method  

            c.Site = url;
            c.Len = data.Length;

            Console.WriteLine("GetContentAsync thread: " + Thread.CurrentThread.ManagedThreadId); //çalışan thread'in id'sini alıyoruz.

            return c;
        }


    }


}
