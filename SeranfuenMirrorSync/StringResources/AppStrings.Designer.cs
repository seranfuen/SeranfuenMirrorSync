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
    public class AppStrings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AppStrings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SeranfuenMirrorSync.StringResources.AppStrings", typeof(AppStrings).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Do you wish to save the changes before closing?.
        /// </summary>
        public static string ConfirmSave_Message {
            get {
                return ResourceManager.GetString("ConfirmSave_Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Save changes.
        /// </summary>
        public static string ConfirmSave_Title {
            get {
                return ResourceManager.GetString("ConfirmSave_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The follow error occurred:
        ///
        ///{0}.
        /// </summary>
        public static string ErrorMessage {
            get {
                return ResourceManager.GetString("ErrorMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error happened.
        /// </summary>
        public static string ErrorTitle {
            get {
                return ResourceManager.GetString("ErrorTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Run sync now.
        /// </summary>
        public static string MainMenu_RunNow {
            get {
                return ResourceManager.GetString("MainMenu_RunNow", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Show Last Status.
        /// </summary>
        public static string MainMenu_ShowLastStatus {
            get {
                return ResourceManager.GetString("MainMenu_ShowLastStatus", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to conect to the sync service. Please make sure that it&apos;s running on your computer.
        /// </summary>
        public static string NoService_Error {
            get {
                return ResourceManager.GetString("NoService_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No Service Running.
        /// </summary>
        public static string NoServiceRunning {
            get {
                return ResourceManager.GetString("NoServiceRunning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Service Running.
        /// </summary>
        public static string ServiceRunning {
            get {
                return ResourceManager.GetString("ServiceRunning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The path &apos;{0}&apos; is already in the synchronization source.
        /// </summary>
        public static string SyncSourcesViewModel_AlreadyExistingPath_Message {
            get {
                return ResourceManager.GetString("SyncSourcesViewModel_AlreadyExistingPath_Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Path already exists.
        /// </summary>
        public static string SyncSourcesViewModel_AlreadyExistingPath_Title {
            get {
                return ResourceManager.GetString("SyncSourcesViewModel_AlreadyExistingPath_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An empty path cannot be added.
        /// </summary>
        public static string SyncSourcesViewModel_EmptyPath_Message {
            get {
                return ResourceManager.GetString("SyncSourcesViewModel_EmptyPath_Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Empty path.
        /// </summary>
        public static string SyncSourcesViewModel_EmptyPath_Title {
            get {
                return ResourceManager.GetString("SyncSourcesViewModel_EmptyPath_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The path &apos;{0}&apos; you are trying to add does not seem to exist. Do you stil want to add it?.
        /// </summary>
        public static string SyncSourcesViewModel_PathNotExists_Message {
            get {
                return ResourceManager.GetString("SyncSourcesViewModel_PathNotExists_Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Path not found.
        /// </summary>
        public static string SyncSourcesViewModel_PathNotExists_Title {
            get {
                return ResourceManager.GetString("SyncSourcesViewModel_PathNotExists_Title", resourceCulture);
            }
        }
    }
}
