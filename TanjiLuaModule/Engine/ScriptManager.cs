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
    enum RegisterType
    {
        IN,
        OUT
    }
    class ScriptManager
    {

        public Dictionary<String, ScriptProcess> process = new Dictionary<string, ScriptProcess>();

        public MainForm MainForm { get; }


        public ScriptManager(MainForm main)
        {
            MainForm = main;
        }

        public void Load(string dir)
        {
            if (process.ContainsKey(dir))
            {
                process.TryGetValue(dir,out ScriptProcess sp);
                sp.Dispose();
                process.Remove(dir);
            }
            ScriptProcess scriptProcess = new ScriptProcess(MainForm, dir, this);
            scriptProcess.Load();
            process.Add(dir,scriptProcess);
        }

        public void Remove(ScriptProcess script)
        {
            process.Remove(script.ScriptFile);
            script.Dispose();
        }

    }
}
