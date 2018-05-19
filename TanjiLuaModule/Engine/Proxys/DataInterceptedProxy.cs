using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanjiLuaModule.Engine.Proxys
{
    internal class DataInterceptedProxy
    {
        private readonly DataInterceptedType _type;

        [MoonSharpHidden]
        public DataInterceptedProxy(DataInterceptedType type)
        {
            this._type = type;
        }

        public string String()
        {
            return _type.String();
        }
        public string Int()
        {
            return _type.Int();
        }
        public string Bool()
        {
            return _type.Bool();
        }
        public string Short()
        {
            return _type.Short();
        }

        public List<DynValue> Data(DynValue id, IEnumerable<DynValue> dataTypes)
        {
            return _type.GetInterceptedData(id, dataTypes);
        }
 
    }
}
