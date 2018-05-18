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
        ScriptProcess script;
        MainForm mainForm;

        [MoonSharpHidden]
        public DataInterceptedType(MainForm mainForm, ScriptProcess script)
        {
            this.script = script;
            this.mainForm = mainForm;
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

        public List<DynValue> GetInterceptedData(DynValue id, List<DynValue> dataTypes)
        {
            List<DynValue> table = new List<DynValue>();
            if (!script.RegistredHandlers.ContainsKey((long)id.Number))
            {
                return table;
            }
            script.RegistredHandlers.TryGetValue((long)id.Number, out DataInterceptedEventArgs type);
            script.RegistredHandlers.Remove((long)id.Number);
            foreach (DynValue value in dataTypes)
            {
                switch (value.String.ToLower())
                {
                    case "int":
                        table.Add(DynValue.NewNumber(type.Packet.ReadInteger()));
                        break;
                    case "str":
                        table.Add(DynValue.NewString(type.Packet.ReadString()));
                        break;
                    case "bool" :
                        table.Add(DynValue.NewBoolean(type.Packet.ReadBoolean()));
                        break;
                    case "short":
                        table.Add(DynValue.NewNumber(type.Packet.ReadShort()));
                        break;
                    default:
                        continue;
                }
            }
            return table;
        }
    }
}
