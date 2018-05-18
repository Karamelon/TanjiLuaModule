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
        private MainForm mainForm;

        public GuiType(MainForm mainForm, ScriptProcess scriptp)
        {
            this.mainForm = mainForm;
            scriptProcess = scriptp;
            Form = new Form();
            Form.Disposed += new EventHandler(delegate
            {
                scriptProcess.ScriptManager.Remove(scriptProcess);
            });
            
        }

        public void Create(string title, int width, int height)
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
            Form.FormBorderStyle = FormBorderStyle.FixedToolWindow;
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
                try
                {
                    script.Call(script.Globals["button_"+id+"_click"]);
                }
                catch (Exception) { }
            });
            Form.Controls.Add(button);
        }


        public void AddLabel(string id, string text, int x, int y)
        {
            Script script = scriptProcess.Script;
            Label label = new Label
            {
                Name = id,
                Text = text,
                Location = new Point(x, y)
            };
            Form.Controls.Add(label);
        }

        public void AddCheckBox(string id, string text, int x, int y)
        {
            Script script = scriptProcess.Script;
            CheckBox checkbox = new CheckBox
            {
                Name = id,
                Text = text,
                Location = new Point(x, y)
            };
            checkbox.Click += new EventHandler(delegate {
                try
                {
                    script.Call(script.Globals["checkbox_" + id + "_click"], checkbox.Checked);
                } catch (Exception) { }
            });
            Form.Controls.Add(checkbox);

        }

        public void AddTextbox(string id, int x, int y)
        {
            Script script = scriptProcess.Script;
            TextBox textbox = new TextBox()
            {
                Name = id,
                Location = new Point(x, y)
            };
            Form.Controls.Add(textbox);
        }

        public String GetValue(String name)
        {
            if (!Form.Controls.ContainsKey(name))
            {
                return null;
            }
            Control[] ctns = Form.Controls.Find(name, true);
            var ctn = ctns.GetValue(0) as Control;
            return ctn.Text;
        }

        public void SetValue(String name, String value)
        {
            if (!Form.Controls.ContainsKey(name))
            {
                return;
            }
            Control[] ctns = Form.Controls.Find(name, true);
            var ctn = ctns.GetValue(0) as Control;
            ctn.Text = value;
        }

        public bool IsChecked(String ckbox)
        {
            if (!Form.Controls.ContainsKey(ckbox))
            {
                return false;
            }
            Control[] ctns = Form.Controls.Find(ckbox, true);
            var ctn = ctns.GetValue(0) as Control;
            if (ctn is CheckBox)
            {
                var ckb = ctn as CheckBox;
                return ckb.Checked;

            }
            return false;
        }

        public void Show()
        {
            Form.Show();
        }
    }
}
