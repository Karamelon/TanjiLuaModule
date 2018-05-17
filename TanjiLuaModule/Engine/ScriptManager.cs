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
        //public List<ScriptProcess> process = new List<ScriptProcess>();

        public MainForm MainForm { get; }


        public ScriptManager(MainForm main)
        {
            MainForm = main;
        }

        public void Load(string dir)
        {
            ScriptProcess scriptProcess = new ScriptProcess(MainForm, dir);
            scriptProcess.Load();
            process.Add(dir,scriptProcess);
        }

        public void Remove(ScriptProcess script)
        {
            process.Remove(script.Dir);
            script.Dispose();
        }

    }
}
