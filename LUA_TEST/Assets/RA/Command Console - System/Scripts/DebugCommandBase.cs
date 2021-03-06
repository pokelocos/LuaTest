using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RA.CommandConsole
{
    public class DebugCommandBase
    {
        private string _commandId;
        private string _commandDescription;
        private string _commandFormat;

        public string CommandId { get { return _commandId; } }
        public string CommandDescription { get { return _commandDescription; } }
        public string CommandFormat { get { return _commandFormat; } }
        public DebugCommandBase(string id, string description, string format)
        {
            _commandId = id;
            _commandDescription = description;
            _commandFormat = format;
        }
    }

    public class DebugCommand : DebugCommandBase
    {
        private Action command;

        public DebugCommand(string id, string description, string format,Action command) : base(id, description, format)
        {
            this.command = command;
        }

        public void Invoke()
        {
            command.Invoke();
        }
    }

    public class DebugCommand<T1> : DebugCommandBase
    {
        private Action<T1> command;

        public DebugCommand(string id, string description, string format, Action<T1> command) : base(id, description, format)
        {
            this.command = command;
        }

        public void Invoke(T1 value)
        {
            command?.Invoke(value);
        }
    }

    /*
    public class DebugCommand<T1,T2> : DebugCommandBase
    {
        private Action<T1,T2> command;

        public DebugCommand(string id, string description, string format, Action<T1,T2> command) : base(id, description, format)
        {
            this.command = command;
        }

        public void Invoke(T1 v1,T2 v2)
        {
            command?.Invoke(v1, v2);
        }
    }

    public class DebugCommand<T1,T2,T3> : DebugCommandBase
    {
        private Action<T1, T2, T3> command;

        public DebugCommand(string id, string description, string format, Action<T1, T2, T3> command) : base(id, description, format)
        {
            this.command = command;
        }

        public void Invoke(T1 v1, T2 v2,T3 v3)
        {
            command?.Invoke(v1, v2, v3);
        }
    }

    public class DebugCommand<T1, T2, T3, T4> : DebugCommandBase
    {
        private Action<T1, T2, T3, T4> command;

        public DebugCommand(string id, string description, string format, Action<T1, T2, T3, T4> command) : base(id, description, format)
        {
            this.command = command;
        }

        public void Invoke(T1 v1, T2 v2, T3 v3, T4 v4)
        {
            command?.Invoke(v1, v2, v3, v4);
        }
    }
    */
}
