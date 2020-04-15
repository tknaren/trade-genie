namespace TradeGenie
{
    partial class ElderImpulseStrategy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ElderImpulseStrategy));
            this.btnStartTrading = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvCurrentPositions = new System.Windows.Forms.DataGridView();
            this.btnToggleOrderPlacement = new System.Windows.Forms.Button();
            this.lblRealTimeOrders = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCOTotal = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblCOPending = new System.Windows.Forms.Label();
            this.lblCOCompleted = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkBracketOrder = new System.Windows.Forms.CheckBox();
            this.chkCoverOrder = new System.Windows.Forms.CheckBox();
            this.btnStopTrading = new System.Windows.Forms.Button();
            this.btnMTNRefresh = new System.Windows.Forms.Button();
            this.txtSearchBySymbol = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbCompleted = new System.Windows.Forms.RadioButton();
            this.rbPending = new System.Windows.Forms.RadioButton();
            this.rbAllOrders = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbBracketOrder = new System.Windows.Forms.RadioButton();
            this.rbCoverOrder = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbShort = new System.Windows.Forms.RadioButton();
            this.rbLong = new System.Windows.Forms.RadioButton();
            this.rbBothPositions = new System.Windows.Forms.RadioButton();
            this.rbBothOrders = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrentPositions)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStartTrading
            // 
            this.btnStartTrading.BackColor = System.Drawing.Color.Silver;
            this.btnStartTrading.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartTrading.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartTrading.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnStartTrading.Location = new System.Drawing.Point(54, 501);
            this.btnStartTrading.Name = "btnStartTrading";
            this.btnStartTrading.Size = new System.Drawing.Size(129, 41);
            this.btnStartTrading.TabIndex = 22;
            this.btnStartTrading.Text = "Start Trading";
            this.btnStartTrading.UseVisualStyleBackColor = false;
            this.btnStartTrading.Click += new System.EventHandler(this.btnStartTrading_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(523, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 26);
            this.label1.TabIndex = 26;
            this.label1.Text = "Elder Strategy";
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.Location = new System.Drawing.Point(901, 501);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(130, 41);
            this.btnRefresh.TabIndex = 27;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvCurrentPositions
            // 
            this.dgvCurrentPositions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCurrentPositions.Location = new System.Drawing.Point(12, 176);
            this.dgvCurrentPositions.Name = "dgvCurrentPositions";
            this.dgvCurrentPositions.Size = new System.Drawing.Size(1145, 309);
            this.dgvCurrentPositions.TabIndex = 28;
            this.dgvCurrentPositions.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvCurrentPositions_ColumnHeaderMouseClick);
            // 
            // btnToggleOrderPlacement
            // 
            this.btnToggleOrderPlacement.BackColor = System.Drawing.Color.Silver;
            this.btnToggleOrderPlacement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleOrderPlacement.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToggleOrderPlacement.Location = new System.Drawing.Point(205, 501);
            this.btnToggleOrderPlacement.Name = "btnToggleOrderPlacement";
            this.btnToggleOrderPlacement.Size = new System.Drawing.Size(129, 41);
            this.btnToggleOrderPlacement.TabIndex = 59;
            this.btnToggleOrderPlacement.Text = "REAL TIME ON";
            this.btnToggleOrderPlacement.UseVisualStyleBackColor = false;
            this.btnToggleOrderPlacement.Click += new System.EventHandler(this.btnToggleOrderPlacement_Click);
            // 
            // lblRealTimeOrders
            // 
            this.lblRealTimeOrders.AutoSize = true;
            this.lblRealTimeOrders.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRealTimeOrders.ForeColor = System.Drawing.Color.Black;
            this.lblRealTimeOrders.Location = new System.Drawing.Point(17, 21);
            this.lblRealTimeOrders.Name = "lblRealTimeOrders";
            this.lblRealTimeOrders.Size = new System.Drawing.Size(166, 19);
            this.lblRealTimeOrders.TabIndex = 61;
            this.lblRealTimeOrders.Text = "Position / Order Status";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblCOTotal);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.lblCOPending);
            this.panel1.Controls.Add(this.lblCOCompleted);
            this.panel1.Controls.Add(this.label13);
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
            this.panel1.Location = new System.Drawing.Point(12, 62);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(534, 98);
            this.panel1.TabIndex = 62;
            // 
            // lblCOTotal
            // 
            this.lblCOTotal.AutoSize = true;
            this.lblCOTotal.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCOTotal.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblCOTotal.Location = new System.Drawing.Point(470, 64);
            this.lblCOTotal.Name = "lblCOTotal";
            this.lblCOTotal.Size = new System.Drawing.Size(33, 19);
            this.lblCOTotal.TabIndex = 57;
            this.lblCOTotal.Text = "100";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(398, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 19);
            this.label4.TabIndex = 56;
            this.label4.Text = "CO Total";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label8.Location = new System.Drawing.Point(376, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 19);
            this.label8.TabIndex = 54;
            this.label8.Text = "CO Pending";
            // 
            // lblCOPending
            // 
            this.lblCOPending.AutoSize = true;
            this.lblCOPending.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCOPending.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblCOPending.Location = new System.Drawing.Point(470, 35);
            this.lblCOPending.Name = "lblCOPending";
            this.lblCOPending.Size = new System.Drawing.Size(33, 19);
            this.lblCOPending.TabIndex = 55;
            this.lblCOPending.Text = "100";
            // 
            // lblCOCompleted
            // 
            this.lblCOCompleted.AutoSize = true;
            this.lblCOCompleted.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCOCompleted.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblCOCompleted.Location = new System.Drawing.Point(470, 8);
            this.lblCOCompleted.Name = "lblCOCompleted";
            this.lblCOCompleted.Size = new System.Drawing.Size(33, 19);
            this.lblCOCompleted.TabIndex = 53;
            this.lblCOCompleted.Text = "100";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label13.Location = new System.Drawing.Point(358, 8);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(106, 19);
            this.label13.TabIndex = 52;
            this.label13.Text = "CO Completed";
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkBracketOrder);
            this.groupBox1.Controls.Add(this.chkCoverOrder);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(999, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(158, 104);
            this.groupBox1.TabIndex = 67;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Place Orders";
            // 
            // chkBracketOrder
            // 
            this.chkBracketOrder.AutoSize = true;
            this.chkBracketOrder.ForeColor = System.Drawing.Color.Green;
            this.chkBracketOrder.Location = new System.Drawing.Point(21, 66);
            this.chkBracketOrder.Name = "chkBracketOrder";
            this.chkBracketOrder.Size = new System.Drawing.Size(124, 23);
            this.chkBracketOrder.TabIndex = 1;
            this.chkBracketOrder.Text = "Bracket Order";
            this.chkBracketOrder.UseVisualStyleBackColor = true;
            // 
            // chkCoverOrder
            // 
            this.chkCoverOrder.AutoSize = true;
            this.chkCoverOrder.ForeColor = System.Drawing.Color.Green;
            this.chkCoverOrder.Location = new System.Drawing.Point(21, 37);
            this.chkCoverOrder.Name = "chkCoverOrder";
            this.chkCoverOrder.Size = new System.Drawing.Size(111, 23);
            this.chkCoverOrder.TabIndex = 0;
            this.chkCoverOrder.Text = "Cover Order";
            this.chkCoverOrder.UseVisualStyleBackColor = true;
            // 
            // btnStopTrading
            // 
            this.btnStopTrading.BackColor = System.Drawing.Color.Silver;
            this.btnStopTrading.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStopTrading.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStopTrading.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnStopTrading.Location = new System.Drawing.Point(350, 502);
            this.btnStopTrading.Name = "btnStopTrading";
            this.btnStopTrading.Size = new System.Drawing.Size(129, 41);
            this.btnStopTrading.TabIndex = 68;
            this.btnStopTrading.Text = "Stop Trading";
            this.btnStopTrading.UseVisualStyleBackColor = false;
            this.btnStopTrading.Click += new System.EventHandler(this.btnStopTrading_Click);
            // 
            // btnMTNRefresh
            // 
            this.btnMTNRefresh.BackColor = System.Drawing.Color.White;
            this.btnMTNRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMTNRefresh.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMTNRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnMTNRefresh.Image")));
            this.btnMTNRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMTNRefresh.Location = new System.Drawing.Point(765, 502);
            this.btnMTNRefresh.Name = "btnMTNRefresh";
            this.btnMTNRefresh.Size = new System.Drawing.Size(130, 41);
            this.btnMTNRefresh.TabIndex = 69;
            this.btnMTNRefresh.Text = "MTP Refresh";
            this.btnMTNRefresh.UseVisualStyleBackColor = false;
            this.btnMTNRefresh.Click += new System.EventHandler(this.btnMTNRefresh_Click);
            // 
            // txtSearchBySymbol
            // 
            this.txtSearchBySymbol.Location = new System.Drawing.Point(810, 78);
            this.txtSearchBySymbol.Name = "txtSearchBySymbol";
            this.txtSearchBySymbol.Size = new System.Drawing.Size(175, 20);
            this.txtSearchBySymbol.TabIndex = 71;
            this.txtSearchBySymbol.TextChanged += new System.EventHandler(this.txtSearchBySymbol_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label11.Location = new System.Drawing.Point(806, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(131, 19);
            this.label11.TabIndex = 58;
            this.label11.Text = "Search By Symbol";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbCompleted);
            this.groupBox2.Controls.Add(this.rbPending);
            this.groupBox2.Controls.Add(this.rbAllOrders);
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(561, 56);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(218, 49);
            this.groupBox2.TabIndex = 72;
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
            this.rbAllOrders.CheckedChanged += new System.EventHandler(this.rbAllOrders_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbBothOrders);
            this.groupBox3.Controls.Add(this.rbBracketOrder);
            this.groupBox3.Controls.Add(this.rbCoverOrder);
            this.groupBox3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(561, 111);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(218, 49);
            this.groupBox3.TabIndex = 73;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Order Variety";
            // 
            // rbBracketOrder
            // 
            this.rbBracketOrder.AutoSize = true;
            this.rbBracketOrder.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbBracketOrder.Location = new System.Drawing.Point(146, 22);
            this.rbBracketOrder.Name = "rbBracketOrder";
            this.rbBracketOrder.Size = new System.Drawing.Size(66, 19);
            this.rbBracketOrder.TabIndex = 2;
            this.rbBracketOrder.TabStop = true;
            this.rbBracketOrder.Text = "Bracket";
            this.rbBracketOrder.UseVisualStyleBackColor = true;
            // 
            // rbCoverOrder
            // 
            this.rbCoverOrder.AutoSize = true;
            this.rbCoverOrder.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbCoverOrder.Location = new System.Drawing.Point(71, 22);
            this.rbCoverOrder.Name = "rbCoverOrder";
            this.rbCoverOrder.Size = new System.Drawing.Size(56, 19);
            this.rbCoverOrder.TabIndex = 1;
            this.rbCoverOrder.TabStop = true;
            this.rbCoverOrder.Text = "Cover";
            this.rbCoverOrder.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbShort);
            this.groupBox4.Controls.Add(this.rbLong);
            this.groupBox4.Controls.Add(this.rbBothPositions);
            this.groupBox4.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(810, 111);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(175, 49);
            this.groupBox4.TabIndex = 74;
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
            // rbBothOrders
            // 
            this.rbBothOrders.AutoSize = true;
            this.rbBothOrders.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbBothOrders.Location = new System.Drawing.Point(6, 22);
            this.rbBothOrders.Name = "rbBothOrders";
            this.rbBothOrders.Size = new System.Drawing.Size(50, 19);
            this.rbBothOrders.TabIndex = 6;
            this.rbBothOrders.TabStop = true;
            this.rbBothOrders.Text = "Both";
            this.rbBothOrders.UseVisualStyleBackColor = true;
            // 
            // ElderImpulseStrategy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 554);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtSearchBySymbol);
            this.Controls.Add(this.btnMTNRefresh);
            this.Controls.Add(this.btnStopTrading);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblRealTimeOrders);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnToggleOrderPlacement);
            this.Controls.Add(this.dgvCurrentPositions);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStartTrading);
            this.Name = "ElderImpulseStrategy";
            this.Text = " ";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrentPositions)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnStartTrading;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvCurrentPositions;
        private System.Windows.Forms.Button btnToggleOrderPlacement;
        private System.Windows.Forms.Label lblRealTimeOrders;
        private System.Windows.Forms.Panel panel1;
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
        private System.Windows.Forms.Label lblBOTotal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkBracketOrder;
        private System.Windows.Forms.CheckBox chkCoverOrder;
        private System.Windows.Forms.Button btnStopTrading;
        private System.Windows.Forms.Button btnMTNRefresh;
        private System.Windows.Forms.Label lblCOTotal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblCOPending;
        private System.Windows.Forms.Label lblCOCompleted;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtSearchBySymbol;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbCompleted;
        private System.Windows.Forms.RadioButton rbPending;
        private System.Windows.Forms.RadioButton rbAllOrders;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbBracketOrder;
        private System.Windows.Forms.RadioButton rbCoverOrder;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbShort;
        private System.Windows.Forms.RadioButton rbLong;
        private System.Windows.Forms.RadioButton rbBothPositions;
        private System.Windows.Forms.RadioButton rbBothOrders;
    }
}