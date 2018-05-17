using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanjiLuaModule.Engine.Types;

namespace TanjiLuaModule.Engine.Proxys
{
    class GuiProxy
    {
        GuiType type;
        [MoonSharpHidden]
        public GuiProxy(GuiType type)
        {
            this.type = type;
        }

        public void TopMost()
        {
            type.TopMost(true);
        }

        public void TopMost(DynValue actived)
        {
            type.TopMost(actived.Boolean);
        }
        public void Create(DynValue name, DynValue x, DynValue y)
        {
            type.Create(name.String, (int)x.Number, (int)y.Number);
        }
        public void AddButton(DynValue id, DynValue text, DynValue x, DynValue y)
        {
            type.AddButton(id.String, text.String, (int)x.Number, (int)y.Number);
        }
        public void AddCheckBox(DynValue id, DynValue text, DynValue x, DynValue y)
        {
            type.AddCheckBox(id.String, text.String, (int)x.Number, (int)y.Number);
        }
        public void AddLabel(DynValue id, DynValue text, DynValue x, DynValue y)
        {
            type.AddLabel(id.String, text.String, (int)x.Number, (int)y.Number);
        }
        public void Show(DynValue value)
        {
            type.Show();
        }
    }
}
