namespace TradeGenie
{
    partial class CamarillaStrategy : TradeGenieForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CamarillaStrategy));
            this.label1 = new System.Windows.Forms.Label();
            this.lblRealTimeOrders = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblBOTotal = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblProfit = new System.Windows.Forms.Label();
            this.lblBOPending = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblLoss = new System.Windows.Forms.Label();
            this.lblBOCompleted = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblProfitLoss = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbCompleted = new System.Windows.Forms.RadioButton();
            this.rbPending = new System.Windows.Forms.RadioButton();
            this.rbAllOrders = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbShort = new System.Windows.Forms.RadioButton();
            this.rbLong = new System.Windows.Forms.RadioButton();
            this.rbBothPositions = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSearchBySymbol = new System.Windows.Forms.TextBox();
            this.dgvCurrentPositions = new System.Windows.Forms.DataGridView();
            this.btnMTNRefresh = new System.Windows.Forms.Button();
            this.btnStopTrading = new System.Windows.Forms.Button();
            this.btnToggleOrderPlacement = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnStartTrading = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dtPreviousTradeDate = new System.Windows.Forms.DateTimePicker();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrentPositions)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(494, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 26);
            this.label1.TabIndex = 27;
            this.label1.Text = "Camarilla Strategy";
            // 
            // lblRealTimeOrders
            // 
            this.lblRealTimeOrders.AutoSize = true;
            this.lblRealTimeOrders.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRealTimeOrders.ForeColor = System.Drawing.Color.Black;
            this.lblRealTimeOrders.Location = new System.Drawing.Point(24, 23);
            this.lblRealTimeOrders.Name = "lblRealTimeOrders";
            this.lblRealTimeOrders.Size = new System.Drawing.Size(166, 19);
            this.lblRealTimeOrders.TabIndex = 62;
            this.lblRealTimeOrders.Text = "Position / Order Status";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblBOTotal);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.lblProfit);
            this.panel1.Controls.Add(this.lblBOPending);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.lblLoss);
            this.panel1.Controls.Add(this.lblBOCompleted);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.lblProfitLoss);
            this.panel1.Location = new System.Drawing.Point(28, 61);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(339, 90);
            this.panel1.TabIndex = 63;
            // 
            // lblBOTotal
            // 
            this.lblBOTotal.AutoSize = true;
            this.lblBOTotal.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBOTotal.ForeColor = System.Drawing.Color.Teal;
            this.lblBOTotal.Location = new System.Drawing.Point(292, 64);
            this.lblBOTotal.Name = "lblBOTotal";
            this.lblBOTotal.Size = new System.Drawing.Size(33, 19);
            this.lblBOTotal.TabIndex = 51;
            this.lblBOTotal.Text = "100";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Teal;
            this.label2.Location = new System.Drawing.Point(213, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 19);
            this.label2.TabIndex = 50;
            this.label2.Text = "BO Total";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Teal;
            this.label7.Location = new System.Drawing.Point(191, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 19);
            this.label7.TabIndex = 48;
            this.label7.Text = "BO Pending";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Green;
            this.label10.Location = new System.Drawing.Point(12, 8);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 19);
            this.label10.TabIndex = 40;
            this.label10.Text = "Profit";
            // 
            // lblProfit
            // 
            this.lblProfit.AutoSize = true;
            this.lblProfit.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProfit.ForeColor = System.Drawing.Color.Green;
            this.lblProfit.Location = new System.Drawing.Point(78, 8);
            this.lblProfit.Name = "lblProfit";
            this.lblProfit.Size = new System.Drawing.Size(49, 19);
            this.lblProfit.TabIndex = 41;
            this.lblProfit.Text = "10000";
            // 
            // lblBOPending
            // 
            this.lblBOPending.AutoSize = true;
            this.lblBOPending.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBOPending.ForeColor = System.Drawing.Color.Teal;
            this.lblBOPending.Location = new System.Drawing.Point(292, 35);
            this.lblBOPending.Name = "lblBOPending";
            this.lblBOPending.Size = new System.Drawing.Size(33, 19);
            this.lblBOPending.TabIndex = 49;
            this.lblBOPending.Text = "100";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(23, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 19);
            this.label9.TabIndex = 42;
            this.label9.Text = "Loss";
            // 
            // lblLoss
            // 
            this.lblLoss.AutoSize = true;
            this.lblLoss.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoss.ForeColor = System.Drawing.Color.Red;
            this.lblLoss.Location = new System.Drawing.Point(78, 35);
            this.lblLoss.Name = "lblLoss";
            this.lblLoss.Size = new System.Drawing.Size(49, 19);
            this.lblLoss.TabIndex = 43;
            this.lblLoss.Text = "10000";
            // 
            // lblBOCompleted
            // 
            this.lblBOCompleted.AutoSize = true;
            this.lblBOCompleted.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBOCompleted.ForeColor = System.Drawing.Color.Teal;
            this.lblBOCompleted.Location = new System.Drawing.Point(292, 8);
            this.lblBOCompleted.Name = "lblBOCompleted";
            this.lblBOCompleted.Size = new System.Drawing.Size(33, 19);
            this.lblBOCompleted.TabIndex = 47;
            this.lblBOCompleted.Text = "100";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label5.Location = new System.Drawing.Point(36, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 19);
            this.label5.TabIndex = 44;
            this.label5.Text = "PL";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Teal;
            this.label6.Location = new System.Drawing.Point(173, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 19);
            this.label6.TabIndex = 46;
            this.label6.Text = "BO Completed";
            // 
            // lblProfitLoss
            // 
            this.lblProfitLoss.AutoSize = true;
            this.lblProfitLoss.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProfitLoss.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblProfitLoss.Location = new System.Drawing.Point(78, 64);
            this.lblProfitLoss.Name = "lblProfitLoss";
            this.lblProfitLoss.Size = new System.Drawing.Size(49, 19);
            this.lblProfitLoss.TabIndex = 45;
            this.lblProfitLoss.Text = "10000";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbCompleted);
            this.groupBox2.Controls.Add(this.rbPending);
            this.groupBox2.Controls.Add(this.rbAllOrders);
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(605, 61);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(218, 49);
            this.groupBox2.TabIndex = 73;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Order Status";
            // 
            // rbCompleted
            // 
            this.rbCompleted.AutoSize = true;
            this.rbCompleted.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbCompleted.Location = new System.Drawing.Point(133, 22);
            this.rbCompleted.Name = "rbCompleted";
            this.rbCompleted.Size = new System.Drawing.Size(83, 19);
            this.rbCompleted.TabIndex = 2;
            this.rbCompleted.TabStop = true;
            this.rbCompleted.Text = "Completed";
            this.rbCompleted.UseVisualStyleBackColor = true;
            // 
            // rbPending
            // 
            this.rbPending.AutoSize = true;
            this.rbPending.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPending.Location = new System.Drawing.Point(58, 23);
            this.rbPending.Name = "rbPending";
            this.rbPending.Size = new System.Drawing.Size(69, 19);
            this.rbPending.TabIndex = 1;
            this.rbPending.TabStop = true;
            this.rbPending.Text = "Pending";
            this.rbPending.UseVisualStyleBackColor = true;
            // 
            // rbAllOrders
            // 
            this.rbAllOrders.AutoSize = true;
            this.rbAllOrders.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAllOrders.Location = new System.Drawing.Point(6, 22);
            this.rbAllOrders.Name = "rbAllOrders";
            this.rbAllOrders.Size = new System.Drawing.Size(40, 19);
            this.rbAllOrders.TabIndex = 0;
            this.rbAllOrders.TabStop = true;
            this.rbAllOrders.Text = "All";
            this.rbAllOrders.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbShort);
            this.groupBox4.Controls.Add(this.rbLong);
            this.groupBox4.Controls.Add(this.rbBothPositions);
            this.groupBox4.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(840, 61);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(218, 49);
            this.groupBox4.TabIndex = 77;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Position";
            // 
            // rbShort
            // 
            this.rbShort.AutoSize = true;
            this.rbShort.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbShort.Location = new System.Drawing.Point(115, 23);
            this.rbShort.Name = "rbShort";
            this.rbShort.Size = new System.Drawing.Size(54, 19);
            this.rbShort.TabIndex = 5;
            this.rbShort.TabStop = true;
            this.rbShort.Text = "Short";
            this.rbShort.UseVisualStyleBackColor = true;
            // 
            // rbLong
            // 
            this.rbLong.AutoSize = true;
            this.rbLong.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbLong.Location = new System.Drawing.Point(59, 23);
            this.rbLong.Name = "rbLong";
            this.rbLong.Size = new System.Drawing.Size(50, 19);
            this.rbLong.TabIndex = 4;
            this.rbLong.TabStop = true;
            this.rbLong.Text = "Long";
            this.rbLong.UseVisualStyleBackColor = true;
            // 
            // rbBothPositions
            // 
            this.rbBothPositions.AutoSize = true;
            this.rbBothPositions.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbBothPositions.Location = new System.Drawing.Point(8, 22);
            this.rbBothPositions.Name = "rbBothPositions";
            this.rbBothPositions.Size = new System.Drawing.Size(50, 19);
            this.rbBothPositions.TabIndex = 3;
            this.rbBothPositions.TabStop = true;
            this.rbBothPositions.Text = "Both";
            this.rbBothPositions.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label11.Location = new System.Drawing.Point(407, 61);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(131, 19);
            this.label11.TabIndex = 75;
            this.label11.Text = "Search By Symbol";
            // 
            // txtSearchBySymbol
            // 
            this.txtSearchBySymbol.Location = new System.Drawing.Point(411, 90);
            this.txtSearchBySymbol.Name = "txtSearchBySymbol";
            this.txtSearchBySymbol.Size = new System.Drawing.Size(175, 20);
            this.txtSearchBySymbol.TabIndex = 76;
            // 
            // dgvCurrentPositions
            // 
            this.dgvCurrentPositions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCurrentPositions.Location = new System.Drawing.Point(28, 167);
            this.dgvCurrentPositions.Name = "dgvCurrentPositions";
            this.dgvCurrentPositions.Size = new System.Drawing.Size(1145, 327);
            this.dgvCurrentPositions.TabIndex = 78;
            // 
            // btnMTNRefresh
            // 
            this.btnMTNRefresh.BackColor = System.Drawing.Color.White;
            this.btnMTNRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMTNRefresh.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMTNRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnMTNRefresh.Image")));
            this.btnMTNRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMTNRefresh.Location = new System.Drawing.Point(772, 518);
            this.btnMTNRefresh.Name = "btnMTNRefresh";
            this.btnMTNRefresh.Size = new System.Drawing.Size(130, 41);
            this.btnMTNRefresh.TabIndex = 83;
            this.btnMTNRefresh.Text = "MTP Refresh";
            this.btnMTNRefresh.UseVisualStyleBackColor = false;
            // 
            // btnStopTrading
            // 
            this.btnStopTrading.BackColor = System.Drawing.Color.Silver;
            this.btnStopTrading.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStopTrading.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStopTrading.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnStopTrading.Location = new System.Drawing.Point(357, 518);
            this.btnStopTrading.Name = "btnStopTrading";
            this.btnStopTrading.Size = new System.Drawing.Size(129, 41);
            this.btnStopTrading.TabIndex = 82;
            this.btnStopTrading.Text = "Stop Trading";
            this.btnStopTrading.UseVisualStyleBackColor = false;
            // 
            // btnToggleOrderPlacement
            // 
            this.btnToggleOrderPlacement.BackColor = System.Drawing.Color.Silver;
            this.btnToggleOrderPlacement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleOrderPlacement.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToggleOrderPlacement.Location = new System.Drawing.Point(212, 517);
            this.btnToggleOrderPlacement.Name = "btnToggleOrderPlacement";
            this.btnToggleOrderPlacement.Size = new System.Drawing.Size(129, 41);
            this.btnToggleOrderPlacement.TabIndex = 81;
            this.btnToggleOrderPlacement.Text = "REAL TIME ON";
            this.btnToggleOrderPlacement.UseVisualStyleBackColor = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.Location = new System.Drawing.Point(908, 517);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(130, 41);
            this.btnRefresh.TabIndex = 80;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // btnStartTrading
            // 
            this.btnStartTrading.BackColor = System.Drawing.Color.Silver;
            this.btnStartTrading.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartTrading.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartTrading.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnStartTrading.Location = new System.Drawing.Point(61, 517);
            this.btnStartTrading.Name = "btnStartTrading";
            this.btnStartTrading.Size = new System.Drawing.Size(129, 41);
            this.btnStartTrading.TabIndex = 79;
            this.btnStartTrading.Text = "Start Trading";
            this.btnStartTrading.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(407, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 19);
            this.label3.TabIndex = 84;
            this.label3.Text = "Previous Trade Date";
            //this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // dtPreviousTradeDate
            // 
            this.dtPreviousTradeDate.Location = new System.Drawing.Point(611, 125);
            this.dtPreviousTradeDate.Name = "dtPreviousTradeDate";
            this.dtPreviousTradeDate.Size = new System.Drawing.Size(200, 20);
            this.dtPreviousTradeDate.TabIndex = 85;
            // 
            // CamarillaStrategy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1222, 571);
            this.Controls.Add(this.dtPreviousTradeDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnMTNRefresh);
            this.Controls.Add(this.btnStopTrading);
            this.Controls.Add(this.btnToggleOrderPlacement);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnStartTrading);
            this.Controls.Add(this.dgvCurrentPositions);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtSearchBySymbol);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblRealTimeOrders);
            this.Controls.Add(this.label1);
            this.Name = "CamarillaStrategy";
            this.Text = "CamarillaStrategy";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrentPositions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRealTimeOrders;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblBOTotal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblProfit;
        private System.Windows.Forms.Label lblBOPending;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblLoss;
        private System.Windows.Forms.Label lblBOCompleted;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblProfitLoss;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbCompleted;
        private System.Windows.Forms.RadioButton rbPending;
        private System.Windows.Forms.RadioButton rbAllOrders;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbShort;
        private System.Windows.Forms.RadioButton rbLong;
        private System.Windows.Forms.RadioButton rbBothPositions;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtSearchBySymbol;
        private System.Windows.Forms.DataGridView dgvCurrentPositions;
        private System.Windows.Forms.Button btnMTNRefresh;
        private System.Windows.Forms.Button btnStopTrading;
        private System.Windows.Forms.Button btnToggleOrderPlacement;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnStartTrading;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtPreviousTradeDate;
    }
}