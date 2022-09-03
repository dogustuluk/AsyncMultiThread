using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskCancellationFormApp
{
    public partial class Form1 : Form
    {
        CancellationTokenSource ct = new CancellationTokenSource();

        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {/*
          * asenkron bir metodun token alabilmesi için constructor'ında overload'ı olması lazım.
          * her asenkron metodun token parametresi olmayabilir.
          * her bir asenkron metodun illa bir cancelation token'ı olmayabilir.
          */
            Task<HttpResponseMessage> mytask;

            try
            {
                mytask = new HttpClient().GetAsync("https://www.google.com", ct.Token);

                await mytask;

                var content = await mytask.Result.Content.ReadAsStringAsync();

                richTextBox1.Text = content;
            }
            catch (TaskCanceledException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ct.Cancel();
        }
    }
}
