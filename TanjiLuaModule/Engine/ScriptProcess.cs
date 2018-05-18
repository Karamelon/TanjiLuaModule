using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
using MoonSharp.Interpreter.Platforms;
using Sulakore.Communication;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TanjiLuaModule.Engine.Proxys;
using TanjiLuaModule.Engine.Types;

namespace TanjiLuaModule.Engine
{
    class ScriptProcess
    {
        public Script Script;
        public String ScriptFile;
        public long Last = 1;
        public Dictionary<long, DataInterceptedEventArgs> RegistredHandlers = new Dictionary<long, DataInterceptedEventArgs>();
        public Dictionary<int, RegisterType> RegistredPacketHandlers = new Dictionary<int, RegisterType>();

        private MainForm mainForm;
        public ScriptManager ScriptManager { get; }

        public ScriptProcess(MainForm mainForm, String file, ScriptManager scriptManager)
        {
            ScriptFile = file;
            this.mainForm = mainForm;   
        }
      
        public void Load()
        {
            try
            {                
                Script = new Script();

                UserData.RegisterProxyType<IncomingProxy, IncomingType>(r => new IncomingProxy(r));
                UserData.RegisterProxyType<OutgoingProxy, OutgoingType>(r => new OutgoingProxy(r));
                UserData.RegisterProxyType<DataInterceptedProxy, DataInterceptedType>(r => new DataInterceptedProxy(r));
                UserData.RegisterProxyType<GuiProxy, GuiType>(r => new GuiProxy(r));

                Script.Options.ScriptLoader = new EmbeddedResourcesScriptLoader();
                Script.GlobalOptions.Platform = new StandardPlatformAccessor();
                
                Script.Globals["Client"] = new IncomingType(mainForm, this);
                Script.Globals["Server"] = new OutgoingType(mainForm, this);
                Script.Globals["Gui"] = new GuiType(mainForm, this);
                Script.Globals["Intercept"] = new DataInterceptedType(mainForm, this);
                //Util
                Script.Globals["msgBox"] = (Action<String,String>)((v1,v2)=> MessageBox.Show(v2,v1));
                Script.Globals["print"] = (Action<String>)((v1) => mainForm.AddLog(v1));
                Script.DoString(File.ReadAllText(ScriptFile));           
                mainForm.AddLog("SCRIPT -> " + Path.GetFileName(ScriptFile) + " LOADED");
            }
            catch (ScriptRuntimeException sre)
            {
                mainForm.AddLog("LUA ERROR: " + sre.ToString());
                ScriptManager.Remove(this);
            }
            catch (SyntaxErrorException sye)
            {
                mainForm.AddLog("LUA SYNTAX ERROR: " + sye.Message);
                ScriptManager.Remove(this);
            }

        }

        public void Dispose()
        {
            foreach (KeyValuePair<int, RegisterType> entry in RegistredPacketHandlers)
            {
                if (entry.Value == RegisterType.OUT)
                    mainForm.Triggers.OutDetach(ushort.Parse(entry.Key.ToString()));
                else
                    mainForm.Triggers.InDetach(ushort.Parse(entry.Key.ToString()));
            }
            Script = null;
            Last = 1;
            RegistredHandlers.Clear();
            RegistredPacketHandlers.Clear();
        }
    }
}
