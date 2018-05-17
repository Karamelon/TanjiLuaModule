using MoonSharp.Interpreter;
using Sulakore.Communication;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TanjiLuaModule.Engine.Proxys;

namespace TanjiLuaModule.Engine.Types
{
    class OutgoingType
    {
        ScriptProcess scriptProcess;
        MainForm Loader { get; }
        public OutgoingType(MainForm loader, ScriptProcess script)
        {
            Loader = loader;
            this.scriptProcess = script;
        }

        public void Send(ushort header, List<DynValue> vlas)
        {
            object[] values = new object[vlas.Count];
            int pos = 0;
            foreach (DynValue val in vlas)
            {
                Loader.addLog(val.ToString());
                if (val.Type == DataType.Number)
                {
                    values[pos] = val.Number;
                }
                if (val.Type == DataType.String)
                {
                    values[pos] = val.String;
                }
                if (val.Type == DataType.Boolean)
                {
                    values[pos] = val.Boolean;
                }
                pos++;
            }
            Loader.Connection.SendToServerAsync(header, values);
        }

        public void Register(int packet)
        {
            Script lua = scriptProcess.Script;
            scriptProcess.RegistredPacketHandlers.Add(packet, RegisterType.OUT);
            Loader.Triggers.OutAttach(ushort.Parse(packet+""), (dt)=>
            {
                scriptProcess.ServerMessageRecivedFire(packet, dt);
            }
            );
        }
    }
}
