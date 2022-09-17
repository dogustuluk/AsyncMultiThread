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

namespace TaskPLINQCancellationToken
{
    public partial class Form1 : Form
    {
        CancellationTokenSource cts;
        public Form1()
        {
            InitializeComponent();
            cts = new CancellationTokenSource();
        }
        private bool Hesapla(int x)
        {
            Thread.SpinWait(500);//herhangi bir thread'i bloklamayız.500 kere for döngüsü çalışıyormuş gibi düşünülebilir.
            return x % 12 == 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Task.Run(() => //ayrı bir thread içerisinde çalıştırmak için
            {
                try
                {
                    /*
                * buradaki iptal etme işlemi datanın listelenmediği zamanda yapılmalı. Eğer datalar listelenmeye başlarsa olmaz.
                */
                    Enumerable.Range(1, 100000).AsParallel().WithCancellation(cts.Token).Where(Hesapla).ToList().ForEach(x =>
                    {
                        Thread.Sleep(100);
                        cts.Token.ThrowIfCancellationRequested();//datalar akarken eğer iptal et butonuna tıklanırsa iptal edilmesini istiyorsak yazarız.
                        listBox1.Invoke((MethodInvoker)delegate { listBox1.Items.Add(x); });
                    });
                }
                catch (OperationCanceledException ex)
                {
                    MessageBox.Show("işlem iptal edildi.");
                }
                catch (Exception ex2)
                {
                    MessageBox.Show("Genel bir hata meydana geldi.");
                }
               
            });
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cts.Cancel();
        }
    }
}
