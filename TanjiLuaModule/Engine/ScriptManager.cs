using MoonSharp.Interpreter;
using Sulakore.Communication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanjiLuaModule.Engine
{
    internal enum RegisterType
    {
        In,
        Out
    }

    internal class ScriptManager
    {
        private readonly List<ScriptProcess> _process = new List<ScriptProcess>();


        public ScriptManager(MainForm main)
        {
        }

        public void Load(ScriptProcess sp)
        { 
            sp.Load();
            _process.Add(sp);
        }

        public void Remove(ScriptProcess script)
        {
            _process.Remove(script);
            script.Dispose();
        }

    }
}
