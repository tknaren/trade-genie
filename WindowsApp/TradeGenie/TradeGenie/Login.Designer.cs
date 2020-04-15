namespace TradeGenie
{
    partial class Login : TradeGenieForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txt_retURL = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_URL = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblStatusMsg = new System.Windows.Forms.Label();
            this.btnGetKiteUrl = new System.Windows.Forms.Button();
            this.txtAccessToken = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPublicToken = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSubsequentLogin = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Returned URL";
            // 
            // txt_retURL
            // 
            this.txt_retURL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txt_retURL.Location = new System.Drawing.Point(110, 117);
            this.txt_retURL.Name = "txt_retURL";
            this.txt_retURL.Size = new System.Drawing.Size(432, 23);
            this.txt_retURL.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(69, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "URL";
            // 
            // txt_URL
            // 
            this.txt_URL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txt_URL.Location = new System.Drawing.Point(110, 75);
            this.txt_URL.Name = "txt_URL";
            this.txt_URL.Size = new System.Drawing.Size(432, 23);
            this.txt_URL.TabIndex = 1;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.Ivory;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Location = new System.Drawing.Point(381, 161);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(161, 43);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Today\'s First Login";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lblStatusMsg
            // 
            this.lblStatusMsg.AutoSize = true;
            this.lblStatusMsg.Location = new System.Drawing.Point(302, 176);
            this.lblStatusMsg.Name = "lblStatusMsg";
            this.lblStatusMsg.Size = new System.Drawing.Size(0, 15);
            this.lblStatusMsg.TabIndex = 4;
            this.lblStatusMsg.Click += new System.EventHandler(this.label3_Click);
            // 
            // btnGetKiteUrl
            // 
            this.btnGetKiteUrl.BackColor = System.Drawing.Color.Ivory;
            this.btnGetKiteUrl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetKiteUrl.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetKiteUrl.Location = new System.Drawing.Point(110, 161);
            this.btnGetKiteUrl.Name = "btnGetKiteUrl";
            this.btnGetKiteUrl.Size = new System.Drawing.Size(162, 43);
            this.btnGetKiteUrl.TabIndex = 5;
            this.btnGetKiteUrl.Text = "Get Kite URL";
            this.btnGetKiteUrl.UseVisualStyleBackColor = false;
            this.btnGetKiteUrl.Click += new System.EventHandler(this.btnGetKiteUrl_Click);
            // 
            // txtAccessToken
            // 
            this.txtAccessToken.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtAccessToken.Location = new System.Drawing.Point(110, 236);
            this.txtAccessToken.Name = "txtAccessToken";
            this.txtAccessToken.Size = new System.Drawing.Size(432, 23);
            this.txtAccessToken.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 239);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Access Token";
            // 
            // txtPublicToken
            // 
            this.txtPublicToken.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtPublicToken.Location = new System.Drawing.Point(110, 276);
            this.txtPublicToken.Name = "txtPublicToken";
            this.txtPublicToken.Size = new System.Drawing.Size(432, 23);
            this.txtPublicToken.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 279);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Public Token";
            // 
            // btnSubsequentLogin
            // 
            this.btnSubsequentLogin.BackColor = System.Drawing.Color.Ivory;
            this.btnSubsequentLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubsequentLogin.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubsequentLogin.Location = new System.Drawing.Point(237, 321);
            this.btnSubsequentLogin.Name = "btnSubsequentLogin";
            this.btnSubsequentLogin.Size = new System.Drawing.Size(162, 43);
            this.btnSubsequentLogin.TabIndex = 10;
            this.btnSubsequentLogin.Text = "Subsequent Login";
            this.btnSubsequentLogin.UseVisualStyleBackColor = false;
            this.btnSubsequentLogin.Click += new System.EventHandler(this.btnSubsequentLogin_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(266, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 23);
            this.label5.TabIndex = 11;
            this.label5.Text = "Kite Connect";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 385);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnSubsequentLogin);
            this.Controls.Add(this.txtPublicToken);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtAccessToken);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnGetKiteUrl);
            this.Controls.Add(this.lblStatusMsg);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txt_URL);
            this.Controls.Add(this.txt_retURL);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Login";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.Resize += new System.EventHandler(this.Login_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_retURL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_URL;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblStatusMsg;
        private System.Windows.Forms.Button btnGetKiteUrl;
        private System.Windows.Forms.TextBox txtAccessToken;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPublicToken;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSubsequentLogin;
        private System.Windows.Forms.Label label5;
    }
}