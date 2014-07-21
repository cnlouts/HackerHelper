using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HackerHelper.usercontrols;

namespace HackerHelper
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var name = this.treeView1.SelectedNode.Name;
            switch (name)
            {
                case "node1":
                    splitContainer1.Panel2.Controls.Clear();
                    this.splitContainer1.Panel2.Controls.Add(new WebDirForce());
                    break;
                case "node3":
                    splitContainer1.Panel2.Controls.Clear();
                    this.splitContainer1.Panel2.Controls.Add(new googleUrlCapture());
                    break;
            }
        }

    }
}
