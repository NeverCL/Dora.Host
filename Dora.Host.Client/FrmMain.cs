using Dora.Agent.Handle;
using Dora.Host.Client.RemoteControl;
using Dora.Host.Core.Client;
using Dora.Host.Core.Message;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dora.Host.Client
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private async void BtnSnap_Click(object sender, EventArgs e)
        {
            await SnapControl.Instance.SendAsync(_ => Console.WriteLine(_.Id));
        }

        private async void FrmMain_Load(object sender, EventArgs e)
        {
            var context = new Context();
            context.FromChannel = context.Id + "";
            await ClientMessenger.Instance.SubscribeAsync(context.FromChannel, c => {
                var agents = c.Data as IEnumerable<string>;
                if (agents == null)
                    return;
                foreach (var agent in agents)
                    this.cmbChannel.Items.Add(agent);
            });
            await ClientMessenger.Instance.PublishAsync(context);
        }
    }
}
