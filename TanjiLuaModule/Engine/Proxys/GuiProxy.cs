using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanjiLuaModule.Engine.Types;

namespace TanjiLuaModule.Engine.Proxys
{
    internal class GuiProxy
    {
        private readonly GuiType _type;

        [MoonSharpHidden]
        public GuiProxy(GuiType type)
        {
            this._type = type;
        }

        public DynValue GetValue(DynValue name)
        {
            return DynValue.NewString(_type.GetValue(name.String));
        }
        public void SetValue(DynValue name, DynValue value)
        {
            _type.SetValue(name.String, value.String);
        }
        public DynValue IsCheked(DynValue name)
        {
            return DynValue.NewBoolean(_type.IsChecked(name.String));
        }
        public void TopMost(DynValue actived)
        {
            _type.TopMost(actived.Boolean);
        }
        public void Create(DynValue name, DynValue x, DynValue y)
        {
            _type.Create(name.String, (int)x.Number, (int)y.Number);
        }
        public void AddButton(DynValue id, DynValue text, DynValue x, DynValue y)
        {
            _type.AddButton(id.String, text.String, (int)x.Number, (int)y.Number);
        }
        public void AddCheckBox(DynValue id, DynValue text, DynValue x, DynValue y)
        {
            _type.AddCheckBox(id.String, text.String, (int)x.Number, (int)y.Number);
        }
        public void AddLabel(DynValue id, DynValue text, DynValue x, DynValue y)
        {
            _type.AddLabel(id.String, text.String, (int)x.Number, (int)y.Number);
        }
        public void Show(DynValue value)
        {
            _type.Show();
        }
    }
}
