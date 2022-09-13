using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TaskParallelLibrary_TPL_
{
    /*
  * task parallel library(tpl) nedir?
  * thirdparty değildir.
  * bu teknoloji ya da kütüphane ile kodlarımızı birden fazla thread'de zorlanmadan çalıştırabiliriz.
  * yoğun işlemlerimiz varsa ve bunları birden fazla thread ile çok kısa bir sürede sonuç almak istersek kullanabiliriz.
  * eğer kalabalık bir array'imiz var ise, bunların içerisindeki item'ları çeşitli parçalara bölüp her bir parçaya bir thread atarak işlemleri gerçekleştirir. Bu sayede uzun süren işlemlerin işlenme süresini kısaltmış olmaktadır.
  * thread yönetimi ile ilgilenmiyoruz, thread'leri herhangi bir göreve atamakla ilgilenmiyoruz ve bu thread'lerin yaşam döngüleri ile de ilgilenmiyoruz. Bunlarla TPL ilgileniyor olacak.
  */

    /*
     * race condition nedir?
     * çoklu thread uygulamalarda, birden çok thread'in paylaşılan bir hafıza alanına aynı anda erişmeye çalışmasıdır.
     * bu durum illaki olacak diye bir durum yok ama olma ihtimali vardır.
     * engellemek için thread'i kitlemek gerekiyor. tabii bu durumda da performansta ciddi bir düşüş de olacaktır.
     * paylaşımlı data kullanmadan yaparsak kodlarımız daha hızlı çalışacaktır çünkü herhangi bir lag mekanizması olmayacak.
     * örnek bir senaryo vermek gerekirse; a ve b thread'lerimiz ortak bir data üzerinde(int 5değerinde) veri artırımı yapsın. a thread'i t1 zamanda 5 değerini alsın ve b thread'i de t2 zamanında 5 değerini alsın. ardından a thread'i t3 zamanında 5 değerini 1 artırsın ve b thread'i de t4 zamanında 5 değerini 1 arttırsın. her iki thread'in de ulaşacağı nihai sonuç a için t5 ve b için t6 zamanında aynı olan 6 değeri olacaktır. fakat olması gereken sonuç b thread'inin 7 sonucunu döndürmesi olacaktır.
     */

    /*
     * Parallel.ForEach metodu
     * içerisine almış olduğu bir array'i ve array içerisindeki itemleri farklı thread'lerde çalıştırarak multithread bir kod yazmamıza imkan verir.
     * bunu nasıl yapar -> foreach array'i bu kütüphane belli parçalara ayırmaktadır. mesela 500 item'a sahip bir array verirsek, bunları 100 parçaya ayırıp her bir parçaya bir thread atar. ilgili thread kendisine alanan parçayı işlemeye çalışır.
     * Eğer yapılacak olan işlem küçük ise performansı olumsuz etkilemektedir.
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            string picturesPath = @"C:\Users\dogus\Pictures\TPL";

            var files = Directory.GetFiles(picturesPath);//dosyaları aktarmak için. her bir dosyanın yolunu alır.

            Parallel.ForEach(files, (item) =>
            {
                Console.WriteLine("thread no: " + Thread.CurrentThread.ManagedThreadId);

                Image img = new Bitmap(item);

                var thumbnail = img.GetThumbnailImage(50, 50, () => false,IntPtr.Zero);

                thumbnail.Save(Path.Combine(picturesPath, "thumbnail",Path.GetFileName(item)));


            });
            sw.Start();
            Console.WriteLine("İşlem Bitti: "+sw.ElapsedMilliseconds);
            
            sw.Reset();
            
            sw.Start();
            files.ToList().ForEach(x =>
            {
                Console.WriteLine("thread no: " + Thread.CurrentThread.ManagedThreadId);

                Image img = new Bitmap(x);

                var thumbnail = img.GetThumbnailImage(50, 50, () => false, IntPtr.Zero);

                thumbnail.Save(Path.Combine(picturesPath, "thumbnail", Path.GetFileName(x)));
            });
            sw.Stop(); Console.WriteLine("İşlem Bitti: " + sw.ElapsedMilliseconds);


        }
    }
}
