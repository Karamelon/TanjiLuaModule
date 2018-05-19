using MoonSharp.Interpreter;
using Sulakore.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TanjiLuaModule.Engine.Proxys
{
    class DataInterceptedType
    {
        private readonly ScriptProcess _script;

        [MoonSharpHidden]
        public DataInterceptedType(MainForm mainForm, ScriptProcess script)
        {
            this._script = script;
        }

        public string String()
        {
            return "str";
        }
        public string Int()
        {
            return "int";
        }
        public string Bool()
        {
            return "bool";
        }
        public string Short()
        {
            return "short";
        }

        public List<DynValue> GetInterceptedData(DynValue id, IEnumerable<DynValue> dataTypes)
        {
            var table = new List<DynValue>();
            if (!_script.RegistredHandlers.ContainsKey((long)id.Number))
            {
                return table;
            }
            _script.RegistredHandlers.TryGetValue((long)id.Number, out var type);
            _script.RegistredHandlers.Remove((long)id.Number);
            foreach (var value in dataTypes)
            {
                switch (value.String.ToLower())
                {
                    case "int":
                        if (type != null) table.Add(DynValue.NewNumber(type.Packet.ReadInteger()));
                        break;
                    case "str":
                        if (type != null) table.Add(DynValue.NewString(type.Packet.ReadString()));
                        break;
                    case "bool" :
                        table.Add(DynValue.NewBoolean(type != null && type.Packet.ReadBoolean()));
                        break;
                    case "short":
                        if (type != null) table.Add(DynValue.NewNumber(type.Packet.ReadShort()));
                        break;
                    default:
                        continue;
                }
            }
            return table;
        }
    }
}
