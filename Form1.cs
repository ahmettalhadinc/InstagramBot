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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string password = textBox2.Text;
            string instagramUserName= textBox3.Text;
            IWebDriver browser = new ChromeDriver();

            string url = "https://www.instagram.com/";
            browser.Navigate().GoToUrl(url);
            Thread.Sleep(3000);

            var login =browser.FindElement(By.XPath("//*[@id='loginForm']/div/div[5]/button"));
            login.Click();

            var kuladi =browser.FindElement(By.XPath("//*[@id='email']"));
            kuladi.SendKeys(email);
            
            var sifre = browser.FindElement(By.XPath("//*[@id='pass']"));
            sifre.SendKeys(password);

            var facelogin = browser.FindElement(By.XPath("//*[@id='loginbutton']"));
            facelogin.Click();
            Thread.Sleep(15000);
            
            var reddet =browser.FindElement(By.XPath("/html/body/div[3]/div[1]/div/div[2]/div/div/div/div/div[2]/div/div/div[3]/button[2]"));
            reddet.Click();
            Thread.Sleep(2000);
            
           
            
            var takipcilist = "https://www.instagram.com/"+instagramUserName+"/followers/";
            browser.Navigate().GoToUrl(takipcilist);
            Thread.Sleep(2500);

            string jsCommand = "" +
                "followerss=document.querySelector('._aano');" +
                "followerss.scrollTo(0,followerss.scrollHeight);" +
                "var sayfaSonu= followerss.scrollHeight;" +
                "return sayfaSonu;";
            IJavaScriptExecutor js = (IJavaScriptExecutor)browser;
            var sayfaSonu = Convert.ToInt32(js.ExecuteScript(jsCommand));
            while(true)
            {
                var son = sayfaSonu;
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
            var takipedilenlist = "https://www.instagram.com/"+instagramUserName+"/following/";
            browser.Navigate().GoToUrl(takipedilenlist);
            Thread.Sleep(1500);

            string jsCommandd = "" +
                "followersss=document.querySelector('._aano');" +
                "followersss.scrollTo(0,followersss.scrollHeight);" +
                "var sayfaSonuu= followersss.scrollHeight;" +
                "return sayfaSonuu;";
            IJavaScriptExecutor jss = (IJavaScriptExecutor)browser;
            var sayfaSonuu = Convert.ToInt32(jss.ExecuteScript(jsCommandd));
            while (true)
            {
                var sonn = sayfaSonuu;
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
