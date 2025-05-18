using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsXML.Helpers
{
    internal static partial class ControlHelper
    {
        public static readonly Type TypeOfControl = typeof(Control);
        public static readonly Type TypeOfForm = typeof(Form);

        private static HashSet<Type> ControlTypes = new HashSet<Type>();

        static ControlHelper()
        {
            foreach (var ass in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    LoadAssembly(ass);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Assembly `{ass}` could not load: {ex}");
                }
            }
        }

        public static void LoadAssembly(string assemblyName)
            => LoadAssembly(AppDomain.CurrentDomain.Load(assemblyName));

        public static void LoadAssembly(Assembly ass)
        {
            foreach (var type in ass.ExportedTypes)
            {
                if (type.IsAbstract || type.IsInterface) continue;

                if (TypeOfControl.IsAssignableFrom(type))
                {
                    if (!ControlTypes.Contains(type))
                    {
                        ControlTypes.Add(type);
                    }
                }
            }
        }
    }

    internal static partial class ControlHelper
    {
        public static Type ResolveControlType(string tagName, params string[] namespaces)
        {
            return
                ControlTypes.FirstOrDefault(t => t.FullName == tagName)
                ??
                namespaces.Select(ns => ControlTypes.FirstOrDefault(t => t.Namespace == ns && t.Name == tagName)).FirstOrDefault(t => t is not null);
        }

        public static Control CreateControl(string tagName, params string[] namespaces)
            => CreateControl<Control>(tagName, namespaces: namespaces);

        public static ControlType CreateControl<ControlType>(string tagName, params string[] namespaces)
        {
            if (ResolveControlType(tagName, namespaces: namespaces) is Type controlType)
            {
                return (ControlType)Activator.CreateInstance(controlType);
            }
            return default;
        }
    }

    internal static partial class ControlHelper
    {
        public static void ApplyAttribute(Control control, Type controlType, string attributeName, string attributeValue)
        {
            var prop = controlType.GetProperty(attributeName);
            if (prop == null || !prop.CanWrite)
            {
                Debug.WriteLine($"[{controlType.FullName}] Property {attributeName} not found or is not writable.");
                return;
            }

            try
            {
                object value = ConvertAttributeValue(prop.PropertyType, attributeValue);
                if (value != null)
                {
                    prop.SetValue(control, value);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[{controlType.FullName}] Error setting property {attributeName} to {attributeValue}: {ex.Message}");
            }
        }

        private static object ConvertAttributeValue(Type propertyType, object attributeValue)
        {
            if (attributeValue == null)
            {
                return null;
            }

            var converter = System.ComponentModel.TypeDescriptor.GetConverter(propertyType);

            // Use TypeConverter if available
            if (converter != null && converter.CanConvertFrom(attributeValue.GetType()))
            {
                try
                {
                    return converter.ConvertFrom(attributeValue);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"TypeConverter failed to convert value: {ex.Message}");
                }
            }

            // Fallback to Convert.ChangeType
            try
            {
                return Convert.ChangeType(attributeValue, propertyType);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Convert.ChangeType failed to convert value: {ex.Message}");
            }

            throw new InvalidOperationException($"Cannot convert value '{attributeValue}' to type '{propertyType.Name}'");
        }
    }

    internal static partial class ControlHelper
    {

    }
}
