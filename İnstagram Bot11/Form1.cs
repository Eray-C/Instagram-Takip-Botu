using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace İnstagram_Bot11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string kullaniciadi, sifre, girilenrandomsayi, Following;
        int sleeptime, randomsayi1, randomsayi2, bekletmesüresi;
        string[] parcalar;
        bool hata = false;
        private void button1_Click(object sender, EventArgs e)
        {

            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {


                if (textBox3.Text == "")
                {
                    MessageBox.Show("Açılacak Kişiyi Girmediniz");
                    return;
                }
                var driverService = ChromeDriverService.CreateDefaultService();
                driverService.HideCommandPromptWindow = true;
                IWebDriver driver = new ChromeDriver(driverService);
                driver.Navigate().GoToUrl("https://www.instagram.com");
                Thread.Sleep(5000);

                IWebElement userName = driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div/div/div[1]/section/main/article/div[2]/div[1]/div[2]/form/div/div[1]/div/label/input"));
                IWebElement password = driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div/div/div[1]/section/main/article/div[2]/div[1]/div[2]/form/div/div[2]/div/label/input"));
                IWebElement LoginBtn = driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div/div/div[1]/section/main/article/div[2]/div[1]/div[2]/form/div/div[3]/button"));

                userName.SendKeys(textBox1.Text);
                password.SendKeys(textBox2.Text);
                LoginBtn.Click();
                Thread.Sleep(5000);

                string username = textBox3.Text;
                driver.Navigate().GoToUrl("https://www.instagram.com/" + username);
                Thread.Sleep(7000);

                IWebElement FollowerLİnk = driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div/div/div[1]/div[1]/div[2]/section/main/div/header/section/ul/li[2]/a"));

                FollowerLİnk.Click();
                Thread.Sleep(5000);



                string ParentFollowerElementFullXPath = "/html/body/div[2]/div/div/div[2]/div/div/div[1]/div/div[2]/div/div/div/div/div[2]/div/div/div[2]/div[1]/div";

                IWebElement ParentFollowerElement = driver.FindElement(By.XPath(ParentFollowerElementFullXPath));

                IReadOnlyCollection<IWebElement> FollowerElements = driver.FindElements(By.XPath(ParentFollowerElementFullXPath + "/div"));
                Thread.Sleep(5000);

                string TakipçiListesininDivSırası;
                var SadeceXtümtakipçilerigörebilirDivXPath = "/html/body/div[2]/div/div/div[2]/div/div/div[1]/div/div[2]/div/div/div/div/div[2]/div/div/div[2]/div[1]";
                IWebElement SadeceXtümtakipçilerigörebilirDiv = driver.FindElement(By.XPath(SadeceXtümtakipçilerigörebilirDivXPath));

                if (SadeceXtümtakipçilerigörebilirDiv.Text == "Sadece instagram tüm takipçileri görebilir.")
                {
                    TakipçiListesininDivSırası = "2";
                }
                else
                {
                    TakipçiListesininDivSırası = "1";
                }

                string AnyButtonFullXPath = "/html/body/div[2]/div/div/div[2]/div/div/div[1]/div/div[2]/div/div/div/div/div[2]/div/div/div[2]/div[{1}]/div/div[{0}]/div/div/div/div[3]/div/button";

                for (int i = 1; ; i++)
                {
                    string CurrentButtonFullXPath = string.Format(AnyButtonFullXPath, i, TakipçiListesininDivSırası);
                    IWebElement FollowButton = driver.FindElement(By.XPath(CurrentButtonFullXPath));
                    try
                    {
                        IWebElement Hata = driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[2]/div/div[2]/div[1]/div/div[2]/div/div/div/div/div[2]/div/div/div[2]/button[2]"));
                        if (Hata.Text == "Tamam")
                        {
                            hata = true;
                            Hata.Click();
                            if (hata == true)
                            {
                                Thread.Sleep(TimeSpan.FromMinutes(bekletmesüresi));

                            }
                        }
                        else
                        {
                            continue;
                        }


                    }
                    catch { }
                    var color = FollowButton.GetCssValue("color");

                    if (color == "rgba(0, 0, 0, 1)")
                    {
                        continue;
                    }
                    else
                    {
                        FollowButton.Click();
                    }
                    if (radioButton2.Checked)
                    {
                        Thread.Sleep(sleeptime * 1000);
                    }
                    else if (radioButton1.Checked)
                    {
                        Random rnd = new Random();
                        Thread.Sleep(rnd.Next(randomsayi1, randomsayi2) * 1000);
                    }



                }
            });
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                textBox2.Focus();
            }
        }



        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                button1.PerformClick();
            }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            bekletmesüresi = Convert.ToInt16(textBox6.Text);
            sleeptime = Convert.ToUInt16(textBox4.Text);
            string veri = textBox5.Text;
            if (veri.Contains("-"))
            {
                girilenrandomsayi = textBox5.Text;
                parcalar = girilenrandomsayi.Split('-');
                randomsayi1 = int.Parse(parcalar[0]);
                randomsayi2 = int.Parse(parcalar[1]);
                MessageBox.Show("Değişiklikler kaydedildi.");
                //button3.Enabled = false;
            }
            else
            {
                MessageBox.Show("HATA sayıların arasında" + "      " + "-" + "     " + "olmalı");
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
            textBox6.Text = "5";
            bekletmesüresi = Convert.ToInt16(textBox6.Text);
            radioButton1.Checked = true;
            randomsayi1 = 7;
            randomsayi2 = 12;
            sleeptime = 5;
            textBox4.Text = "5";
            textBox5.Text = "7-12";
            Following = Properties.Settings.Default.following;
            textBox3.Text = Following;

            kullaniciadi = Properties.Settings.Default.Kullaniciadi;
            textBox1.Text = kullaniciadi;

            sifre = Properties.Settings.Default.Sifre;
            textBox2.Text = sifre;
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                textBox4.Enabled = false;
            }
            else
            {
                textBox4.Enabled = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                textBox5.Enabled = false;
            }
            else
            {
                textBox5.Enabled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Following = textBox3.Text;
                Properties.Settings.Default.following = Following;
                Properties.Settings.Default.Save();

                kullaniciadi = textBox1.Text;
                Properties.Settings.Default.Kullaniciadi = kullaniciadi;
                Properties.Settings.Default.Save();

                sifre = textBox2.Text;
                Properties.Settings.Default.Sifre = sifre;
                Properties.Settings.Default.Save();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
