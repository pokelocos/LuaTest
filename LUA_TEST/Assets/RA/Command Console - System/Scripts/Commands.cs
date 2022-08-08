using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace RA.CommandConsole
{
    public static class Commands
    {
        public static Console main;

        internal static List<object> commandList = new List<object>();
        private static bool _metaLog = false;

        public static void start()
        {
            var spawnNode = new DebugCommand<string>("SpawnNode", "", "spawn_node", (x) => { 
                Debug.Log("a"); 
            });
           
            var removeNode = new DebugCommand<string>("RemoveNode", "", "remove_node", (x) => {
                Debug.Log("a");
            });
            
            var spawnEffect = new DebugCommand<string>("SpawnEffect", "", "spawn_effect", (x) => {
                Debug.Log("a");
            });
           
            var removeEffect = new DebugCommand<string>("RemoveEffect", "", "remove_effect", (x) => {
                Debug.Log("a");
            });
           
            var addMoney = new DebugCommand<string>("AddMoney", "", "add_money", (x) => {
                Debug.Log("a");
            });
           
            var setCycle = new DebugCommand<string>("SetCycle", "", "set_cycle", (x) => {
                Debug.Log("a");
            });
          
            var destroyAll = new DebugCommand<string>("DestroyAll", "", "destroy_all", (x) => {
                Debug.Log("a");
            });
           
            var getReward = new DebugCommand<string>("GetReward", "", "get_reward", (x) => {
                Debug.Log("a");
            });
            
            var help = new DebugCommand<string>("Help", "", "help", (x) => {
                Debug.Log("a");
            });
        }
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static List<object> CollectMetohdsFromScripts() 
        {
            List<object> commands = new List<object>();

            var methods = Assembly.GetExecutingAssembly()
                        .GetTypes()
                        .SelectMany(t => t.GetMethods())
                        .Where(m => m.GetCustomAttributes(typeof(CommandAttribute), false).Length > 0)
                        .ToArray();

            foreach (var method in methods)
            {
                var attribute = method.GetCustomAttributes(typeof(CommandAttribute), false)[0] as CommandAttribute;
                var action = (Action)method.CreateDelegate(typeof(Action));
                commands.Add(new DebugCommand(attribute.Id, attribute.Description, attribute.Format, action));
            }
            commandList = commands;
            return commands;
        }

        /// <summary>
        /// Takes the text delivered by the input, divides it and identifies it,
        /// finally executes the code associated with the command.
        /// </summary>
        public static bool HandleInput(string input)
        {
            string[] properties = input.Split(' ');
            for (int i = 0; i < commandList.Count; i++)
            {
                var command = commandList[i];
                var commandBase = command as DebugCommandBase;
               
                if (input.Contains(commandBase.CommandId))
                {
                    if (commandList[i] as DebugCommand != null)
                    {
                        (commandList[i] as DebugCommand).Invoke();
                        return true;
                    }
                    else if ((commandList[i] as DebugCommand<int>) != null) // cambiar esto a algo con tipos dinamicos
                    {
                        (commandList[i] as DebugCommand<int>).Invoke(int.Parse(properties[i]));
                        return true;
                    }
                }
            }
            return false;
        }

        public static void Log(string s)
        {
            Debug.Log(s);
            if(main != null)
            {
                main.Log(s);
            }
        }
    }

    

    public static class CommandUtils
    {
        [Command("help", "show a list of commands.", "help")]
        public static void Help()
        {
            var commands = Commands.commandList;

            for (int i = 0; i < commands.Count; i++)
            {
                var command = commands[i] as DebugCommandBase;

                string label = $"{command.CommandFormat} - {command.CommandDescription}";
                Commands.Log(label);
            }
        }
    }

}