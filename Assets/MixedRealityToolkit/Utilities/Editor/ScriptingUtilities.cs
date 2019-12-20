﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using UnityEditor;

namespace Microsoft.MixedReality.Toolkit.Utilities.Editor
{
    /// <summary>
    /// A set of utilities to configure script compilation. 
    /// </summary>
    public static class ScriptingUtilities
    {
        /// <summary>
        /// Appends a set of symbolic constant definitions to Unity's Scripting Define Symbols for the
        /// specified build target group.
        /// </summary>
        /// <param name="targetGroup">The build target group for which the symbols are to be defined.</param>
        /// <param name="symbols">Array of symbols to define.</param>
        public static void AppendScriptingDefinitions(
            BuildTargetGroup targetGroup,
            string[] symbols)
        {
            if (symbols == null || symbols.Length == 0) { return; }

            string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);
            foreach (string symbol in symbols)
            {
                if (string.IsNullOrWhiteSpace(defines))
                {
                    defines = symbol;
                    continue;
                }

                if (!defines.Contains(symbol))
                {
                    defines = string.Concat(defines, $";{symbol}");
                }
            }

            PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup, defines);
        }

        /// <summary>
        /// Removes a set of symbolic constant definitions to Unity's Scripting Define Symbols from the
        /// specified build target group.
        /// </summary>
        /// <param name="targetGroup">The build target group for which the symbols are to be removed.</param>
        /// <param name="symbols">Array of symbols to remove.</param>
        public static void RemoveScriptingDefinitions(
            BuildTargetGroup targetGroup,
            string[] symbols)
        {
            if (symbols == null || symbols.Length == 0) { return; }
            List<string> toRemove = new List<string>(symbols);

            string[] oldDefines = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup).Split(';');
            List<string> defines = new List<string>();

            foreach (string s in oldDefines)
            {
                if (!toRemove.Contains(s))
                {
                    defines.Add(s);
                }
            }

            PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup, string.Join(";", defines.ToArray()));
        }
    }
}
