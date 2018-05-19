using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanjiLuaModule.Engine.Types;

namespace TanjiLuaModule.Engine.Proxys
{
    internal class OutgoingProxy
    {
        private readonly OutgoingType _type;

        [MoonSharpHidden]
        public OutgoingProxy(OutgoingType type)
        {
            this._type = type;
        }

        public void Send(DynValue header, List<DynValue>send)
        {
            _type.Send((ushort)header.Number, send);
        }

        public void Register(DynValue value)
        {
            if (value.Type == DataType.Number)
            {
                _type.Register((int)value.Number);
            }
        }
    }
}
