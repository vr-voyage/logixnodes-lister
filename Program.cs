using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.CSharp;
using Newtonsoft.Json;

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

        public static string TypeName(Type t)
        {

            switch(t.Name)
            {
                case "Byte":
                    return "byte";
                case "SByte":
                    return "sbyte";
                case "Char":
                    return "char";
                /* Strangely, Decimal is Decimal
                    * case "Decimal":
                    return "decimal";*/
                case "IntPtr":
                    return "nint";
                case "UIntPtr":
                    return "nuint";
                case "Int64":
                    return "long";
                case "UInt64":
                    return "ulong";
                case "Int16":
                    return "int16";
                case "UInt16":
                    return "uint16";
                case "Boolean":
                    return "bool";
                case "Int32":
                    return "int";
                case "UInt32":
                    return "uint32";
                case "Single":
                    return "float";
                case "Double":
                    return "double";
                case "String":
                    return "string";
                case "Object":
                    return "object";
                default:
                    return t.Name;
            }

            
        }

        struct SlotInfos
        {
            public string name;
            public string type;
            public int generic;

            public SlotInfos(string new_name, Type new_type)
            {
                name = new_name;
                type = TypeName(new_type);
                switch(type)
                {
                    case "T":
                        generic = 1;
                        break;
                    case "N":
                        generic = 1;
                        break;
                    case "O":
                        generic = 2;
                        break;
                    default:
                        generic = 0;
                        break;
                }
            }
        }

        struct RemoteLogixNode
        {
            public string classname;
            public string type;
            public List<SlotInfos> inputs;
            public List<SlotInfos> outputs;

            public RemoteLogixNode(string name)
            {
                classname = name;
                type = "Standard";
                inputs = new List<SlotInfos>(8);
                outputs = new List<SlotInfos>(8);
            }
            
        }

        static RemoteLogixNode LogiXListInputs(Type logixNodeType)
        {
            RemoteLogixNode node = new RemoteLogixNode(logixNodeType.FullName);

            foreach (FieldInfo f in logixNodeType.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public))
            {
                Type fieldType = f.FieldType;
                string fieldName = f.Name;
                switch(fieldType.Name)
                {
                    case "Input`1":
                        //Console.WriteLine("Input   : {0} ({1})", fieldName, fieldType.GetGenericArguments()[0].Name);
                        node.inputs.Add(new SlotInfos(fieldName, fieldType.GetGenericArguments()[0]));
                        break;
                    case "Output`1":
                        //Console.WriteLine("Output  : {0} ({1})", fieldName, fieldType.GetGenericArguments()[0].Name);
                        node.outputs.Add(new SlotInfos(fieldName, fieldType.GetGenericArguments()[0]));
                        break;
                    case "Impulse":
                        //Console.WriteLine("Impulse : {0} ({1})", fieldName, fieldType);
                        node.outputs.Insert(0, new SlotInfos(fieldName, fieldType));
                        break;
                }
            }

            foreach (MethodInfo m in logixNodeType.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public))
            {
                foreach (CustomAttributeData c in m.CustomAttributes)
                {
                    if (c.AttributeType == typeof(FrooxEngine.LogiX.ImpulseTarget))
                    {
                        //Console.WriteLine("Impulse Target : {0}", m.Name);
                        node.inputs.Insert(0, new SlotInfos(m.Name, typeof(FrooxEngine.LogiX.Impulse)));
                    }
                        
                }
            }

            foreach (Type t in logixNodeType.GetInterfaces())
            {
                string simpleName = t.Name;
                if (simpleName == "IElementContent`1")
                {
                    //Console.WriteLine("Output  : {0} ({1})", "*", t.GetGenericArguments()[0].Name);
                    node.outputs.Add(new SlotInfos("*", t.GetGenericArguments()[0]));
                    break;
                }
            }

            return node;
        }

        static void Main(string[] args)
        {
            List<RemoteLogixNode> nodes = new List<RemoteLogixNode>(1024);
            Type[] types = typeof(FrooxEngine.LogiX.LogixNode).Assembly.GetTypes();
            foreach (Type t in types)
            {
                string fullName = t.FullName;
                if (!fullName.StartsWith("FrooxEngine.LogiX"))
                    continue;
                
                if (!fullName.Contains("<") && !fullName.Contains("+") && !t.IsAbstract && !IgnoredTypeName(fullName))
                {
                    Console.WriteLine(fullName);
                    nodes.Add(LogiXListInputs(t));
                }
                //Console.WriteLine(fullName);  
            }
            Console.WriteLine("Hello World!");
            LogiXListInputs(typeof(FrooxEngine.LogiX.Experimental.WriteTextToFile));
            Console.WriteLine(JsonConvert.SerializeObject(nodes, Formatting.Indented));
            Console.WriteLine("Generated {0} nodes", nodes.Count);
        }
    }
}
