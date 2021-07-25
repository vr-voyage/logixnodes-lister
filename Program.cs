using System;
using System.Reflection;

namespace ListFrooxTypes
{
    class Program
    {
        static string[] ignoreList = new string[]
        {
            "FrooxEngine.LogiX.AsInput",
            "FrooxEngine.LogiX.AsOutput",
            "FrooxEngine.LogiX.CacheNode`1",
            "FrooxEngine.LogiX.ConnectPointSide",
            "FrooxEngine.LogiX.ConnectionWire",
            "FrooxEngine.LogiX.CurrentCulture",
            "FrooxEngine.LogiX.DefaultNodes",
            "FrooxEngine.LogiX.DelayValueNode`1",
            "FrooxEngine.LogiX.DriveProxy",
            "FrooxEngine.LogiX.DriverNode",
            "FrooxEngine.LogiX.DriverNode`1",
            "FrooxEngine.LogiX.DualInputOperator`1",
            "FrooxEngine.LogiX.DynamicImpulseTarget",
            "FrooxEngine.LogiX.HiddenNode",
            "FrooxEngine.LogiX.IConnectionElement",
            "FrooxEngine.LogiX.IContextData",
            "FrooxEngine.LogiX.IDriverNode",
            "FrooxEngine.LogiX.IInputElement",
            "FrooxEngine.LogiX.IOutputElement",
            "FrooxEngine.LogiX.IPassthroughNode",
            "FrooxEngine.LogiX.IReferenceNode",
            "FrooxEngine.LogiX.Impulse",
            "FrooxEngine.LogiX.ImpulseRelay",
            "FrooxEngine.LogiX.ImpulseSourceProxy",
            "FrooxEngine.LogiX.ImpulseTarget",
            "FrooxEngine.LogiX.ImpulseTargetInfo",
            "FrooxEngine.LogiX.ImpulseTargetProxy",
            "FrooxEngine.LogiX.InputOverride",
            "FrooxEngine.LogiX.InputProxy",
            "FrooxEngine.LogiX.Input`1",
            "FrooxEngine.LogiX.InvariantCulture",
            "FrooxEngine.LogiX.LogixContext",
            "FrooxEngine.LogiX.LogixController",
            "FrooxEngine.LogiX.LogixHelper",
            "FrooxEngine.LogiX.LogixInterface",
            "FrooxEngine.LogiX.LogixInterfaceOpener",
            "FrooxEngine.LogiX.LogixInterfaceProxy",
            "FrooxEngine.LogiX.LogixNode",
            "FrooxEngine.LogiX.LogixNodeSelector",
            "FrooxEngine.LogiX.LogixOperator`1",
            "FrooxEngine.LogiX.LogixReference",
            "FrooxEngine.LogiX.LogixReferenceExtensions",
            "FrooxEngine.LogiX.LogixTip",
            "FrooxEngine.LogiX.LogixTraversal",
            "FrooxEngine.LogiX.MemberProxy",
            "FrooxEngine.LogiX.MultiInputOperator`1",
            "FrooxEngine.LogiX.MultiInputOperator`2",
            "FrooxEngine.LogiX.NodeDefaultTypeAttribute",
            "FrooxEngine.LogiX.NodeTypes",
            "FrooxEngine.LogiX.NodeVisualType",
            "FrooxEngine.LogiX.OutputProxy",
            "FrooxEngine.LogiX.Output`1",
            "FrooxEngine.LogiX.PrimitiveLogixOperator`1",
            "FrooxEngine.LogiX.ReferenceNode",
            "FrooxEngine.LogiX.ReferenceNode`1",
            "FrooxEngine.LogiX.RelayNode`1",
            "FrooxEngine.LogiX.TypeCastCompatibility",
            "FrooxEngine.LogiX.UpdatingRelayNode`1",
            "FrooxEngine.LogiX.VariableInputOutputNode",
            "FrooxEngine.LogiX.WriteLatch`1",
            "FrooxEngine.Logix.Input.ValueTextFieldNodeBase`1",
            "FrooxEngine.Logix.Input.ControllerBase`1",
            "FrooxEngine.Logix.Input.ControllerNode`1",
            "FrooxEngine.LogiX.Assets.AttachAsset`1",
            "FrooxEngine.LogiX.Avatar.AnchorEventNode",
            "FrooxEngine.LogiX.Cast.CastNode`2",
            "FrooxEngine.LogiX.DualInputOperator`1",
            "FrooxEngine.LogiX.Input.ControllerBase`1",
            "FrooxEngine.LogiX.Input.ControllerNode`1",
            "FrooxEngine.LogiX.Input.TextFieldNodeBase`1",
            "FrooxEngine.LogiX.Input.ValueTextFieldNodeBase`1",
            "FrooxEngine.LogiX.LogixNode",
            "FrooxEngine.LogiX.LogixOperator`1",
            "FrooxEngine.LogiX.MultiInputOperator`1",
            "FrooxEngine.LogiX.MultiInputOperator`2",
            "FrooxEngine.LogiX.Network.WebsocketBaseNode",
            "FrooxEngine.LogiX.Playback.PlaybackSetter",
            "FrooxEngine.LogiX.PrimitiveLogixOperator`1",
            "FrooxEngine.LogiX.ProgramFlow.FireOnBool",
            "FrooxEngine.LogiX.ProgramFlow.FireOnChangeBase`1",
            "FrooxEngine.LogiX.ProgramFlow.LocalFireOnBool",
            "FrooxEngine.LogiX.Twitch.TwitchNode",
            "FrooxEngine.LogiX.Utility.BodyNodeOperator`1",
            "FrooxEngine.LogiX.VariableInputOutputNode"
        };

