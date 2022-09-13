using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TaskInstance
{
    /*
     * Task Instance ->>>Propterty: Result
     * async metot çağırdığımızda geriye yeni bir task instance'ı döner (nesne örneği).
     * bu property ile dönen datayı alabiliriz. fakat bunun bir dezavantajı vardır. ->>> bu property'si o andaki thread'i bloklar.
     * neden ihtiyaç duyulur ->> normal bir metot içerisinden async metot çağırmak istersek bunu kullanarak istediğimiz datayı alabiliriz.
     * datanın geldiğinden eminsek kullanabiliriz.
     */
    internal class Program
    {
        private async static Task Main(string[] args)
        {
            Console.WriteLine(GetData());


        }
        public static string GetData()
        {
            var task = new HttpClient().GetStringAsync("https://www.google.com");
            return task.Result; //task değişkeninin sonucu 5sn sürüyorsa bu satıra geldiğinde o anki thread'i bloklar. buradan sonuç dönünceye kadar thread bloklanacağından dolayı responsive'liği kaybederiz. form app ise pencere oynatması, herhangi bir butona tıklama gibi işlemleri yapamaz, ekran donacaktır.
        }
    }
}
