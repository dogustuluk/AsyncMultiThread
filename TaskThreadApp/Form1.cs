using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskThreadApp
{
    public partial class Form1 : Form
    {

        //Run metodu
        /*
         * Bazı kodlarımızı ayrı bir thread'te çalıştırmak için kullanırız. 
         * Hangi metotlaro çalışıtırmak uygundur; -> işlemcimizi yoracak olan işlemleri farklı bir thread'te çalıştırmak uygundur.(yoğun matematiksel işlemler içerdiği zaman), (antivirüs taraması yaparken[tüm dosyaları arayıp virüs olup olmadığını kontrol ediyor])
         * (dosyaya yazarke, dosyadan data okurken, veri tabanına data yazarken, veri tabanında okurken, web sayfasına istek yaparken) ayrı bir thread kullanmak uygun değildir. Belki bu işlemler yapılırken herhangi bir thread kullanılmayacaktır. bu işlemlerde best practise olarak asenkron programlama yapıcaz.
         */

        public static int Counter { get; set; } = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            //await Go(progressBar1);
            //await Go(progressBar2); //await keyword'ü sonuç alınıncaya kadar bekler.
            var aTask = Go(progressBar1);
            var bTask = Go(progressBar2);

            await Task.WhenAll(aTask, bTask);
        }

        public async /*void*/ Task Go(ProgressBar progressBar)
        {
            await Task.Run(() =>
            {
                Enumerable.Range(1, 100).ToList().ForEach(x =>
                {
                    Thread.Sleep(100);
                 //   progressBar.Value = x; //farkı bir thread'te çalıştığımız için UI thread'e erişilemeyecektir. Bunun için aşağıdaki şekilde Invoke ile yapmamız gerekiyor
                    progressBar.Invoke((MethodInvoker)delegate { progressBar.Value = x; });
                });
            });

            //Enumerable.Range(1, 100).ToList().ForEach(x =>
            // {
            //     Thread.Sleep(100);
            //     progressBar.Value = x;
            //     //çok hızlı döndüğü için asenkronmuş gibi görebiliriz. Aslında aynı thread içerisinde çalışmaktadırlar. bunun sebebi ise metot çok hızlı yanıt veriyor fakat UI taraf bu hıza yeterli bir şekilde karşılık veremiyor. Bu durumu gözlemlemek için thread'i blokluyoruz burada.
            // });
        }



        private void btnCounter_Click(object sender, EventArgs e)
        {
            btnCounter.Text = Counter++.ToString();
        }
    }
}
