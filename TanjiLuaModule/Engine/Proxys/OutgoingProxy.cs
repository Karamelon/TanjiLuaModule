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
    class OutgoingProxy
    {
        OutgoingType type;

        [MoonSharpHidden]
        public OutgoingProxy(OutgoingType type)
        {
            this.type = type;
        }

        public void Send(DynValue header, List<DynValue>send)
        {
            type.Send((ushort)header.Number, send);
        }

        public void Register(DynValue value)
        {
            if (value.Type == DataType.Number)
            {
                type.Register((int)value.Number);
            }
        }
    }
}
