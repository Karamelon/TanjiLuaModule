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
        MainForm mainLoader;
        public String Dir;

        long last = 1;
        public Dictionary<long, DataInterceptedEventArgs> RegistredHandlers = new Dictionary<long, DataInterceptedEventArgs>();
        public Dictionary<int, RegisterType> RegistredPacketHandlers = new Dictionary<int, RegisterType>();

        public ScriptProcess(MainForm loader, String dir)
        {
            Dir = dir;
            this.mainLoader = loader;   
        }
         
        public void ServerMessageRecivedFire(int header, DataInterceptedEventArgs dt)
        {
            RegistredHandlers.Add(last, dt);
            Script.CallAsync(Script.Globals["ServerMessageHandler"], DynValue.NewNumber(header), DynValue.NewNumber(last));
            last++;
        }

        public void ClientMessageRecivedFire(int header, DataInterceptedEventArgs dt)
        {
            RegistredHandlers.Add(last, dt);
            Script.CallAsync(Script.Globals["ClientMessageHandler"], DynValue.NewNumber(header), DynValue.NewNumber(last));
            last++;
        }

        public void Dispose()
        {
            foreach (KeyValuePair<int, RegisterType> entry in RegistredPacketHandlers)
            {
                if (entry.Value == RegisterType.OUT)
                    mainLoader.Triggers.OutDetach(ushort.Parse(entry.Key.ToString()));
                else
                    mainLoader.Triggers.InDetach(ushort.Parse(entry.Key.ToString()));
            }
            Script = null;
            last = 1;
            RegistredHandlers.Clear();
            RegistredPacketHandlers.Clear();
        }

        public void Load()
        {
            try
            {                
                Script = new Script();
                UserData.RegisterProxyType<IncommingProxy, IncommingType>(r => new IncommingProxy(r));
                UserData.RegisterProxyType<OutgoingProxy, OutgoingType>(r => new OutgoingProxy(r));
                UserData.RegisterProxyType<DataInterceptedProxy, DataInterceptedType>(r => new DataInterceptedProxy(r));
                UserData.RegisterProxyType<GuiProxy, GuiType>(r => new GuiProxy(r));
                Script.Options.ScriptLoader = new EmbeddedResourcesScriptLoader();
                Script.GlobalOptions.Platform = new StandardPlatformAccessor();
                Script.Globals["Client"] = new IncommingType(mainLoader, this);
                Script.Globals["Server"] = new OutgoingType(mainLoader, this);
                Script.Globals["Gui"] = new GuiType(mainLoader, this);
                Script.Globals["Intercept"] = new DataInterceptedType(mainLoader, this);
                //Util
                Script.Globals["msgBox"] = (Action<String,String>)((v1,v2)=> MessageBox.Show(v2,v1));
                Script.Globals["print"] = (Action<String>)((v1) => mainLoader.addLog(v1));
                Script.DoString(File.ReadAllText(Dir));           
                mainLoader.addLog("SCRIPT -> " + Path.GetFileName(Dir) + " LOADED");
            }
            catch (ScriptRuntimeException sre)
            {
                mainLoader.addLog("LUA ERROR: " + sre.ToString());
                mainLoader.ScriptManager.Remove(this);
            }
            catch (SyntaxErrorException sye)
            {
                mainLoader.addLog("LUA SYNTAX ERROR: " + sye.Message);
                mainLoader.ScriptManager.Remove(this);
            }

        }

    }
}
