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
        private ScriptProcess scriptProcess;
        private MainForm mainForm;

        public OutgoingType(MainForm mainForm, ScriptProcess script)
        {
            this.mainForm = mainForm;
            this.scriptProcess = script;
        }

        public void Send(ushort header, List<DynValue> vlas)
        {
            object[] values = new object[vlas.Count];
            int pos = 0;
            foreach (DynValue val in vlas)
            {
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
            mainForm.Connection.SendToServerAsync(header, values);
        }

        public void Register(int packet)
        {
            Script lua = scriptProcess.Script;
            scriptProcess.RegistredPacketHandlers.Add(packet, RegisterType.OUT);
            mainForm.Triggers.OutAttach((ushort)packet, (dt)=>
            {
                ServerMessageRecivedFire(packet, dt);
            }
            );

        }
        public void ServerMessageRecivedFire(int header, DataInterceptedEventArgs dt)
        {
            try
            {
                scriptProcess.RegistredHandlers.Add(scriptProcess.Last, dt);
                scriptProcess.Script.CallAsync(scriptProcess.Script.Globals["ServerMessageHandler"],
                    DynValue.NewNumber(header),
                    DynValue.NewNumber(scriptProcess.Last));
                scriptProcess.Last++;
            }
            catch (Exception) { }
        }
    }
}
