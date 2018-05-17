using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TanjiLuaModule.Engine.Types
{
    class GuiType
    {
        private Form Form;
        private ScriptProcess scriptProcess;
        private MainForm Loader;

        public GuiType(MainForm main, ScriptProcess scriptp)
        {
            Loader = main;
            scriptProcess = scriptp;
            Form = new Form();
            Form.Disposed += new EventHandler(delegate
            {
                Loader.ScriptManager.Remove(scriptProcess);
            });
            
        }

        public void Create(string title, int width, int height)
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
            Form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Form.Width = width;
            Form.Text = title;
            Form.Height = height;
            Form.Icon = ((Icon)(resources.GetObject("$this.Icon")));
        }

        public void TopMost(bool active)
        {
            Form.TopMost = active;
        }

        public void AddButton(string id, string text, int x, int y)
        {
            Script script = scriptProcess.Script;
            Button button = new Button
            {
                Name = id,
                Text = text,
                Location = new Point(x, y)
            };
            button.Click += new EventHandler(delegate {
                script.Call(script.Globals["button_"+id+"_click"]);
            });
            Form.Controls.Add(button);
        }


        public void AddLabel(string id, string text, int x, int y)
        {
            Script script = scriptProcess.Script;
            Label button = new Label
            {
                Name = id,
                Text = text,
                Location = new Point(x, y)
            };
            Form.Controls.Add(button);
        }

        public void AddCheckBox(string id, string text, int x, int y)
        {
            Script script = scriptProcess.Script;
            CheckBox button = new CheckBox
            {
                Name = id,
                Text = text,
                Location = new Point(x, y)
            };
            button.Click += new EventHandler(delegate {
                script.Call(script.Globals["checkbox_" + id + "_click"], button.Checked);
            });
            Form.Controls.Add(button);

        }

        public void Show()
        {
            Form.Show();
        }
    }
}
