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

        public void send(DynValue header, List<DynValue>send)
        {
            type.Send(ushort.Parse(header.Number + ""), send);
        }

        public void register(DynValue value)
        {
            if (value.Type == DataType.Number)
            {
                type.Register((int)value.Number);
            }
            else
            {
                throw new ScriptRuntimeException("(Outgoing:register) Only accepts STRING or INT.");
            }
        }
    }
}
