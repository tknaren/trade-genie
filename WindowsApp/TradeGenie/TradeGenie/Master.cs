using KiteConnect;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradeGenie
{
    public partial class Master : TradeGenieForm
    {
        private int childFormNumber = 0;
        Thread monitorOTMP;
        private Object thisLock = new Object();
        private static bool continueMonitoring = true;

        public Master()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void Master_Load(object sender, EventArgs e)
        {
            LoginToKite();

            toolStripStatusLabel.Text = StatusMessage;

            //TickerForm ticker = new TickerForm();
            //ticker.MdiParent = this;
            //ticker.Dock = DockStyle.Top;
            //ticker.Show();

        }

        private void toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void statusStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripStatusLabel_Click(object sender, EventArgs e)
        {

        }

        private void btnLoadNSEInstruments_Click(object sender, EventArgs e)
        {

        }

        private void btnLoadNFOInstruments_Click(object sender, EventArgs e)
        {

        }

        private void btnSubscribeNifty_Click(object sender, EventArgs e)
        {

        }





        private void lblNiftyValue_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void menuItemNSEInstruments_Click(object sender, EventArgs e)
        {
            List<Instrument> instruments = kite.GetInstruments("NSE");

            dbMethods.LoadInstruments(instruments);
        }

        private void menuItemNFOInstruments_Click(object sender, EventArgs e)
        {
            List<Instrument> instruments = kite.GetInstruments("NFO");

            dbMethods.LoadInstruments(instruments);
        }

        private void showOHLCFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MorningBreakoutStrategy ohlc = new MorningBreakoutStrategy();
            ohlc.MdiParent = this;
            ohlc.Dock = DockStyle.Bottom;
            ohlc.Show();
        }

        private void showNiftyTickerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TickerForm ticker = new TickerForm();
            ticker.MdiParent = this;
            ticker.Show();
        }

        private void loadOHLCHistoryDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ElderBusinessLogic elderBusiness = new ElderBusinessLogic();

            string[] timeInterval = UserConfiguration.IntervalInMin.Split(',');

            for (int i = 0; i < timeInterval.Length; i++)
            {
                try
                {
                    Logger.GenericLog("Before " + timeInterval[i].ToString() + " min Download Data");

                    elderBusiness.DownloadAndLoadData(Convert.ToInt32(timeInterval[i]));

                    Logger.GenericLog("After " + timeInterval[i].ToString() + " min Download Data");

                    elderBusiness.MainBusinessLogic(Convert.ToInt32(timeInterval[i]));

                    Logger.GenericLog("After " + timeInterval[i].ToString() + " min Elder Calculation Data");
                }
                catch (Exception ex)
                {
                    Logger.ErrorLogToFile(ex);
                }
            }

        }

        private void elderImpulseStrategyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ElderImpulseStrategy ohlc = new ElderImpulseStrategy();
            ohlc.MdiParent = this;
            ohlc.Dock = DockStyle.Fill;
            ohlc.Show();
        }

        private void monitorOrdersTradesMarginsPositionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            continueMonitoring = true;
            monitorOTMP = new Thread(new ThreadStart(MonitorOTMP));
            monitorOTMP.Start();
            MessageBox.Show("Monitoring Started. ");
            //MonitorOTMP();
        }

        private void MonitorOTMP()
        {
            //1. GetOrder
            //2. GetTrades
            //3. GetMargin
            //4. GetPositions

            // Write logic to stop the thread as well

            while (continueMonitoring)
            {
                try
                {
                    List<KiteConnect.Order> orders = kite.GetOrders();

                    lock (thisLock)
                    {
                        dbMethods.LoadOrders(orders);
                    }

                    //Logger.LogToFile("Orders loaded");

                    PositionResponse pr = kite.GetPositions();
                    dbMethods.LoadPositions(pr.Net);

                    //Logger.LogToFile("Positions loaded");

                    //KiteConnect.Margin margin = kite.Margins("equity");
                    //dbMethods.LoadMargins(margin);

                    //Logger.LogToFile("Margins loaded");

                    List<KiteConnect.Trade> trades = kite.GetOrderTrades();

                    lock (thisLock)
                    {
                        dbMethods.LoadTrades(trades);
                    }
                    

                    //Logger.LogToFile("Trades loaded");
                }
                catch (Exception ex)
                {
                    Logger.ErrorLogToFile(ex);
                }

                Thread.Sleep(UserConfiguration.MorningOTMPInterval);
            }
        }

        private void stopMonitoringOTMPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            continueMonitoring = false;

            MessageBox.Show("Request is placed to stop monitoring.");
        }

        private void startLoadingLTPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<uint> instrumentList = dbMethods.GetAllInstrumentTokenForSubscription();
            instrumentList.ToArray();

        }

        
    }
}
