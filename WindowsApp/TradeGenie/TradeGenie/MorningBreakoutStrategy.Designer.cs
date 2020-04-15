namespace TradeGenie
{
    partial class MorningBreakoutStrategy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MorningBreakoutStrategy));
            this.btnGetOHLCData = new System.Windows.Forms.Button();
            this.dgvOHLCData = new System.Windows.Forms.DataGridView();
            this.btnLoadOHLC = new System.Windows.Forms.Button();
            this.btnTradingScripts = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFilterBySymbol = new System.Windows.Forms.TextBox();
            this.btnStartTrading = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvMorningBreakout = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblScriptNotEntered = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblPending = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblExecuted = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblProfitLoss = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblLoss = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblProfit = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnToggleOrderPlacement = new System.Windows.Forms.Button();
            this.lblRealTimeOrders = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.lblClosedPositions = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblOpenOrders = new System.Windows.Forms.Label();
            this.lblEntriesExecuted = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblOpenEntries = new System.Windows.Forms.Label();
            this.lblExecutedOrders = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lblOpenPositions = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGetOTP = new System.Windows.Forms.Button();
            this.rbUpTrend = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbSideways = new System.Windows.Forms.RadioButton();
            this.rbDownTrend = new System.Windows.Forms.RadioButton();
            this.lblCoverOrder = new System.Windows.Forms.Button();
            this.btnSubscribeAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOHLCData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMorningBreakout)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGetOHLCData
            // 
            this.btnGetOHLCData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnGetOHLCData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetOHLCData.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetOHLCData.Location = new System.Drawing.Point(1202, 106);
            this.btnGetOHLCData.Name = "btnGetOHLCData";
            this.btnGetOHLCData.Size = new System.Drawing.Size(53, 43);
            this.btnGetOHLCData.TabIndex = 8;
            this.btnGetOHLCData.Text = "Get Current OHLC";
            this.btnGetOHLCData.UseVisualStyleBackColor = false;
            this.btnGetOHLCData.Visible = false;
            // 
            // dgvOHLCData
            // 
            this.dgvOHLCData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOHLCData.Location = new System.Drawing.Point(1202, 41);
            this.dgvOHLCData.Name = "dgvOHLCData";
            this.dgvOHLCData.Size = new System.Drawing.Size(44, 39);
            this.dgvOHLCData.TabIndex = 11;
            this.dgvOHLCData.Visible = false;
            // 
            // btnLoadOHLC
            // 
            this.btnLoadOHLC.BackColor = System.Drawing.Color.DarkSalmon;
            this.btnLoadOHLC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadOHLC.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadOHLC.Location = new System.Drawing.Point(15, 403);
            this.btnLoadOHLC.Name = "btnLoadOHLC";
            this.btnLoadOHLC.Size = new System.Drawing.Size(134, 42);
            this.btnLoadOHLC.TabIndex = 13;
            this.btnLoadOHLC.Text = "1. Load OHLC To DB";
            this.btnLoadOHLC.UseVisualStyleBackColor = false;
            this.btnLoadOHLC.Click += new System.EventHandler(this.btnLoadOHLC_Click);
            // 
            // btnTradingScripts
            // 
            this.btnTradingScripts.BackColor = System.Drawing.Color.Yellow;
            this.btnTradingScripts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTradingScripts.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTradingScripts.Location = new System.Drawing.Point(171, 404);
            this.btnTradingScripts.Name = "btnTradingScripts";
            this.btnTradingScripts.Size = new System.Drawing.Size(129, 42);
            this.btnTradingScripts.TabIndex = 14;
            this.btnTradingScripts.Text = "2. Load Morning Breakout Data";
            this.btnTradingScripts.UseVisualStyleBackColor = false;
            this.btnTradingScripts.Click += new System.EventHandler(this.btnTradingScripts_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(25, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 15);
            this.label3.TabIndex = 15;
            this.label3.Text = "Filter by Symbol";
            // 
            // txtFilterBySymbol
            // 
            this.txtFilterBySymbol.Location = new System.Drawing.Point(14, 91);
            this.txtFilterBySymbol.Name = "txtFilterBySymbol";
            this.txtFilterBySymbol.Size = new System.Drawing.Size(129, 20);
            this.txtFilterBySymbol.TabIndex = 16;
            this.txtFilterBySymbol.TextChanged += new System.EventHandler(this.txtFilterBySymbol_TextChanged);
            // 
            // btnStartTrading
            // 
            this.btnStartTrading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnStartTrading.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartTrading.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartTrading.Location = new System.Drawing.Point(324, 405);
            this.btnStartTrading.Name = "btnStartTrading";
            this.btnStartTrading.Size = new System.Drawing.Size(129, 41);
            this.btnStartTrading.TabIndex = 17;
            this.btnStartTrading.Text = "3. Start Trading";
            this.btnStartTrading.UseVisualStyleBackColor = false;
            this.btnStartTrading.Click += new System.EventHandler(this.btnStartTrading_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1199, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "OHLC Data";
            this.label4.Visible = false;
            // 
            // dgvMorningBreakout
            // 
            this.dgvMorningBreakout.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMorningBreakout.Location = new System.Drawing.Point(15, 164);
            this.dgvMorningBreakout.Name = "dgvMorningBreakout";
            this.dgvMorningBreakout.Size = new System.Drawing.Size(1096, 216);
            this.dgvMorningBreakout.TabIndex = 20;
            this.dgvMorningBreakout.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMorningBreakout_ColumnHeaderMouseClick);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.Location = new System.Drawing.Point(1010, 403);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(101, 41);
            this.btnRefresh.TabIndex = 22;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblScriptNotEntered
            // 
            this.lblScriptNotEntered.AutoSize = true;
            this.lblScriptNotEntered.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScriptNotEntered.ForeColor = System.Drawing.Color.Teal;
            this.lblScriptNotEntered.Location = new System.Drawing.Point(285, 71);
            this.lblScriptNotEntered.Name = "lblScriptNotEntered";
            this.lblScriptNotEntered.Size = new System.Drawing.Size(33, 19);
            this.lblScriptNotEntered.TabIndex = 51;
            this.lblScriptNotEntered.Text = "100";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Teal;
            this.label8.Location = new System.Drawing.Point(190, 71);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 19);
            this.label8.TabIndex = 50;
            this.label8.Text = "No Entries";
            // 
            // lblPending
            // 
            this.lblPending.AutoSize = true;
            this.lblPending.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPending.ForeColor = System.Drawing.Color.Teal;
            this.lblPending.Location = new System.Drawing.Point(285, 40);
            this.lblPending.Name = "lblPending";
            this.lblPending.Size = new System.Drawing.Size(33, 19);
            this.lblPending.TabIndex = 49;
            this.lblPending.Text = "100";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Teal;
            this.label7.Location = new System.Drawing.Point(205, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 19);
            this.label7.TabIndex = 48;
            this.label7.Text = "Pending";
            // 
            // lblExecuted
            // 
            this.lblExecuted.AutoSize = true;
            this.lblExecuted.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExecuted.ForeColor = System.Drawing.Color.Teal;
            this.lblExecuted.Location = new System.Drawing.Point(285, 13);
            this.lblExecuted.Name = "lblExecuted";
            this.lblExecuted.Size = new System.Drawing.Size(33, 19);
            this.lblExecuted.TabIndex = 47;
            this.lblExecuted.Text = "100";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Teal;
            this.label6.Location = new System.Drawing.Point(199, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 19);
            this.label6.TabIndex = 46;
            this.label6.Text = "Executed";
            // 
            // lblProfitLoss
            // 
            this.lblProfitLoss.AutoSize = true;
            this.lblProfitLoss.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProfitLoss.ForeColor = System.Drawing.Color.Green;
            this.lblProfitLoss.Location = new System.Drawing.Point(121, 71);
            this.lblProfitLoss.Name = "lblProfitLoss";
            this.lblProfitLoss.Size = new System.Drawing.Size(49, 19);
            this.lblProfitLoss.TabIndex = 45;
            this.lblProfitLoss.Text = "10000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Green;
            this.label5.Location = new System.Drawing.Point(20, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 19);
            this.label5.TabIndex = 44;
            this.label5.Text = "Profit/Loss";
            // 
            // lblLoss
            // 
            this.lblLoss.AutoSize = true;
            this.lblLoss.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoss.ForeColor = System.Drawing.Color.Red;
            this.lblLoss.Location = new System.Drawing.Point(121, 40);
            this.lblLoss.Name = "lblLoss";
            this.lblLoss.Size = new System.Drawing.Size(49, 19);
            this.lblLoss.TabIndex = 43;
            this.lblLoss.Text = "10000";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(66, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 19);
            this.label9.TabIndex = 42;
            this.label9.Text = "Loss";
            // 
            // lblProfit
            // 
            this.lblProfit.AutoSize = true;
            this.lblProfit.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProfit.ForeColor = System.Drawing.Color.Green;
            this.lblProfit.Location = new System.Drawing.Point(121, 13);
            this.lblProfit.Name = "lblProfit";
            this.lblProfit.Size = new System.Drawing.Size(49, 19);
            this.lblProfit.TabIndex = 41;
            this.lblProfit.Text = "10000";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Green;
            this.label10.Location = new System.Drawing.Point(55, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 19);
            this.label10.TabIndex = 40;
            this.label10.Text = "Profit";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.lblScriptNotEntered);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.lblProfit);
            this.panel1.Controls.Add(this.lblPending);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.lblLoss);
            this.panel1.Controls.Add(this.lblExecuted);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.lblProfitLoss);
            this.panel1.Location = new System.Drawing.Point(782, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(329, 100);
            this.panel1.TabIndex = 52;
            // 
            // btnToggleOrderPlacement
            // 
            this.btnToggleOrderPlacement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnToggleOrderPlacement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleOrderPlacement.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToggleOrderPlacement.Location = new System.Drawing.Point(483, 405);
            this.btnToggleOrderPlacement.Name = "btnToggleOrderPlacement";
            this.btnToggleOrderPlacement.Size = new System.Drawing.Size(129, 41);
            this.btnToggleOrderPlacement.TabIndex = 54;
            this.btnToggleOrderPlacement.Text = "REAL TIME ON";
            this.btnToggleOrderPlacement.UseVisualStyleBackColor = false;
            this.btnToggleOrderPlacement.Click += new System.EventHandler(this.btnToggleOrderPlacement_Click);
            // 
            // lblRealTimeOrders
            // 
            this.lblRealTimeOrders.AutoSize = true;
            this.lblRealTimeOrders.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRealTimeOrders.ForeColor = System.Drawing.Color.Black;
            this.lblRealTimeOrders.Location = new System.Drawing.Point(886, 21);
            this.lblRealTimeOrders.Name = "lblRealTimeOrders";
            this.lblRealTimeOrders.Size = new System.Drawing.Size(173, 19);
            this.lblRealTimeOrders.TabIndex = 52;
            this.lblRealTimeOrders.Text = "Simulation Order Status";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.lblClosedPositions);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.lblOpenOrders);
            this.panel2.Controls.Add(this.lblEntriesExecuted);
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.lblOpenEntries);
            this.panel2.Controls.Add(this.lblExecutedOrders);
            this.panel2.Controls.Add(this.label20);
            this.panel2.Controls.Add(this.label21);
            this.panel2.Controls.Add(this.lblOpenPositions);
            this.panel2.Location = new System.Drawing.Point(397, 51);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(362, 100);
            this.panel2.TabIndex = 55;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(8, 71);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(111, 19);
            this.label11.TabIndex = 60;
            this.label11.Text = "Open Positions";
            // 
            // lblClosedPositions
            // 
            this.lblClosedPositions.AutoSize = true;
            this.lblClosedPositions.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClosedPositions.ForeColor = System.Drawing.Color.Teal;
            this.lblClosedPositions.Location = new System.Drawing.Point(317, 71);
            this.lblClosedPositions.Name = "lblClosedPositions";
            this.lblClosedPositions.Size = new System.Drawing.Size(25, 19);
            this.lblClosedPositions.TabIndex = 63;
            this.lblClosedPositions.Text = "99";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(73, 13);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(46, 19);
            this.label13.TabIndex = 52;
            this.label13.Text = "Open";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Teal;
            this.label14.Location = new System.Drawing.Point(180, 71);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(118, 19);
            this.label14.TabIndex = 62;
            this.label14.Text = "Closed Positions";
            // 
            // lblOpenOrders
            // 
            this.lblOpenOrders.AutoSize = true;
            this.lblOpenOrders.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOpenOrders.ForeColor = System.Drawing.Color.Red;
            this.lblOpenOrders.Location = new System.Drawing.Point(129, 13);
            this.lblOpenOrders.Name = "lblOpenOrders";
            this.lblOpenOrders.Size = new System.Drawing.Size(25, 19);
            this.lblOpenOrders.TabIndex = 53;
            this.lblOpenOrders.Text = "99";
            // 
            // lblEntriesExecuted
            // 
            this.lblEntriesExecuted.AutoSize = true;
            this.lblEntriesExecuted.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntriesExecuted.ForeColor = System.Drawing.Color.Teal;
            this.lblEntriesExecuted.Location = new System.Drawing.Point(317, 39);
            this.lblEntriesExecuted.Name = "lblEntriesExecuted";
            this.lblEntriesExecuted.Size = new System.Drawing.Size(25, 19);
            this.lblEntriesExecuted.TabIndex = 61;
            this.lblEntriesExecuted.Text = "99";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Red;
            this.label17.Location = new System.Drawing.Point(22, 40);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(97, 19);
            this.label17.TabIndex = 54;
            this.label17.Text = "Open Entries";
            // 
            // lblOpenEntries
            // 
            this.lblOpenEntries.AutoSize = true;
            this.lblOpenEntries.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOpenEntries.ForeColor = System.Drawing.Color.Red;
            this.lblOpenEntries.Location = new System.Drawing.Point(129, 40);
            this.lblOpenEntries.Name = "lblOpenEntries";
            this.lblOpenEntries.Size = new System.Drawing.Size(25, 19);
            this.lblOpenEntries.TabIndex = 55;
            this.lblOpenEntries.Text = "99";
            // 
            // lblExecutedOrders
            // 
            this.lblExecutedOrders.AutoSize = true;
            this.lblExecutedOrders.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExecutedOrders.ForeColor = System.Drawing.Color.Teal;
            this.lblExecutedOrders.Location = new System.Drawing.Point(317, 13);
            this.lblExecutedOrders.Name = "lblExecutedOrders";
            this.lblExecutedOrders.Size = new System.Drawing.Size(25, 19);
            this.lblExecutedOrders.TabIndex = 59;
            this.lblExecutedOrders.Text = "99";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Teal;
            this.label20.Location = new System.Drawing.Point(180, 41);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(126, 19);
            this.label20.TabIndex = 56;
            this.label20.Text = "Entries Executed ";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.Teal;
            this.label21.Location = new System.Drawing.Point(230, 13);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(71, 19);
            this.label21.TabIndex = 58;
            this.label21.Text = "Executed";
            // 
            // lblOpenPositions
            // 
            this.lblOpenPositions.AutoSize = true;
            this.lblOpenPositions.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOpenPositions.ForeColor = System.Drawing.Color.Red;
            this.lblOpenPositions.Location = new System.Drawing.Point(129, 71);
            this.lblOpenPositions.Name = "lblOpenPositions";
            this.lblOpenPositions.Size = new System.Drawing.Size(25, 19);
            this.lblOpenPositions.TabIndex = 57;
            this.lblOpenPositions.Text = "99";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(245, 26);
            this.label2.TabIndex = 18;
            this.label2.Text = "Morning Breakout Strategy";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(507, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 19);
            this.label1.TabIndex = 56;
            this.label1.Text = "Real Time Order Status";
            // 
            // btnGetOTP
            // 
            this.btnGetOTP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnGetOTP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetOTP.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetOTP.Location = new System.Drawing.Point(641, 404);
            this.btnGetOTP.Name = "btnGetOTP";
            this.btnGetOTP.Size = new System.Drawing.Size(129, 41);
            this.btnGetOTP.TabIndex = 57;
            this.btnGetOTP.Text = "Get Orders, Trades and Positions";
            this.btnGetOTP.UseVisualStyleBackColor = false;
            this.btnGetOTP.Click += new System.EventHandler(this.btnGetOTP_Click);
            // 
            // rbUpTrend
            // 
            this.rbUpTrend.AutoSize = true;
            this.rbUpTrend.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbUpTrend.Location = new System.Drawing.Point(17, 22);
            this.rbUpTrend.Name = "rbUpTrend";
            this.rbUpTrend.Size = new System.Drawing.Size(73, 19);
            this.rbUpTrend.TabIndex = 59;
            this.rbUpTrend.TabStop = true;
            this.rbUpTrend.Text = "Up Trend";
            this.rbUpTrend.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbSideways);
            this.groupBox1.Controls.Add(this.rbDownTrend);
            this.groupBox1.Controls.Add(this.rbUpTrend);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(224, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(131, 100);
            this.groupBox1.TabIndex = 60;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nifty Trend ";
            // 
            // rbSideways
            // 
            this.rbSideways.AutoSize = true;
            this.rbSideways.Checked = true;
            this.rbSideways.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSideways.Location = new System.Drawing.Point(17, 70);
            this.rbSideways.Name = "rbSideways";
            this.rbSideways.Size = new System.Drawing.Size(76, 19);
            this.rbSideways.TabIndex = 61;
            this.rbSideways.TabStop = true;
            this.rbSideways.Text = "Sideways";
            this.rbSideways.UseVisualStyleBackColor = true;
            // 
            // rbDownTrend
            // 
            this.rbDownTrend.AutoSize = true;
            this.rbDownTrend.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDownTrend.Location = new System.Drawing.Point(17, 45);
            this.rbDownTrend.Name = "rbDownTrend";
            this.rbDownTrend.Size = new System.Drawing.Size(89, 19);
            this.rbDownTrend.TabIndex = 60;
            this.rbDownTrend.TabStop = true;
            this.rbDownTrend.Text = "Down Trend";
            this.rbDownTrend.UseVisualStyleBackColor = true;
            // 
            // lblCoverOrder
            // 
            this.lblCoverOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblCoverOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCoverOrder.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCoverOrder.Location = new System.Drawing.Point(891, 403);
            this.lblCoverOrder.Name = "lblCoverOrder";
            this.lblCoverOrder.Size = new System.Drawing.Size(94, 41);
            this.lblCoverOrder.TabIndex = 61;
            this.lblCoverOrder.Text = "Cover Order Check";
            this.lblCoverOrder.UseVisualStyleBackColor = false;
            this.lblCoverOrder.Click += new System.EventHandler(this.lblCoverOrder_Click);
            // 
            // btnSubscribeAll
            // 
            this.btnSubscribeAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnSubscribeAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubscribeAll.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubscribeAll.Location = new System.Drawing.Point(791, 403);
            this.btnSubscribeAll.Name = "btnSubscribeAll";
            this.btnSubscribeAll.Size = new System.Drawing.Size(94, 41);
            this.btnSubscribeAll.TabIndex = 62;
            this.btnSubscribeAll.Text = "Subscribe All Scripts";
            this.btnSubscribeAll.UseVisualStyleBackColor = false;
            this.btnSubscribeAll.Click += new System.EventHandler(this.btnSubscribeAll_Click);
            // 
            // MorningBreakoutStrategy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 456);
            this.Controls.Add(this.btnSubscribeAll);
            this.Controls.Add(this.lblCoverOrder);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnGetOTP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblRealTimeOrders);
            this.Controls.Add(this.btnToggleOrderPlacement);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgvMorningBreakout);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnStartTrading);
            this.Controls.Add(this.txtFilterBySymbol);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnTradingScripts);
            this.Controls.Add(this.btnLoadOHLC);
            this.Controls.Add(this.dgvOHLCData);
            this.Controls.Add(this.btnGetOHLCData);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "MorningBreakoutStrategy";
            this.Text = "Morning Breakout";
            ((System.ComponentModel.ISupportInitialize)(this.dgvOHLCData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMorningBreakout)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetOHLCData;
        private System.Windows.Forms.DataGridView dgvOHLCData;
        private System.Windows.Forms.Button btnLoadOHLC;
        private System.Windows.Forms.Button btnTradingScripts;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFilterBySymbol;
        private System.Windows.Forms.Button btnStartTrading;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvMorningBreakout;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblScriptNotEntered;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblPending;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblExecuted;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblProfitLoss;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblLoss;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblProfit;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnToggleOrderPlacement;
        private System.Windows.Forms.Label lblRealTimeOrders;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblClosedPositions;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblOpenOrders;
        private System.Windows.Forms.Label lblEntriesExecuted;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblOpenEntries;
        private System.Windows.Forms.Label lblExecutedOrders;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lblOpenPositions;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGetOTP;
        private System.Windows.Forms.RadioButton rbUpTrend;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbSideways;
        private System.Windows.Forms.RadioButton rbDownTrend;
        private System.Windows.Forms.Button lblCoverOrder;
        private System.Windows.Forms.Button btnSubscribeAll;
    }
}