using System;
using System.IO;
using System.Threading.Tasks;

namespace TaskConsoleApp.FromResult
{
    internal class Program
    {
        //FromResult metodu
        /*
         * parametre olarak obje alır. Bu objeyi geriye bir task nesne örneği ile döner
         * örn. bir metottan geriye daha önce almış olduğumuz statik bir datayı dönmek istersek kullanabiliriz.
         * genellikle cache'lenmiş datayı dönmek amacıyla kullanılır.
         * */
        public static string CacheData { get; set; }
        private async static Task Main(string[] args)
        {
            CacheData = await GetDataAsync();
            Console.WriteLine(CacheData);
        }

        public static Task<string> GetDataAsync()
        {
            //Task.Run<string>(() =>
            //{
            //    return "doğuş";
            //    return 6;
            //})

            if (String.IsNullOrEmpty(CacheData))
            {
                return File.ReadAllTextAsync("dosya.txt");
            }
            else
            {
                //return Task.FromResult(CacheData);
                return Task.FromResult<string>(CacheData); //generic hali tip güvenlidir
            }
        }
    }
}
