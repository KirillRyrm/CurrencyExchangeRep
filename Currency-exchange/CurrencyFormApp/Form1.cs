using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;
using MyLib;

namespace CurrencyFormApp
{
    public partial class Form1 : Form
    {
        Dictionary<string, double> rates = new Dictionary<string, double> ();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<CurrencyRate> ratesList = GetRates();
            foreach (CurrencyRate rate in ratesList)
            {
                rates.Add(rate.cc, rate.rate);
            }
            FillComboBoxes(ratesList);
        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            
            // 
            FillCurrencyList(GetRates());
        }

        private List<CurrencyRate> GetRates() {
            string date = dateTimePickerCurrencyDate.Value.ToString("yyyyMMdd");
            string URI = $"https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?date={date}&json";

            // 
            string webResponseString = MyLib.Web.GetWebContent(URI);

            // Список объектов типа CurrencyRate
            List<CurrencyRate> currRates = JsonConvert.DeserializeObject<List<CurrencyRate>>(webResponseString);
            return currRates;
        }

        string GetWebContent(string UriString)
        {
            string webResponseString;

            // Create a request for the URL.
            WebRequest request = WebRequest.Create(UriString);
            request.Method = WebRequestMethods.Http.Get;
            //request.Credentials = CredentialCache.DefaultCredentials;

            using (WebResponse response = request.GetResponse())
            {
                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader streamRdr = new StreamReader(dataStream);
                    webResponseString = streamRdr.ReadToEnd();
                }
            };

            return webResponseString;
        }

        void FillCurrencyList(List<CurrencyRate> currRates)
        {
            // Очищаем listViewCurrates от ранее загруженных элементов
            listViewCurrates.Items.Clear();

            foreach (var item in currRates)
            {
                if (item.txt.Contains(textBoxFilter.Text))
                {
                    listViewCurrates.Items.Add(new ListViewItem(item.ToStringArray()));
                }
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        void FillComboBoxes(List<CurrencyRate> currRates)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();

            foreach (var item in currRates)
            {
                comboBox1.Items.Add(item.cc);
                comboBox2.Items.Add(item.cc);
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double a;
                rates.TryGetValue(comboBox1.SelectedItem.ToString(), out a);
                double b;
                rates.TryGetValue(comboBox2.SelectedItem.ToString(), out b);
                double c = Convert.ToDouble(textBox1.Text);
                label5.Text = Convert.ToString(a / b * c);
            } catch { }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePickerCurrencyDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
        
