using KiteConnect;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradeGenie
{
    public partial class TickerForm : TradeGenieForm
    {
        public TickerForm()
        {
            InitializeComponent();
        }

        protected void onTick(Tick TickData)
        {
            switch (TickData.InstrumentToken)
            {
                //lblNiftyValue.Text = Nifty50Value;
                //lblNiftyJuniorValue.Text = NiftyJuniorValue;
                //lblNiftyMidcapValue.Text = NiftyMidcapValue;

                case 256265: //Nifty 50
                    SetLabel(lblNiftyValue, TickData.LastPrice.ToString());
                    SetLabel(lblNiftyChange, (TickData.LastPrice - TickData.Close).ToString());
                    break;
                case 260105: //Nifty Bank
                    SetLabel(lblNiftyBankValue, TickData.LastPrice.ToString());
                    SetLabel(lblNiftyBankChange, (TickData.LastPrice - TickData.Close).ToString());
                    break;
                case 260873: //Nifty Midcap
                    SetLabel(lblNiftyMidcapValue, TickData.LastPrice.ToString());
                    SetLabel(lblNiftyMidcapChange, (TickData.LastPrice - TickData.Close).ToString());
                    break;
                default:
                    //Logger.GenericLog(TickData.InstrumentToken + ":" + TickData.LastPrice);
                    break;
            }
        }


        private void SetLabel(Label objLabel, string val)
        {
            MethodInvoker inv = delegate
            {
                objLabel.Text = val;
            };

            this.Invoke(inv);
        }

        private void ColorChange(Label objLabel)
        {
            if (Decimal.Parse(objLabel.Text) > 0)
            {
                objLabel.ForeColor = Color.Green;
            }
            else
            {
                objLabel.ForeColor = Color.Red;
            }
        }

        private void lblNiftyChange_TextChanged(object sender, EventArgs e)
        {
            ColorChange(this.lblNiftyChange);
        }

        private void lblNiftyBankChange_TextChanged(object sender, EventArgs e)
        {
            ColorChange(this.lblNiftyBankChange);
        }

        private void lblNiftyMidcapChange_TextChanged(object sender, EventArgs e)
        {
            ColorChange(this.lblNiftyMidcapChange);
        }

        private void TickerForm_Load(object sender, EventArgs e)
        {
            //ticker.OnTick += this.onTick;

            //List<string> insListForSubscription = dbMethods.GetAllInstrumentTokenForSubscription();

            List<uint> insListForSubscription = new List<uint>();

            insListForSubscription.Add(256265); //Nifty 50
            insListForSubscription.Add(260105); //Nifty Bank
            insListForSubscription.Add(260873); //Nifty Midcap

            uint[] arInstrumentList = insListForSubscription.ToArray();

            ticker.Subscribe(Tokens: arInstrumentList );
            ticker.SetMode(Tokens: arInstrumentList, Mode: "quote");
        }

        private void TickerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ticker.Close();
        }
    }
}
