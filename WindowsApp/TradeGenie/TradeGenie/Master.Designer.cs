namespace TradeGenie
{
    partial class Master : TradeGenieForm
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuItemActions = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemNSEInstruments = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemNFOInstruments = new System.Windows.Forms.ToolStripMenuItem();
            this.showOHLCFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.elderImpulseStrategyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadOHLCHistoryDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showNiftyTickerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monitorOrdersTradesMarginsPositionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopMonitoringOTMPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startLoadingLTPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopLoadingLTPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemActions});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(1072, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            this.menuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip_ItemClicked);
            // 
            // menuItemActions
            // 
            this.menuItemActions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemNSEInstruments,
            this.menuItemNFOInstruments,
            this.showOHLCFormToolStripMenuItem,
            this.elderImpulseStrategyToolStripMenuItem,
            this.loadOHLCHistoryDataToolStripMenuItem,
            this.showNiftyTickerToolStripMenuItem,
            this.monitorOrdersTradesMarginsPositionsToolStripMenuItem,
            this.stopMonitoringOTMPToolStripMenuItem,
            this.startLoadingLTPToolStripMenuItem,
            this.stopLoadingLTPToolStripMenuItem});
            this.menuItemActions.Name = "menuItemActions";
            this.menuItemActions.Size = new System.Drawing.Size(59, 20);
            this.menuItemActions.Text = "Actions";
            // 
            // menuItemNSEInstruments
            // 
            this.menuItemNSEInstruments.Name = "menuItemNSEInstruments";
            this.menuItemNSEInstruments.Size = new System.Drawing.Size(216, 22);
            this.menuItemNSEInstruments.Text = "Load NSE Instruments";
            this.menuItemNSEInstruments.Click += new System.EventHandler(this.menuItemNSEInstruments_Click);
            // 
            // menuItemNFOInstruments
            // 
            this.menuItemNFOInstruments.Enabled = false;
            this.menuItemNFOInstruments.Name = "menuItemNFOInstruments";
            this.menuItemNFOInstruments.Size = new System.Drawing.Size(216, 22);
            this.menuItemNFOInstruments.Text = "Load NFO Instruments";
            this.menuItemNFOInstruments.Click += new System.EventHandler(this.menuItemNFOInstruments_Click);
            // 
            // showOHLCFormToolStripMenuItem
            // 
            this.showOHLCFormToolStripMenuItem.Name = "showOHLCFormToolStripMenuItem";
            this.showOHLCFormToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.showOHLCFormToolStripMenuItem.Text = "Morning Breakout Strategy";
            this.showOHLCFormToolStripMenuItem.Click += new System.EventHandler(this.showOHLCFormToolStripMenuItem_Click);
            // 
            // elderImpulseStrategyToolStripMenuItem
            // 
            this.elderImpulseStrategyToolStripMenuItem.Name = "elderImpulseStrategyToolStripMenuItem";
            this.elderImpulseStrategyToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.elderImpulseStrategyToolStripMenuItem.Text = "Elder Impulse Strategy";
            this.elderImpulseStrategyToolStripMenuItem.Click += new System.EventHandler(this.elderImpulseStrategyToolStripMenuItem_Click);
            // 
            // loadOHLCHistoryDataToolStripMenuItem
            // 
            this.loadOHLCHistoryDataToolStripMenuItem.Enabled = false;
            this.loadOHLCHistoryDataToolStripMenuItem.Name = "loadOHLCHistoryDataToolStripMenuItem";
            this.loadOHLCHistoryDataToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.loadOHLCHistoryDataToolStripMenuItem.Text = "Load OHLC History Data";
            this.loadOHLCHistoryDataToolStripMenuItem.Click += new System.EventHandler(this.loadOHLCHistoryDataToolStripMenuItem_Click);
            // 
            // showNiftyTickerToolStripMenuItem
            // 
            this.showNiftyTickerToolStripMenuItem.Enabled = false;
            this.showNiftyTickerToolStripMenuItem.Name = "showNiftyTickerToolStripMenuItem";
            this.showNiftyTickerToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.showNiftyTickerToolStripMenuItem.Text = "Show Nifty Ticker";
            this.showNiftyTickerToolStripMenuItem.Click += new System.EventHandler(this.showNiftyTickerToolStripMenuItem_Click);
            // 
            // monitorOrdersTradesMarginsPositionsToolStripMenuItem
            // 
            this.monitorOrdersTradesMarginsPositionsToolStripMenuItem.Name = "monitorOrdersTradesMarginsPositionsToolStripMenuItem";
            this.monitorOrdersTradesMarginsPositionsToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.monitorOrdersTradesMarginsPositionsToolStripMenuItem.Text = "Get OTMP";
            this.monitorOrdersTradesMarginsPositionsToolStripMenuItem.Click += new System.EventHandler(this.monitorOrdersTradesMarginsPositionsToolStripMenuItem_Click);
            // 
            // stopMonitoringOTMPToolStripMenuItem
            // 
            this.stopMonitoringOTMPToolStripMenuItem.Name = "stopMonitoringOTMPToolStripMenuItem";
            this.stopMonitoringOTMPToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.stopMonitoringOTMPToolStripMenuItem.Text = "Stop Monitoring OTMP";
            this.stopMonitoringOTMPToolStripMenuItem.Click += new System.EventHandler(this.stopMonitoringOTMPToolStripMenuItem_Click);
            // 
            // startLoadingLTPToolStripMenuItem
            // 
            this.startLoadingLTPToolStripMenuItem.Name = "startLoadingLTPToolStripMenuItem";
            this.startLoadingLTPToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.startLoadingLTPToolStripMenuItem.Text = "Start Loading LTP";
            this.startLoadingLTPToolStripMenuItem.Click += new System.EventHandler(this.startLoadingLTPToolStripMenuItem_Click);
            // 
            // stopLoadingLTPToolStripMenuItem
            // 
            this.stopLoadingLTPToolStripMenuItem.Name = "stopLoadingLTPToolStripMenuItem";
            this.stopLoadingLTPToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.stopLoadingLTPToolStripMenuItem.Text = "Stop Loading LTP";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 613);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip.Size = new System.Drawing.Size(1072, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            this.statusStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip_ItemClicked);
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel.Text = "Status";
            this.toolStripStatusLabel.Click += new System.EventHandler(this.toolStripStatusLabel_Click);
            // 
            // Master
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 635);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Master";
            this.Text = "Trade Genie Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Master_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem menuItemActions;
        private System.Windows.Forms.ToolStripMenuItem menuItemNSEInstruments;
        private System.Windows.Forms.ToolStripMenuItem menuItemNFOInstruments;
        private System.Windows.Forms.ToolStripMenuItem showOHLCFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showNiftyTickerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadOHLCHistoryDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem elderImpulseStrategyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem monitorOrdersTradesMarginsPositionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopMonitoringOTMPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startLoadingLTPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopLoadingLTPToolStripMenuItem;
    }
}



