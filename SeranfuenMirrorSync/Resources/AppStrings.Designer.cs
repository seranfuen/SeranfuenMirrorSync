﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SeranfuenMirrorSync.StringResources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class AppStrings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AppStrings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SeranfuenMirrorSync.Resources.AppStrings", typeof(AppStrings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The path &apos;{0}&apos; is already in the synchronization source.
        /// </summary>
        internal static string SyncSourcesViewModel_AlreadyExistingPath_Message {
            get {
                return ResourceManager.GetString("SyncSourcesViewModel_AlreadyExistingPath_Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Path already exists.
        /// </summary>
        internal static string SyncSourcesViewModel_AlreadyExistingPath_Title {
            get {
                return ResourceManager.GetString("SyncSourcesViewModel_AlreadyExistingPath_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An empty path cannot be added.
        /// </summary>
        internal static string SyncSourcesViewModel_EmptyPath_Message {
            get {
                return ResourceManager.GetString("SyncSourcesViewModel_EmptyPath_Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Empty path.
        /// </summary>
        internal static string SyncSourcesViewModel_EmptyPath_Title {
            get {
                return ResourceManager.GetString("SyncSourcesViewModel_EmptyPath_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The path &apos;{0}&apos; you are trying to add does not seem to exist. Do you stil want to add it?.
        /// </summary>
        internal static string SyncSourcesViewModel_PathNotExists_Message {
            get {
                return ResourceManager.GetString("SyncSourcesViewModel_PathNotExists_Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Path not found.
        /// </summary>
        internal static string SyncSourcesViewModel_PathNotExists_Title {
            get {
                return ResourceManager.GetString("SyncSourcesViewModel_PathNotExists_Title", resourceCulture);
            }
        }
    }
}