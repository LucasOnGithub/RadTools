using System;
using System.Linq;
using System.Reflection;
using Il2CppInterop.Runtime.Injection;
using RadTools.Core;

namespace RadTools.Utilities;

internal static class Il2CppRegistrar
{
    internal static void RegisterAllTypesInAssembly()
    {
        var assembly = Assembly.GetExecutingAssembly();

        var registerMethod = typeof(ClassInjector)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .First(m => m.Name == "RegisterTypeInIl2Cpp" && m.IsGenericMethod);

        foreach (var type in assembly.GetTypes())
            try
            {
                if (type.IsAbstract ||
                    type.IsInterface ||
                    type.ContainsGenericParameters ||
                    !typeof(MonoWrap).IsAssignableFrom(type))
                    continue;

                var generic = registerMethod.MakeGenericMethod(type);
                generic.Invoke(null, null);

                Console.WriteLine($"Registered MonoBehaviour: {type.FullName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to register {type.FullName}: {ex.Message}");
            }
    }
}