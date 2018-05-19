using System.Collections.Generic;
using MoonSharp.Interpreter;
using TanjiLuaModule.Engine.Types;

namespace TanjiLuaModule.Engine.Proxys
{
    internal class IncomingProxy
    {
        private readonly IncomingType _type;
        
        [MoonSharpHidden]
        public IncomingProxy(IncomingType type)
        {
            this._type = type;
        }

        public void Send(DynValue value, List<DynValue> values){
            _type.Send((ushort) value.Number, values);
        }

        public void Register(DynValue value)
        {
            if (value.Type == DataType.Number)
            {
                _type.Register((int) value.Number);
            }
        }
    }
}
