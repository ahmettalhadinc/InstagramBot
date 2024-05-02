using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace instagramBot
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string password = textBox2.Text;

            IWebDriver browser = new ChromeDriver();

            string url = "https://www.instagram.com/";
            browser.Navigate().GoToUrl(url);
            Thread.Sleep(3000);
                        
            var kuladi = browser.FindElement(By.XPath("//*[@id=\"loginForm\"]/div/div[1]/div/label/input"));
            kuladi.SendKeys(email);

            var sifre = browser.FindElement(By.XPath("//*[@id=\"loginForm\"]/div/div[2]/div/label/input"));
            sifre.SendKeys(password);

            var instaLogin = browser.FindElement(By.XPath("//*[@id=\"loginForm\"]/div/div[3]/button"));
            instaLogin.Click();
            Thread.Sleep(4000);

        

            var takipcilistt = "https://www.instagram.com/"+email+"/followers/";
            browser.Navigate().GoToUrl(takipcilistt);
            Thread.Sleep(2500);

            WebDriverWait wait = new WebDriverWait(browser, TimeSpan.FromSeconds(10));
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            string jsCommand = "" +
                "followerss=document.querySelector('._aano');" +
                "followerss.scrollTo(0,followerss.scrollHeight);" +
                "var sayfaSonu= followerss.scrollHeight;" +
                "return sayfaSonu;";

            IJavaScriptExecutor js = (IJavaScriptExecutor)browser;
            int son = 0;
            int sayfaSonu = 0;
            while (true)
            {
                son = sayfaSonu;
                Thread.Sleep(1000);
                sayfaSonu = Convert.ToInt32(js.ExecuteScript(jsCommand));
                if (son == sayfaSonu)
                    break;
            }
            var takipciler = browser.FindElements(By.CssSelector("._ap3a._aaco._aacw._aacx._aad7._aade"));
            List<string> takipcilerListesi = new List<string>();
            foreach (var takipci in takipciler)
            {
                takipcilerListesi.Add(takipci.Text);
                richTextBox1.AppendText(takipci.Text + "\n");
            }

            var takipkapat = browser.FindElement(By.XPath("/html/body/div[6]/div[1]/div/div[2]/div/div/div/div/div[2]/div/div/div[1]/div/div[3]/div/button"));
            takipkapat.Click();
            var takipedilenlist = "https://www.instagram.com/"+email+"/following/";
            browser.Navigate().GoToUrl(takipedilenlist);
            Thread.Sleep(1500);

            string jsCommandd = "" +
                "followersss=document.querySelector('._aano');" +
                "followersss.scrollTo(0,followersss.scrollHeight);" +
                "var sayfaSonuu= followersss.scrollHeight;" +
                "return sayfaSonuu;";
            IJavaScriptExecutor jss = (IJavaScriptExecutor)browser;
            int sonn = 0;
            int sayfaSonuu = 0;
            
            while (true)
            {
                 sonn = sayfaSonuu;
                Thread.Sleep(1000);
                sayfaSonuu = Convert.ToInt32(jss.ExecuteScript(jsCommandd));
                if (sonn == sayfaSonuu)
                    break;
            }

            var takipEdilen = browser.FindElements(By.CssSelector("._ap3a._aaco._aacw._aacx._aad7._aade"));
            List<string> takipEdilenListesi = new List<string>();
            foreach (var takipcii in takipEdilen)
            {
                takipEdilenListesi.Add(takipcii.Text);
                richTextBox2.AppendText(takipcii.Text + "\n");
            }


            Thread.Sleep(500);
            foreach (string eleman in takipEdilenListesi)
            {
                if (!takipcilerListesi.Contains(eleman))
                {

                    richTextBox3.AppendText(eleman + "\n");
                }

            }
            Thread.Sleep(1000);
            browser.Quit();

        }


    }
}
