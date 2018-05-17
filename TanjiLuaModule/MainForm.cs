using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MoonSharp.Interpreter;
using Sulakore;
using Sulakore.Communication;
using Sulakore.Modules;
using Tangine;
using TanjiLuaModule.Engine;

namespace TanjiLuaModule
{
    [Module("TanjiLua", "Lua inplementation for TANJI")]
    [Author("Karamelon")]
    public partial class MainForm : ExtensionForm
    {
        private ScriptManager scriptManager;

        public MainForm()
        {
            scriptManager = new ScriptManager(this);
            InitializeComponent();
            
        }

        internal ScriptManager ScriptManager { get => scriptManager; set => scriptManager = value; }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }
            OpenFileDialog dialog = (OpenFileDialog)sender;           
            ScriptManager.Load(dialog.FileName);
        }

        public void addLog(String console)
        {
            //listBox1.Items.Add(console);
            listBox1.Invoke((MethodInvoker)(() => listBox1.Items.Add(console)));
        }


        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/Karamelon/TanjiLua");
        }
    }
}
