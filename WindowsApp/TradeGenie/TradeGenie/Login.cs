using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using KiteConnect;
using System.Net;

namespace TradeGenie
{
    public partial class Login : TradeGenieForm
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            GetLoginInfoFromDB();
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {

        }

        private void Login_Resize(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginToKiteConnect();
        }

        private void LoginToKiteConnect()
        {
            try
            {
                //https://aztgwebapp.azurewebsites.net/?request_token=53in4lKIiYotfMKKf1CAfGh71rWj4iCV&action=login&status=success

                if (string.IsNullOrEmpty(txtAccessToken.Text.Trim()))
                {

                    if (txt_retURL.Text.ToLower().Contains("aztgwebapp.azurewebsites.net"))
                    {
                        //requestToken = txt_retURL.Text.Split("&".ToCharArray())[2].Split("=".ToCharArray())[1];

                        string[] url = txt_retURL.Text.Split('?');
                        string[] queryString = url[1].Split('&');

                        for(int i=0; i < queryString.Length; i++)
                        {
                            if (queryString[i].StartsWith("request_token"))
                            {
                                requestToken = queryString[i].Split("=".ToCharArray())[1];
                            }
                        }

                        user = kite.GenerateSession(requestToken, UserConfiguration.APISecret);

                        UserConfiguration.AccessToken = user.AccessToken;
                        UserConfiguration.PublicToken = user.PublicToken;

                        MessageBox.Show("Please note down the Access token for today. Enjoy the trading !! ", "Login Successful!", MessageBoxButtons.OK);

                        SaveLoginInfoInDB(user, requestToken);

                        ShowMdiParent();
                    }
                    else
                    {
                        MessageBox.Show("Please try logging in to Kite thru a separate browser.", "Inalid URL!", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Kite First login for the day is recorded. Please use Access Token for further login's today.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Issue while Logging-in to Kite Connect. Please check error logs.", "Login Issue!", MessageBoxButtons.OK);
                Logger.ErrorLogToFile(ex);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void SaveLoginInfoInDB(User userInfo, string requestToken)
        {
            loginInfo.ClientId = userInfo.UserId;
            loginInfo.LoginDate = DateTime.Today;
            loginInfo.AccessToken = userInfo.AccessToken;
            loginInfo.PublicToken = userInfo.PublicToken;
            loginInfo.RequestToken = requestToken;

            TradeGenieForm.dbMethods.LoadUserLoginInformation(loginInfo);
        }

        private void GetLoginInfoFromDB()
        {
            loginInfo = TradeGenieForm.dbMethods.GetUserLoginInformation();

            txtAccessToken.Text = loginInfo.AccessToken;
            txtPublicToken.Text = loginInfo.PublicToken;
        }


        private void ShowMdiParent()
        {
            Master mdiParent = new Master();
            mdiParent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            mdiParent.Show();
        }

        private void btnSubsequentLogin_Click(object sender, EventArgs e)
        {
            UserConfiguration.AccessToken = txtAccessToken.Text;
            UserConfiguration.PublicToken = txtPublicToken.Text;

            ShowMdiParent();
        }

        private void btnGetKiteUrl_Click(object sender, EventArgs e)
        {
            initSession();
        }

        private void initSession()
        {
            try
            {
                txt_URL.Text = kite.GetLoginURL();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login Failed because of connectivity issues. Please Retry!");
                Logger.ErrorLogToFile(ex);
            }
        }
    }
}
