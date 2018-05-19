using MoonSharp.Interpreter;
using Sulakore.Communication;
using System;
using System.Collections.Generic;

namespace TanjiLuaModule.Engine.Types
{
    internal class OutgoingType
    {
        private readonly ScriptProcess _scriptProcess;
        private readonly MainForm _mainForm;

        public OutgoingType(MainForm mainForm, ScriptProcess script)
        {
            this._mainForm = mainForm;
            this._scriptProcess = script;
        }

        public void Send(ushort header, List<DynValue> vlas)
        {
            var values = new object[vlas.Count];
            var pos = 0;
            foreach (var val in vlas)
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
            _mainForm.Connection.SendToServerAsync(header, values);
        }

        public void Register(int packet)
        {
            var lua = _scriptProcess.Script;
            _scriptProcess.RegistredPacketHandlers.Add(packet, RegisterType.Out);
            _mainForm.Triggers.OutAttach((ushort)packet, (dt)=>
            {
                ServerMessageRecivedFire(packet, dt);
            }
            );

        }

        private void ServerMessageRecivedFire(int header, DataInterceptedEventArgs dt)
        {
            try
            {
                _scriptProcess.RegistredHandlers.Add(_scriptProcess.Last, dt);
                _scriptProcess.Script.CallAsync(_scriptProcess.Script.Globals["ServerMessageHandler"],
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
