using MoonSharp.Interpreter;
using Sulakore.Communication;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanjiLuaModule.Engine.Types
{
    internal class IncomingType
    {

        private readonly ScriptProcess _scriptProcess;
        private readonly MainForm _mainFrom;

        public IncomingType(MainForm mainForm, ScriptProcess script)
        {
            this._mainFrom = mainForm;
            this._scriptProcess = script;
        }

        public void Send(ushort header, List<DynValue> data)
        {
            var values = new object[data.Count];
            var pos = 0;
            foreach (var val in data)
            {
                switch (val.Type)
                {
                    case DataType.Number:
                        values[pos] = val.Number;
                        break;
                    case DataType.String:
                        values[pos] = val.String;
                        break;
                    case DataType.Boolean:
                        values[pos] = val.Boolean;
                        break;                      
                }

                pos++;
            }
            _mainFrom.Connection.SendToClientAsync(header, values);
        }
        public void Register(int packet)
        {
            var lua = _scriptProcess.Script;
            _scriptProcess.RegistredPacketHandlers.Add(packet, RegisterType.In);
            _mainFrom.Triggers.InAttach((ushort)packet, (dt) =>
            {
                ClientMessageRecivedFire(packet, dt);
            });
        }

        private void ClientMessageRecivedFire(int header, DataInterceptedEventArgs dt)
        {
            try
            {

                _scriptProcess.RegistredHandlers.Add(_scriptProcess.Last, dt);
                _scriptProcess.Script.CallAsync(_scriptProcess.Script.Globals["ClientMessageHandler"],
                    DynValue.NewNumber(header),
                    DynValue.NewNumber(_scriptProcess.Last));
                _scriptProcess.Last++;
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
