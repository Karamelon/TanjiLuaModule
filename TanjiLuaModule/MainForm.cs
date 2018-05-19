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
        private readonly ScriptManager _scriptManager;

        public MainForm()
        {
            _scriptManager = new ScriptManager(this);
            InitializeComponent();
            
        }


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
            if (sender is OpenFileDialog dialog) 
                _scriptManager.Load(new ScriptProcess(this, dialog.FileName, _scriptManager));
        }

        public void AddLog(string console)
        {
            listBox1.Invoke((MethodInvoker)(() => {
                var visibleItems = listBox1.ClientSize.Height / listBox1.ItemHeight;
                listBox1.TopIndex = Math.Max(listBox1.Items.Count - visibleItems + 1, 0);
                listBox1.Items.Add(console); }));
        }


        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/Karamelon/TanjiLua");
        }
    }
}
