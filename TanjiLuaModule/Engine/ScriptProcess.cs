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
    internal class ScriptProcess
    {
        public Script Script;
        private readonly string _scriptFile;
        public long Last = 1;
        public readonly Dictionary<long, DataInterceptedEventArgs> RegistredHandlers = new Dictionary<long, DataInterceptedEventArgs>();
        public readonly Dictionary<int, RegisterType> RegistredPacketHandlers = new Dictionary<int, RegisterType>();

        private readonly MainForm _mainForm;
        private ScriptManager ScriptManager { get; }

        public ScriptProcess(MainForm mainForm, string file, ScriptManager scriptManager)
        {
            _scriptFile = file;
            ScriptManager = scriptManager;
            this._mainForm = mainForm;   
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

                Script.GlobalOptions.Platform = new StandardPlatformAccessor();

                Script.Options.ScriptLoader = new EmbeddedResourcesScriptLoader();
                Script.Globals["Client"] = new IncomingType(_mainForm, this);
                Script.Globals["Server"] = new OutgoingType(_mainForm, this);
                Script.Globals["Gui"] = new GuiType(_mainForm, this);
                Script.Globals["Intercept"] = new DataInterceptedType(_mainForm, this);
                //Util
                Script.Globals["msgBox"] = (Action<string,string>)((v1,v2)=> MessageBox.Show(v2,v1));
                Script.Globals["print"] = (Action<string>)((v1) => _mainForm.AddLog(v1));

                Script.DoString(File.ReadAllText(_scriptFile));           
                _mainForm.AddLog("SCRIPT -> " + Path.GetFileName(_scriptFile) + " LOADED");
            }
            catch (ScriptRuntimeException sre)
            {
                _mainForm.AddLog("LUA ERROR: " + sre.ToString());
                ScriptManager.Remove(this);
            }
            catch (SyntaxErrorException sye)
            {
                _mainForm.AddLog("LUA SYNTAX ERROR: " + sye.Message);
                ScriptManager.Remove(this);
            }

        }

        public void Dispose()
        {
            foreach (var entry in RegistredPacketHandlers)
            {
                if (entry.Value == RegisterType.Out)
                    _mainForm.Triggers.OutDetach(ushort.Parse(entry.Key.ToString()));
                else
                    _mainForm.Triggers.InDetach(ushort.Parse(entry.Key.ToString()));
            }
            Script = null;
            Last = 1;
            RegistredHandlers.Clear();
            RegistredPacketHandlers.Clear();
        }
    }
}
