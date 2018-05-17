using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanjiLuaModule.Engine.Types
{
    class IncommingType
    {
        ScriptProcess scriptProcess;
        MainForm Loader { get; }
        public IncommingType(MainForm loader, ScriptProcess script)
        {
            Loader = loader;
            this.scriptProcess = script;
        }

        public void Send(ushort header, List<DynValue> data)
        {
            object[] values = new object[data.Count];
            int pos = 0;
            foreach (DynValue val in data)
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

            Loader.Connection.SendToClientAsync(header, values);
        }
        public void Register(int packet)
        {
            Script lua = scriptProcess.Script;
            scriptProcess.RegistredPacketHandlers.Add(packet, RegisterType.IN);
            Loader.Triggers.InAttach(ushort.Parse(packet + ""), (dt) =>
            {
                scriptProcess.ClientMessageRecivedFire(packet, dt);
            }
            );
        }
    }
}
