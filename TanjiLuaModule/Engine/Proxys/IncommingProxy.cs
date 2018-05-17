using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanjiLuaModule.Engine.Types
{
    class IncommingProxy
    {
        IncommingType type;
        [MoonSharpHidden]
        public IncommingProxy(IncommingType type)
        {
            this.type = type;
        }

        public void send(DynValue value, List<DynValue> values){
            type.Send((ushort)value.Number, values);
        }

        public void register(DynValue value)
        {
            if (value.Type == DataType.Number)
            {
                type.Register((int)value.Number);
            }
            else
            {
                throw new ScriptRuntimeException("(Incomming:register) Only accepts STRING or INT.");
            }
        }
    }
}