        static bool IgnoredTypeName(string typeName)
        {
            bool ignored = false;
            int ignoredCount = ignoreList.Length;
            for (int i = 0; i < ignoredCount && !ignored; i++)
            {
                ignored = (ignoreList[i] == typeName);
            }
            return ignored;
        
        }

        static void LogiXListInputs(Type logixNodeType)
        {
            foreach (FieldInfo f in logixNodeType.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public))
            {
                Type fieldType = f.FieldType;
                string fieldName = f.Name;
                switch(fieldType.Name)
                {
                    case "Input`1":
                        Console.WriteLine("Input   : {0} ({1})", fieldName, fieldType.GetGenericArguments()[0].Name);
                        break;
                    case "Output`1":
                        Console.WriteLine("Output  : {0} ({1})", fieldName, fieldType.GetGenericArguments()[0].Name);
                        break;
                    case "Impulse":
                        Console.WriteLine("Impulse : {0} ({1})", fieldName, fieldType);
                        break;
                }
            }

            foreach (MethodInfo m in logixNodeType.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public))
            {
                foreach (CustomAttributeData c in m.CustomAttributes)
                {
                    if (c.AttributeType == typeof(FrooxEngine.LogiX.ImpulseTarget))
                    {
                        Console.WriteLine("Impulse Target : {0}", m.Name);
                    }
                        
                }
            }

            foreach (Type t in logixNodeType.GetInterfaces())
            {
                string simpleName = t.Name;
                if (simpleName == "IElementContent`1")
                {
                    Console.WriteLine("Output  : {0} ({1})", "*", t.GetGenericArguments()[0].Name);
                }
            }
        }

        static void Main(string[] args)
        {
            Type[] types = typeof(FrooxEngine.LogiX.LogixNode).Assembly.GetTypes();
            foreach (Type t in types)
            {
                string fullName = t.FullName;
                if (!fullName.StartsWith("FrooxEngine.LogiX"))
                    continue;
                
                if (!fullName.Contains("<") && !fullName.Contains("+") && !t.IsAbstract && !IgnoredTypeName(fullName))
                {
                    Console.WriteLine(fullName);
                    LogiXListInputs(t);
                }
                    //Console.WriteLine(fullName);

                
            }
            Console.WriteLine("Hello World!");
            //LogiXListInputs(typeof(FrooxEngine.LogiX.Experimental.WriteTextToFile));
        }
    }
}
