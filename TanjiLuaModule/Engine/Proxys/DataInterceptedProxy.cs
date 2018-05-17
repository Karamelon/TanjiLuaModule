using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanjiLuaModule.Engine.Proxys
{
    class DataInterceptedProxy
    {
        DataInterceptedType type;

        [MoonSharpHidden]
        public DataInterceptedProxy(DataInterceptedType type)
        {
            this.type = type;
        }

        public String STRING()
        {
            return "str";
        }
        public String INT()
        {
            return "int";
        }
        public String BOOL()
        {
            return "bool";
        }
        public String SHORT()
        {
            return "short";
        }

        public List<DynValue> data(DynValue id, List<DynValue> dataTypes)
        {
            return type.GetInterceptedData(id, dataTypes);
        }
 
    }
}
