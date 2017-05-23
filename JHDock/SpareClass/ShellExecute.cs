using System;
using System.Runtime.InteropServices;
using System.Text;

namespace JHDock
{
    #region 备用类和函数（以供以后使用）
    public class ShellExecute
    {
        public enum ShowWindowCommands
        {
            SW_HIDE = 0,    // Hides the window and activates another window.
            SW_SHOWNORMAL = 1,    // Sets the show state based on the SW_ flag specified in the STARTUPINFO 
            SW_NORMAL = 1,    // structure passed to the CreateProcess function by the program that started 
            // the application.
            SW_SHOWMINIMIZED = 2,    // Activates the window and displays it as a minimized window.
            SW_SHOWMAXIMIZED = 3,    // Maximizes the specified window.
            SW_MAXIMIZE = 3,    // Activates the window and displays it as a maximized window.
            SW_SHOWNOACTIVATE = 4,    // Displays a window in its most recent size and position. The active window remains active.
            SW_SHOW = 5,    // Activates the window and displays it in its current size and position.
            SW_MINIMIZE = 6,    // Minimizes the specified window and activates the next top-level window in the z-order.
            SW_SHOWMINNOACTIVE = 7,    // Displays the window as a minimized window. The active window remains active.
            SW_SHOWNA = 8,    // Displays the window in its current state. The active window remains active.
            SW_RESTORE = 9,    // Activates and displays the window.
            SW_SHOWDEFAULT = 10,
        }

        public enum ShellExecuteReturnCodes
        {
            ERROR_OUT_OF_MEMORY = 0,    // The operating system is out of memory or resources.
            ERROR_FILE_NOT_FOUND = 2,    // The specified file was not found. 
            ERROR_PATH_NOT_FOUND = 3,    // The specified path was not found. 
            ERROR_BAD_FORMAT = 11,    // The .exe file is invalid (non-Microsoft Win32?.exe or error in .exe image). 
            SE_ERR_ACCESSDENIED = 5,    // The operating system denied access to the specified file.  
            SE_ERR_ASSOCINCOMPLETE = 27,    // The file name association is incomplete or invalid. 
            SE_ERR_DDEBUSY = 30,    // The Dynamic Data Exchange (DDE) transaction could not be completed because other DDE transactions were being processed. 
            SE_ERR_DDEFAIL = 29,    // The DDE transaction failed. 
            SE_ERR_DDETIMEOUT = 28,    // The DDE transaction could not be completed because the request timed out. 
            SE_ERR_DLLNOTFOUND = 32,    // The specified dynamic-link library (DLL) was not found.  
            SE_ERR_FNF = 2,    // The specified file was not found.  
            SE_ERR_NOASSOC = 31,    // There is no application associated with the given file name extension. This error will also be returned if you attempt to print a file that is not printable. 
            SE_ERR_OOM = 8,    // There was not enough memory to complete the operation. 
            SE_ERR_PNF = 3,    // The specified path was not found. 
            SE_ERR_SHARE = 26,    // A sharing violation occurred. 
        }

        [Flags]
        public enum ShellExecuteFlags
        {
            SEE_MASK_CLASSNAME = 0x00000001,        // Use the class name given by the lpClass member. 
            SEE_MASK_CLASSKEY = 0x00000003,        // Use the class key given by the hkeyClass member.
            SEE_MASK_IDLIST = 0x00000004,        // Use the item identifier list given by the lpIDList member. 
            // The lpIDList member must point to an ITEMIDLIST structure.
            SEE_MASK_INVOKEIDLIST = 0x0000000c,        // Use the IContextMenu interface of the selected item's 
            // shortcut menu handler.
            SEE_MASK_ICON = 0x00000010,        // Use the icon given by the hIcon member.
            SEE_MASK_HOTKEY = 0x00000020,        // Use the hot key given by the dwHotKey member.
            SEE_MASK_NOCLOSEPROCESS = 0x00000040,        // Use to indicate that the hProcess member receives the 
            // process handle. 
            SEE_MASK_CONNECTNETDRV = 0x00000080,        // Validate the share and connect to a drive letter.
            SEE_MASK_FLAG_DDEWAIT = 0x00000100,        // Wait for the Dynamic Data Exchange (DDE) conversation to 
            // terminate before returning
            SEE_MASK_DOENVSUBST = 0x00000200,        // Expand any environment variables specified in the string 
            // given by the lpDirectory or lpFile member. 
            SEE_MASK_FLAG_NO_UI = 0x00000400,        // Do not display an error message box if an error occurs. 
            SEE_MASK_UNICODE = 0x00004000,        // Use this flag to indicate a Unicode application.
            SEE_MASK_NO_CONSOLE = 0x00008000,        // Use to create a console for the new process instead of 
            // having it inherit the parent's console.
            SEE_MASK_ASYNCOK = 0x00100000,
            SEE_MASK_HMONITOR = 0x00200000,        // Use this flag when specifying a monitor on 
            // multi-monitor systems.
            SEE_MASK_NOQUERYCLASSSTORE = 0x01000000,
            SEE_MASK_WAITFORINPUTIDLE = 0x02000000,
            SEE_MASK_FLAG_LOG_USAGE = 0x04000000        // Keep track of the number of times this application has 
            // been launched. 
        }


        // Common verbs
        public const string OpenFile = "open";        // Opens the file specified by the lpFile parameter. 
        // The file can be an executable file, a document file, 
        // or a folder.
        public const string EditFile = "edit";        // Launches an editor and opens the document for editing.
        // If lpFile is not a document file, the function 
        // will fail.
        public const string ExploreFolder = "explore";    // Explores the folder specified by lpFile.
        public const string FindInFolder = "find";        // Initiates a search starting from the specified 
        // directory.
        public const string PrintFile = "print";        // Prints the document file specified by lpFile. If 
        // lpFile is not a document file, the function will fail.


        // properties
        public IntPtr OwnerHandle;            // Handle to the owner window
        public string Verb;                    // The requested operation to make on the file
        public string Path;                    // String that specifies the file or object on which to execute the 
        // specified verb.
        public string Parameters;            // String that specifies the parameters to be passed to the application.
        public string WorkingFolder;        // pecifies the default directory
        public ShowWindowCommands ShowMode;    // Flags that specify how an application is to be displayed 
        // when it is opened.

        public ShellExecute()
        {
            // Set default values
            OwnerHandle = IntPtr.Zero;
            Verb = OpenFile;
            Path = "";
            Parameters = "";
            WorkingFolder = "";
            ShowMode = ShowWindowCommands.SW_SHOWNORMAL;
        }

        public bool Execute()
        {
            int iRetVal;
            iRetVal = (int)JHDock.ShellApi.ShellExecute(OwnerHandle, Verb, Path, Parameters, WorkingFolder, (int)ShowMode);
            return (iRetVal > 32) ? true : false;
        }


    }
    public class ShellFileOperation
    {
        public enum FileOperations
        {
            FO_MOVE = 0x0001,        // Move the files specified in pFrom to the location specified in pTo. 
            FO_COPY = 0x0002,        // Copy the files specified in the pFrom member to the location specified 
            // in the pTo member. 
            FO_DELETE = 0x0003,        // Delete the files specified in pFrom. 
            FO_RENAME = 0x0004        // Rename the file specified in pFrom. You cannot use this flag to rename 
            // multiple files with a single function call. Use FO_MOVE instead. 
        }

        [Flags]
        public enum ShellFileOperationFlags
        {
            FOF_MULTIDESTFILES = 0x0001,    // The pTo member specifies multiple destination files (one for 
            // each source file) rather than one directory where all source 
            // files are to be deposited. 
            FOF_CONFIRMMOUSE = 0x0002,    // Not currently used. 
            FOF_SILENT = 0x0004,    // Do not display a progress dialog box. 
            FOF_RENAMEONCOLLISION = 0x0008,    // Give the file being operated on a new name in a move, copy, or 
            // rename operation if a file with the target name already exists. 
            FOF_NOCONFIRMATION = 0x0010,    // Respond with "Yes to All" for any dialog box that is displayed. 
            FOF_WANTMAPPINGHANDLE = 0x0020,    // If FOF_RENAMEONCOLLISION is specified and any files were renamed,
            // assign a name mapping object containing their old and new names 
            // to the hNameMappings member.
            FOF_ALLOWUNDO = 0x0040,    // Preserve Undo information, if possible. If pFrom does not 
            // contain fully qualified path and file names, this flag is ignored. 
            FOF_FILESONLY = 0x0080,    // Perform the operation on files only if a wildcard file 
            // name (*.*) is specified. 
            FOF_SIMPLEPROGRESS = 0x0100,    // Display a progress dialog box but do not show the file names. 
            FOF_NOCONFIRMMKDIR = 0x0200,    // Do not confirm the creation of a new directory if the operation
            // requires one to be created. 
            FOF_NOERRORUI = 0x0400,    // Do not display a user interface if an error occurs. 
            FOF_NOCOPYSECURITYATTRIBS = 0x0800,    // Do not copy the security attributes of the file.
            FOF_NORECURSION = 0x1000,    // Only operate in the local directory. Don't operate recursively
            // into subdirectories.
            FOF_NO_CONNECTED_ELEMENTS = 0x2000,    // Do not move connected files as a group. Only move the 
            // specified files. 
            FOF_WANTNUKEWARNING = 0x4000,    // Send a warning if a file is being destroyed during a delete 
            // operation rather than recycled. This flag partially 
            // overrides FOF_NOCONFIRMATION.
            FOF_NORECURSEREPARSE = 0x8000        // Treat reparse points as objects, not containers.

        }

        [Flags]
        public enum ShellChangeNotificationEvents : uint
        {
            SHCNE_RENAMEITEM = 0x00000001,    // The name of a nonfolder item has changed. SHCNF_IDLIST or 
            // SHCNF_PATH must be specified in uFlags. dwItem1 contains the 
            // previous PIDL or name of the item. dwItem2 contains the new PIDL
            // or name of the item. 
            SHCNE_CREATE = 0x00000002,    // A nonfolder item has been created. SHCNF_IDLIST or SHCNF_PATH 
            // must be specified in uFlags. dwItem1 contains the item that was 
            // created. dwItem2 is not used and should be NULL. 
            SHCNE_DELETE = 0x00000004,    // A nonfolder item has been deleted. SHCNF_IDLIST or SHCNF_PATH
            // must be specified in uFlags. dwItem1 contains the item that was 
            // deleted. dwItem2 is not used and should be NULL. 
            SHCNE_MKDIR = 0x00000008,    // A folder has been created. SHCNF_IDLIST or SHCNF_PATH must be 
            // specified in uFlags. dwItem1 contains the folder that was 
            // created. dwItem2 is not used and should be NULL. 
            SHCNE_RMDIR = 0x00000010,    // A folder has been removed. SHCNF_IDLIST or SHCNF_PATH must be 
            // specified in uFlags. dwItem1 contains the folder that was 
            // removed. dwItem2 is not used and should be NULL. 
            SHCNE_MEDIAINSERTED = 0x00000020,    // Storage media has been inserted into a drive. SHCNF_IDLIST or
            // SHCNF_PATH must be specified in uFlags. dwItem1 contains the root
            // of the drive that contains the new media. dwItem2 is not used 
            // and should be NULL. 
            SHCNE_MEDIAREMOVED = 0x00000040,    // Storage media has been removed from a drive. SHCNF_IDLIST or 
            // SHCNF_PATH must be specified in uFlags. dwItem1 contains the root
            // of the drive from which the media was removed. dwItem2 is not 
            // used and should be NULL. 
            SHCNE_DRIVEREMOVED = 0x00000080,    // A drive has been removed. SHCNF_IDLIST or SHCNF_PATH must be 
            // specified in uFlags. dwItem1 contains the root of the drive that
            // was removed. dwItem2 is not used and should be NULL. 
            SHCNE_DRIVEADD = 0x00000100,    // A drive has been added. SHCNF_IDLIST or SHCNF_PATH must be 
            // specified in uFlags. dwItem1 contains the root of the drive that
            // was added. dwItem2 is not used and should be NULL. 
            SHCNE_NETSHARE = 0x00000200,    // A folder on the local computer is being shared via the network.
            // SHCNF_IDLIST or SHCNF_PATH must be specified in uFlags. dwItem1
            // contains the folder that is being shared. dwItem2 is not used and
            // should be NULL. 
            SHCNE_NETUNSHARE = 0x00000400,    // A folder on the local computer is no longer being shared via the
            // network. SHCNF_IDLIST or SHCNF_PATH must be specified in uFlags. 
            // dwItem1 contains the folder that is no longer being shared. 
            // dwItem2 is not used and should be NULL. 
            SHCNE_ATTRIBUTES = 0x00000800,    // The attributes of an item or folder have changed. SHCNF_IDLIST
            // or SHCNF_PATH must be specified in uFlags. dwItem1 contains the
            // item or folder that has changed. dwItem2 is not used and should
            // be NULL. 
            SHCNE_UPDATEDIR = 0x00001000,    // The contents of an existing folder have changed, but the folder
            // still exists and has not been renamed. SHCNF_IDLIST or SHCNF_PATH
            // must be specified in uFlags. dwItem1 contains the folder that 
            // has changed. dwItem2 is not used and should be NULL. If a folder
            // has been created, deleted, or renamed, use SHCNE_MKDIR, 
            // SHCNE_RMDIR, or SHCNE_RENAMEFOLDER, respectively, instead. 
            SHCNE_UPDATEITEM = 0x00002000,    // An existing nonfolder item has changed, but the item still exists
            // and has not been renamed. SHCNF_IDLIST or SHCNF_PATH must be 
            // specified in uFlags. dwItem1 contains the item that has changed.
            // dwItem2 is not used and should be NULL. If a nonfolder item has 
            // been created, deleted, or renamed, use SHCNE_CREATE, 
            // SHCNE_DELETE, or SHCNE_RENAMEITEM, respectively, instead. 
            SHCNE_SERVERDISCONNECT = 0x00004000,    // The computer has disconnected from a server. SHCNF_IDLIST or 
            // SHCNF_PATH must be specified in uFlags. dwItem1 contains the 
            // server from which the computer was disconnected. dwItem2 is not
            // used and should be NULL.
            SHCNE_UPDATEIMAGE = 0x00008000,    // An image in the system image list has changed. SHCNF_DWORD must be 
            // specified in uFlags. dwItem1 contains the index in the system image 
            // list that has changed. dwItem2 is not used and should be NULL. 
            SHCNE_DRIVEADDGUI = 0x00010000,    // A drive has been added and the Shell should create a new window
            // for the drive. SHCNF_IDLIST or SHCNF_PATH must be specified in 
            // uFlags. dwItem1 contains the root of the drive that was added. 
            // dwItem2 is not used and should be NULL. 
            SHCNE_RENAMEFOLDER = 0x00020000,    // The name of a folder has changed. SHCNF_IDLIST or SHCNF_PATH must
            // be specified in uFlags. dwItem1 contains the previous pointer to
            // an item identifier list (PIDL) or name of the folder. dwItem2 
            // contains the new PIDL or name of the folder. 
            SHCNE_FREESPACE = 0x00040000,    // The amount of free space on a drive has changed. SHCNF_IDLIST or
            // SHCNF_PATH must be specified in uFlags. dwItem1 contains the root
            // of the drive on which the free space changed. dwItem2 is not used
            // and should be NULL. 
            SHCNE_EXTENDED_EVENT = 0x04000000,    // Not currently used. 
            SHCNE_ASSOCCHANGED = 0x08000000,    // A file type association has changed. SHCNF_IDLIST must be 
            // specified in the uFlags parameter. dwItem1 and dwItem2 are not
            // used and must be NULL. 
            SHCNE_DISKEVENTS = 0x0002381F,    // Specifies a combination of all of the disk event identifiers. 
            SHCNE_GLOBALEVENTS = 0x0C0581E0,    // Specifies a combination of all of the global event identifiers. 
            SHCNE_ALLEVENTS = 0x7FFFFFFF,    // All events have occurred. 
            SHCNE_INTERRUPT = 0x80000000    // The specified event occurred as a result of a system interrupt.
            // As this value modifies other event values, it cannot be used alone.
        }


        public enum ShellChangeNotificationFlags
        {
            SHCNF_IDLIST = 0x0000,    // dwItem1 and dwItem2 are the addresses of ITEMIDLIST structures that
            // represent the item(s) affected by the change. Each ITEMIDLIST must be 
            // relative to the desktop folder. 
            SHCNF_PATHA = 0x0001,    // dwItem1 and dwItem2 are the addresses of null-terminated strings of 
            // maximum length MAX_PATH that contain the full path names of the items 
            // affected by the change. 
            SHCNF_PRINTERA = 0x0002,    // dwItem1 and dwItem2 are the addresses of null-terminated strings that 
            // represent the friendly names of the printer(s) affected by the change. 
            SHCNF_DWORD = 0x0003,    // The dwItem1 and dwItem2 parameters are DWORD values. 
            SHCNF_PATHW = 0x0005,    // like SHCNF_PATHA but unicode string
            SHCNF_PRINTERW = 0x0006,    // like SHCNF_PRINTERA but unicode string
            SHCNF_TYPE = 0x00FF,
            SHCNF_FLUSH = 0x1000,    // The function should not return until the notification has been delivered 
            // to all affected components. As this flag modifies other data-type flags,
            // it cannot by used by itself.
            SHCNF_FLUSHNOWAIT = 0x2000    // The function should begin delivering notifications to all affected 
            // components but should return as soon as the notification process has
            // begun. As this flag modifies other data-type flags, it cannot by used 
            // by itself.
        }

        // properties
        public FileOperations Operation;
        public IntPtr OwnerWindow;
        public ShellFileOperationFlags OperationFlags;
        public String ProgressTitle;
        public String[] SourceFiles;
        public String[] DestFiles;

        public ShellFileOperation()
        {
            // set default properties
            Operation = FileOperations.FO_COPY;
            OwnerWindow = IntPtr.Zero;
            OperationFlags = ShellFileOperationFlags.FOF_ALLOWUNDO
                | ShellFileOperationFlags.FOF_MULTIDESTFILES
                | ShellFileOperationFlags.FOF_NO_CONNECTED_ELEMENTS
                | ShellFileOperationFlags.FOF_WANTNUKEWARNING;
            ProgressTitle = "";
        }

        public bool DoOperation()
        {
            ShellApi.SHFILEOPSTRUCT FileOpStruct = new ShellApi.SHFILEOPSTRUCT();

            FileOpStruct.hwnd = OwnerWindow;
            FileOpStruct.wFunc = (uint)Operation;

            String multiSource = StringArrayToMultiString(SourceFiles);
            String multiDest = StringArrayToMultiString(DestFiles);
            FileOpStruct.pFrom = Marshal.StringToHGlobalUni(multiSource);
            FileOpStruct.pTo = Marshal.StringToHGlobalUni(multiDest);

            FileOpStruct.fFlags = (ushort)OperationFlags;
            FileOpStruct.lpszProgressTitle = ProgressTitle;
            FileOpStruct.fAnyOperationsAborted = 0;
            FileOpStruct.hNameMappings = IntPtr.Zero;

            int RetVal;
            RetVal = ShellApi.SHFileOperation(ref FileOpStruct);

            ShellApi.SHChangeNotify(
                (uint)ShellChangeNotificationEvents.SHCNE_ALLEVENTS,
                (uint)ShellChangeNotificationFlags.SHCNF_DWORD,
                IntPtr.Zero,
                IntPtr.Zero);

            if (RetVal != 0)
                return false;

            if (FileOpStruct.fAnyOperationsAborted != 0)
                return false;

            return true;
        }

        private String StringArrayToMultiString(String[] stringArray)
        {
            String multiString = "";

            if (stringArray == null)
                return "";

            for (int i = 0; i < stringArray.Length; i++)
                multiString += stringArray[i] + '\0';

            multiString += '\0';

            return multiString;
        }
    }
    public class ShellFunctions
    {
        public static IMalloc GetMalloc()
        {
            IntPtr ptrRet;
            ShellApi.SHGetMalloc(out ptrRet);

            Object obj = Marshal.GetTypedObjectForIUnknown(ptrRet, GetMallocType());
            IMalloc imalloc = (IMalloc)obj;

            return imalloc;
        }

        public static IShellFolder GetDesktopFolder()
        {
            IntPtr ptrRet;
            ShellApi.SHGetDesktopFolder(out ptrRet);

            System.Type shellFolderType = System.Type.GetType("JHDock.IShellFolder");
            Object obj = Marshal.GetTypedObjectForIUnknown(ptrRet, shellFolderType);
            IShellFolder ishellFolder = (IShellFolder)obj;

            return ishellFolder;
        }

        public static Type GetShellFolderType()
        {
            System.Type shellFolderType = System.Type.GetType("JHDock.IShellFolder");
            return shellFolderType;
        }

        public static Type GetMallocType()
        {
            System.Type mallocType = System.Type.GetType("JHDock.IMalloc");
            return mallocType;
        }

        public static Type GetFolderFilterType()
        {
            System.Type folderFilterType = System.Type.GetType("JHDock.IFolderFilter");
            return folderFilterType;
        }

        public static Type GetFolderFilterSiteType()
        {
            System.Type folderFilterSiteType = System.Type.GetType("JHDock.IFolderFilterSite");
            return folderFilterSiteType;
        }

        public static IShellFolder GetShellFolder(IntPtr ptrShellFolder)
        {
            System.Type shellFolderType = GetShellFolderType();
            Object obj = Marshal.GetTypedObjectForIUnknown(ptrShellFolder, shellFolderType);
            IShellFolder RetVal = (IShellFolder)obj;
            return RetVal;
        }

    }
    public class ShellAddRecent
    {
        public enum ShellAddRecentDocs
        {
            SHARD_PIDL = 0x00000001,    // The pv parameter points to a null-terminated string with the path 
            // and file name of the object.
            SHARD_PATHA = 0x00000002,    // The pv parameter points to a pointer to an item identifier list 
            // (PIDL) that identifies the document's file object. PIDLs that 
            // identify nonfile objects are not allowed.
            SHARD_PATHW = 0x00000003    // same as SHARD_PATHA but unicode string
        }

        public static void AddToList(String path)
        {
            ShellApi.SHAddToRecentDocs((uint)ShellAddRecentDocs.SHARD_PATHW, path);
        }

        public static void ClearList()
        {
            ShellApi.SHAddToRecentDocs((uint)ShellAddRecentDocs.SHARD_PIDL, IntPtr.Zero);
        }
    }
    public class ShellApi
    {
        public delegate Int32 BrowseCallbackProc(IntPtr hwnd, UInt32 uMsg, Int32 lParam, Int32 lpData);

        // Contains parameters for the SHBrowseForFolder function and receives information about the folder selected 
        // by the user.
        [StructLayout(LayoutKind.Sequential)]
        public struct BROWSEINFO
        {
            public IntPtr hwndOwner;                // Handle to the owner window for the dialog box.

            public IntPtr pidlRoot;                    // Pointer to an item identifier list (PIDL) specifying the 
            // location of the root folder from which to start browsing.

            [MarshalAs(UnmanagedType.LPStr)]        // Address of a buffer to receive the display name of the 
            public String pszDisplayName;            // folder selected by the user.

            [MarshalAs(UnmanagedType.LPStr)]        // Address of a null-terminated string that is displayed 
            public String lpszTitle;                // above the tree view control in the dialog box.

            public UInt32 ulFlags;                    // Flags specifying the options for the dialog box. 

            [MarshalAs(UnmanagedType.FunctionPtr)]    // Address of an application-defined function that the 
            public BrowseCallbackProc lpfn;            // dialog box calls when an event occurs.

            public Int32 lParam;                    // Application-defined value that the dialog box passes to 
            // the callback function

            public Int32 iImage;                    // Variable to receive the image associated with the selected folder.
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct STRRET
        {
            [FieldOffset(0)]
            public UInt32 uType;                        // One of the STRRET_* values

            [FieldOffset(4)]
            public IntPtr pOleStr;                        // must be freed by caller of GetDisplayNameOf

            [FieldOffset(4)]
            public IntPtr pStr;                            // NOT USED

            [FieldOffset(4)]
            public UInt32 uOffset;                        // Offset into SHITEMID

            [FieldOffset(4)]
            public IntPtr cStr;                            // Buffer to fill in (ANSI)
        }

        // Contains information used by ShellExecuteEx
        [StructLayout(LayoutKind.Sequential)]
        public struct SHELLEXECUTEINFO
        {
            public UInt32 cbSize;                    // Size of the structure, in bytes. 
            public UInt32 fMask;                    // Array of flags that indicate the content and validity of the 
            // other structure members.
            public IntPtr hwnd;                        // Window handle to any message boxes that the system might produce
            // while executing this function. 
            [MarshalAs(UnmanagedType.LPWStr)]
            public String lpVerb;                    // String, referred to as a verb, that specifies the action to 
            // be performed. 
            [MarshalAs(UnmanagedType.LPWStr)]
            public String lpFile;                    // Address of a null-terminated string that specifies the name of 
            // the file or object on which ShellExecuteEx will perform the 
            // action specified by the lpVerb parameter.
            [MarshalAs(UnmanagedType.LPWStr)]
            public String lpParameters;                // Address of a null-terminated string that contains the 
            // application parameters.
            [MarshalAs(UnmanagedType.LPWStr)]
            public String lpDirectory;                // Address of a null-terminated string that specifies the name of 
            // the working directory. 
            public Int32 nShow;                        // Flags that specify how an application is to be shown when it 
            // is opened.
            public IntPtr hInstApp;                    // If the function succeeds, it sets this member to a value 
            // greater than 32.
            public IntPtr lpIDList;                    // Address of an ITEMIDLIST structure to contain an item identifier
            // list uniquely identifying the file to execute.
            [MarshalAs(UnmanagedType.LPWStr)]
            public String lpClass;                    // Address of a null-terminated string that specifies the name of 
            // a file class or a globally unique identifier (GUID). 
            public IntPtr hkeyClass;                // Handle to the registry key for the file class.
            public UInt32 dwHotKey;                    // Hot key to associate with the application.
            public IntPtr hIconMonitor;                // Handle to the icon for the file class. OR Handle to the monitor 
            // upon which the document is to be displayed. 
            public IntPtr hProcess;                    // Handle to the newly started application.
        }

        // Contains information that the SHFileOperation function uses to perform file operations. 
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct SHFILEOPSTRUCT
        {
            public IntPtr hwnd;                        // Window handle to the dialog box to display information about the 
            // status of the file operation. 
            public UInt32 wFunc;                    // Value that indicates which operation to perform.
            public IntPtr pFrom;                    // Address of a buffer to specify one or more source file names. 
            // These names must be fully qualified paths. Standard Microsoft?
            // MS-DOS?wild cards, such as "*", are permitted in the file-name 
            // position. Although this member is declared as a null-terminated 
            // string, it is used as a buffer to hold multiple file names. Each 
            // file name must be terminated by a single NULL character. An    
            // additional NULL character must be appended to the end of the 
            // final name to indicate the end of pFrom. 
            public IntPtr pTo;                        // Address of a buffer to contain the name of the destination file or 
            // directory. This parameter must be set to NULL if it is not used.
            // Like pFrom, the pTo member is also a double-null terminated 
            // string and is handled in much the same way. 
            public UInt16 fFlags;                    // Flags that control the file operation. 
            public Int32 fAnyOperationsAborted;        // Value that receives TRUE if the user aborted any file operations
            // before they were completed, or FALSE otherwise. 
            public IntPtr hNameMappings;            // A handle to a name mapping object containing the old and new 
            // names of the renamed files. This member is used only if the 
            // fFlags member includes the FOF_WANTMAPPINGHANDLE flag.
            [MarshalAs(UnmanagedType.LPWStr)]
            public String lpszProgressTitle;        // Address of a string to use as the title of a progress dialog box.
            // This member is used only if fFlags includes the 
            // FOF_SIMPLEPROGRESS flag.
        }




        // Retrieves a pointer to the Shell's IMalloc interface.
        [DllImport("shell32.dll")]
        public static extern Int32 SHGetMalloc(
            out IntPtr hObject);    // Address of a pointer that receives the Shell's IMalloc interface pointer. 

        // Retrieves the path of a folder as an PIDL.
        [DllImport("shell32.dll")]
        public static extern Int32 SHGetFolderLocation(
            IntPtr hwndOwner,        // Handle to the owner window.
            Int32 nFolder,            // A CSIDL value that identifies the folder to be located.
            IntPtr hToken,            // Token that can be used to represent a particular user.
            UInt32 dwReserved,        // Reserved.
            out IntPtr ppidl);        // Address of a pointer to an item identifier list structure 
        // specifying the folder's location relative to the root of the namespace 
        // (the desktop). 

        // Converts an item identifier list to a file system path. 
        [DllImport("shell32.dll")]
        public static extern Int32 SHGetPathFromIDList(
            IntPtr pidl,            // Address of an item identifier list that specifies a file or directory location 
            // relative to the root of the namespace (the desktop). 
            StringBuilder pszPath);    // Address of a buffer to receive the file system path.


        // Takes the CSIDL of a folder and returns the pathname.
        [DllImport("shell32.dll")]
        public static extern Int32 SHGetFolderPath(
            IntPtr hwndOwner,            // Handle to an owner window.
            Int32 nFolder,                // A CSIDL value that identifies the folder whose path is to be retrieved.
            IntPtr hToken,                // An access token that can be used to represent a particular user.
            UInt32 dwFlags,                // Flags to specify which path is to be returned. It is used for cases where 
            // the folder associated with a CSIDL may be moved or renamed by the user. 
            StringBuilder pszPath);        // Pointer to a null-terminated string which will receive the path.

        // Translates a Shell namespace object's display name into an item identifier list and returns the attributes 
        // of the object. This function is the preferred method to convert a string to a pointer to an item 
        // identifier list (PIDL). 
        [DllImport("shell32.dll")]
        public static extern Int32 SHParseDisplayName(
            [MarshalAs(UnmanagedType.LPWStr)]
            String pszName,                // Pointer to a zero-terminated wide string that contains the display name 
            // to parse. 
            IntPtr pbc,                    // Optional bind context that controls the parsing operation. This parameter 
            // is normally set to NULL.
            out IntPtr ppidl,            // Address of a pointer to a variable of type ITEMIDLIST that receives the item
            // identifier list for the object.
            UInt32 sfgaoIn,                // ULONG value that specifies the attributes to query.
            out UInt32 psfgaoOut);        // Pointer to a ULONG. On return, those attributes that are true for the 
        // object and were requested in sfgaoIn will be set. 


        // Retrieves the IShellFolder interface for the desktop folder, which is the root of the Shell's namespace. 
        [DllImport("shell32.dll")]
        public static extern Int32 SHGetDesktopFolder(
            out IntPtr ppshf);            // Address that receives an IShellFolder interface pointer for the 
        // desktop folder.

        // This function takes the fully-qualified pointer to an item identifier list (PIDL) of a namespace object, 
        // and returns a specified interface pointer on the parent object.
        [DllImport("shell32.dll")]
        public static extern Int32 SHBindToParent(
            IntPtr pidl,            // The item's PIDL. 
            [MarshalAs(UnmanagedType.LPStruct)]
            Guid riid,                // The REFIID of one of the interfaces exposed by the item's parent object. 
            out IntPtr ppv,            // A pointer to the interface specified by riid. You must release the object when 
            // you are finished. 
            ref IntPtr ppidlLast);    // The item's PIDL relative to the parent folder. This PIDL can be used with many
        // of the methods supported by the parent folder's interfaces. If you set ppidlLast 
        // to NULL, the PIDL will not be returned. 

        // Accepts a STRRET structure returned by IShellFolder::GetDisplayNameOf that contains or points to a 
        // string, and then returns that string as a BSTR.
        [DllImport("shlwapi.dll")]
        public static extern Int32 StrRetToBSTR(
            ref STRRET pstr,        // Pointer to a STRRET structure.
            IntPtr pidl,            // Pointer to an ITEMIDLIST uniquely identifying a file object or subfolder relative
            // to the parent folder.
            [MarshalAs(UnmanagedType.BStr)]
            out String pbstr);        // Pointer to a variable of type BSTR that contains the converted string.

        // Takes a STRRET structure returned by IShellFolder::GetDisplayNameOf, converts it to a string, and 
        // places the result in a buffer. 
        [DllImport("shlwapi.dll")]
        public static extern Int32 StrRetToBuf(
            ref STRRET pstr,        // Pointer to the STRRET structure. When the function returns, this pointer will no
            // longer be valid.
            IntPtr pidl,            // Pointer to the item's ITEMIDLIST structure.
            StringBuilder pszBuf,    // Buffer to hold the display name. It will be returned as a null-terminated
            // string. If cchBuf is too small, the name will be truncated to fit. 
            UInt32 cchBuf);            // Size of pszBuf, in characters. If cchBuf is too small, the string will be 
        // truncated to fit. 



        // Displays a dialog box that enables the user to select a Shell folder. 
        [DllImport("shell32.dll")]
        public static extern IntPtr SHBrowseForFolder(
            ref BROWSEINFO lbpi);    // Pointer to a BROWSEINFO structure that contains information used to display 
        // the dialog box. 

        // Performs an operation on a specified file.
        [DllImport("shell32.dll")]
        public static extern IntPtr ShellExecute(
            IntPtr hwnd,            // Handle to a parent window.
            [MarshalAs(UnmanagedType.LPStr)]
            String lpOperation,        // Pointer to a null-terminated string, referred to in this case as a verb, 
            // that specifies the action to be performed.
            [MarshalAs(UnmanagedType.LPStr)]
            String lpFile,            // Pointer to a null-terminated string that specifies the file or object on which 
            // to execute the specified verb.
            [MarshalAs(UnmanagedType.LPStr)]
            String lpParameters,    // If the lpFile parameter specifies an executable file, lpParameters is a pointer 
            // to a null-terminated string that specifies the parameters to be passed 
            // to the application.
            [MarshalAs(UnmanagedType.LPStr)]
            String lpDirectory,        // Pointer to a null-terminated string that specifies the default directory. 
            Int32 nShowCmd);        // Flags that specify how an application is to be displayed when it is opened.

        // Performs an action on a file. 
        [DllImport("shell32.dll")]
        public static extern Int32 ShellExecuteEx(
            ref SHELLEXECUTEINFO lpExecInfo);    // Address of a SHELLEXECUTEINFO structure that contains and receives 
        // information about the application being executed. 

        // Copies, moves, renames, or deletes a file system object. 
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern Int32 SHFileOperation(
            ref SHFILEOPSTRUCT lpFileOp);        // Address of an SHFILEOPSTRUCT structure that contains information 
        // this function needs to carry out the specified operation. This 
        // parameter must contain a valid value that is not NULL. You are 
        // responsibile for validating the value. If you do not validate it, 
        // you will experience unexpected results. 

        // Notifies the system of an event that an application has performed. An application should use this function
        // if it performs an action that may affect the Shell. 
        [DllImport("shell32.dll")]
        public static extern void SHChangeNotify(
            UInt32 wEventId,                // Describes the event that has occurred. the 
            // ShellChangeNotificationEvents enum contains a list of options.
            UInt32 uFlags,                    // Flags that indicate the meaning of the dwItem1 and dwItem2 parameters.
            IntPtr dwItem1,                    // First event-dependent value. 
            IntPtr dwItem2);                // Second event-dependent value. 

        // Adds a document to the Shell's list of recently used documents or clears all documents from the list. 
        [DllImport("shell32.dll")]
        public static extern void SHAddToRecentDocs(
            UInt32 uFlags,                    // Flag that indicates the meaning of the pv parameter.
            IntPtr pv);                        // A pointer to either a null-terminated string with the path and file name 
        // of the document, or a PIDL that identifies the document's file object. 
        // Set this parameter to NULL to clear all documents from the list. 
        [DllImport("shell32.dll")]
        public static extern void SHAddToRecentDocs(
            UInt32 uFlags,
            [MarshalAs(UnmanagedType.LPWStr)]
            String pv);

        // Executes a command on a printer object. 
        [DllImport("shell32.dll")]
        public static extern Int32 SHInvokePrinterCommand(
            IntPtr hwnd,                        // Handle of the window that will be used as the parent of any windows 
            // or dialog boxes that are created during the operation.
            UInt32 uAction,                        // A value that determines the type of printer operation that will be 
            // performed.
            [MarshalAs(UnmanagedType.LPWStr)]
            String lpBuf1,                        // Address of a null_terminated string that contains additional 
            // information for the printer command. 
            [MarshalAs(UnmanagedType.LPWStr)]    
            String lpBuf2,                        // Address of a null-terminated string that contains additional
            // information for the printer command. 
            Int32 fModal);                        //  value that determines whether SHInvokePrinterCommand should return
        // after initializing the command or wait until the command is completed.


        // Sends an appbar message to the system. 
        // Address of an APPBARDATA structure. The content of the structure 
        // depends on the value set in the dwMessage parameter. 


        // The RegisterWindowMessage function defines a new window message that is guaranteed to be unique throughout 
        // the system. The message value can be used when sending or posting messages. 
        [DllImport("user32.dll")]
        public static extern UInt32 RegisterWindowMessage([MarshalAs(UnmanagedType.LPTStr)]String lpString);
        // Pointer to a null-terminated string that specifies the message to be registered.     

        public static Int16 GetHResultCode(Int32 hr)
        {
            hr = hr & 0x0000ffff;
            return (Int16)hr;
        }


        public enum CSIDL
        {
            CSIDL_FLAG_CREATE = (0x8000),    // Version 5.0. Combine this CSIDL with any of the following 
            //CSIDLs to force the creation of the associated folder. 
            CSIDL_ADMINTOOLS = (0x0030),    // Version 5.0. The file system directory that is used to store 
            // administrative tools for an individual user. The Microsoft 
            // Management Console (MMC) will save customized consoles to 
            // this directory, and it will roam with the user.
            CSIDL_ALTSTARTUP = (0x001d),    // The file system directory that corresponds to the user's 
            // nonlocalized Startup program group.
            CSIDL_APPDATA = (0x001a),    // Version 4.71. The file system directory that serves as a 
            // common repository for application-specific data. A typical
            // path is C:\Documents and Settings\username\Application Data. 
            // This CSIDL is supported by the redistributable Shfolder.dll 
            // for systems that do not have the Microsoft?Internet 
            // Explorer 4.0 integrated Shell installed.
            CSIDL_BITBUCKET = (0x000a),    // The virtual folder containing the objects in the user's 
            // Recycle Bin.
            CSIDL_CDBURN_AREA = (0x003b),    // Version 6.0. The file system directory acting as a staging
            // area for files waiting to be written to CD. A typical path 
            // is C:\Documents and Settings\username\Local Settings\
            // Application Data\Microsoft\CD Burning.
            CSIDL_COMMON_ADMINTOOLS = (0x002f),    // Version 5.0. The file system directory containing 
            // administrative tools for all users of the computer.
            CSIDL_COMMON_ALTSTARTUP = (0x001e), // The file system directory that corresponds to the 
            // nonlocalized Startup program group for all users. Valid only 
            // for Microsoft Windows NT?systems.
            CSIDL_COMMON_APPDATA = (0x0023), // Version 5.0. The file system directory containing application 
            // data for all users. A typical path is C:\Documents and 
            // Settings\All Users\Application Data.
            CSIDL_COMMON_DESKTOPDIRECTORY = (0x0019), // The file system directory that contains files and folders 
            // that appear on the desktop for all users. A typical path is 
            // C:\Documents and Settings\All Users\Desktop. Valid only for 
            // Windows NT systems.
            CSIDL_COMMON_DOCUMENTS = (0x002e), // The file system directory that contains documents that are 
            // common to all users. A typical paths is C:\Documents and 
            // Settings\All Users\Documents. Valid for Windows NT systems 
            // and Microsoft Windows?95 and Windows 98 systems with 
            // Shfolder.dll installed.
            CSIDL_COMMON_FAVORITES = (0x001f), // The file system directory that serves as a common repository
            // for favorite items common to all users. Valid only for 
            // Windows NT systems.
            CSIDL_COMMON_MUSIC = (0x0035), // Version 6.0. The file system directory that serves as a 
            // repository for music files common to all users. A typical 
            // path is C:\Documents and Settings\All Users\Documents\
            // My Music.
            CSIDL_COMMON_PICTURES = (0x0036), // Version 6.0. The file system directory that serves as a 
            // repository for image files common to all users. A typical 
            // path is C:\Documents and Settings\All Users\Documents\
            // My Pictures.
            CSIDL_COMMON_PROGRAMS = (0x0017), // The file system directory that contains the directories for 
            // the common program groups that appear on the Start menu for
            // all users. A typical path is C:\Documents and Settings\
            // All Users\Start Menu\Programs. Valid only for Windows NT 
            // systems.
            CSIDL_COMMON_STARTMENU = (0x0016), // The file system directory that contains the programs and 
            // folders that appear on the Start menu for all users. A 
            // typical path is C:\Documents and Settings\All Users\
            // Start Menu. Valid only for Windows NT systems.
            CSIDL_COMMON_STARTUP = (0x0018), // The file system directory that contains the programs that 
            // appear in the Startup folder for all users. A typical path 
            // is C:\Documents and Settings\All Users\Start Menu\Programs\
            // Startup. Valid only for Windows NT systems.
            CSIDL_COMMON_TEMPLATES = (0x002d), // The file system directory that contains the templates that 
            // are available to all users. A typical path is C:\Documents 
            // and Settings\All Users\Templates. Valid only for Windows 
            // NT systems.
            CSIDL_COMMON_VIDEO = (0x0037), // Version 6.0. The file system directory that serves as a 
            // repository for video files common to all users. A typical 
            // path is C:\Documents and Settings\All Users\Documents\
            // My Videos.
            CSIDL_CONTROLS = (0x0003), // The virtual folder containing icons for the Control Panel 
            // applications.
            CSIDL_COOKIES = (0x0021), // The file system directory that serves as a common repository 
            // for Internet cookies. A typical path is C:\Documents and 
            // Settings\username\Cookies.
            CSIDL_DESKTOP = (0x0000), // The virtual folder representing the Windows desktop, the root 
            // of the namespace.
            CSIDL_DESKTOPDIRECTORY = (0x0010), // The file system directory used to physically store file 
            // objects on the desktop (not to be confused with the desktop 
            // folder itself). A typical path is C:\Documents and 
            // Settings\username\Desktop.
            CSIDL_DRIVES = (0x0011), // The virtual folder representing My Computer, containing 
            // everything on the local computer: storage devices, printers,
            // and Control Panel. The folder may also contain mapped 
            // network drives.
            CSIDL_FAVORITES = (0x0006), // The file system directory that serves as a common repository 
            // for the user's favorite items. A typical path is C:\Documents
            // and Settings\username\Favorites.
            CSIDL_FONTS = (0x0014), // A virtual folder containing fonts. A typical path is 
            // C:\Windows\Fonts.
            CSIDL_HISTORY = (0x0022), // The file system directory that serves as a common repository
            // for Internet history items.
            CSIDL_INTERNET = (0x0001), // A virtual folder representing the Internet.
            CSIDL_INTERNET_CACHE = (0x0020), // Version 4.72. The file system directory that serves as a 
            // common repository for temporary Internet files. A typical 
            // path is C:\Documents and Settings\username\Local Settings\
            // Temporary Internet Files.
            CSIDL_LOCAL_APPDATA = (0x001c), // Version 5.0. The file system directory that serves as a data
            // repository for local (nonroaming) applications. A typical 
            // path is C:\Documents and Settings\username\Local Settings\
            // Application Data.
            CSIDL_MYDOCUMENTS = (0x000c), // Version 6.0. The virtual folder representing the My Documents
            // desktop item. This should not be confused with 
            // CSIDL_PERSONAL, which represents the file system folder that 
            // physically stores the documents.
            CSIDL_MYMUSIC = (0x000d), // The file system directory that serves as a common repository 
            // for music files. A typical path is C:\Documents and Settings
            // \User\My Documents\My Music.
            CSIDL_MYPICTURES = (0x0027), // Version 5.0. The file system directory that serves as a 
            // common repository for image files. A typical path is 
            // C:\Documents and Settings\username\My Documents\My Pictures.
            CSIDL_MYVIDEO = (0x000e), // Version 6.0. The file system directory that serves as a 
            // common repository for video files. A typical path is 
            // C:\Documents and Settings\username\My Documents\My Videos.
            CSIDL_NETHOOD = (0x0013), // A file system directory containing the link objects that may 
            // exist in the My Network Places virtual folder. It is not the
            // same as CSIDL_NETWORK, which represents the network namespace
            // root. A typical path is C:\Documents and Settings\username\
            // NetHood.
            CSIDL_NETWORK = (0x0012), // A virtual folder representing Network Neighborhood, the root
            // of the network namespace hierarchy.
            CSIDL_PERSONAL = (0x0005), // The file system directory used to physically store a user's
            // common repository of documents. A typical path is 
            // C:\Documents and Settings\username\My Documents. This should
            // be distinguished from the virtual My Documents folder in 
            // the namespace, identified by CSIDL_MYDOCUMENTS. 
            CSIDL_PRINTERS = (0x0004), // The virtual folder containing installed printers.
            CSIDL_PRINTHOOD = (0x001b), // The file system directory that contains the link objects that
            // can exist in the Printers virtual folder. A typical path is 
            // C:\Documents and Settings\username\PrintHood.
            CSIDL_PROFILE = (0x0028), // Version 5.0. The user's profile folder. A typical path is 
            // C:\Documents and Settings\username. Applications should not 
            // create files or folders at this level; they should put their
            // data under the locations referred to by CSIDL_APPDATA or
            // CSIDL_LOCAL_APPDATA.
            CSIDL_PROFILES = (0x003e), // Version 6.0. The file system directory containing user 
            // profile folders. A typical path is C:\Documents and Settings.
            CSIDL_PROGRAM_FILES = (0x0026), // Version 5.0. The Program Files folder. A typical path is 
            // C:\Program Files.
            CSIDL_PROGRAM_FILES_COMMON = (0x002b), // Version 5.0. A folder for components that are shared across 
            // applications. A typical path is C:\Program Files\Common. 
            // Valid only for Windows NT, Windows 2000, and Windows XP 
            // systems. Not valid for Windows Millennium Edition 
            // (Windows Me).
            CSIDL_PROGRAMS = (0x0002), // The file system directory that contains the user's program 
            // groups (which are themselves file system directories).
            // A typical path is C:\Documents and Settings\username\
            // Start Menu\Programs. 
            CSIDL_RECENT = (0x0008), // The file system directory that contains shortcuts to the 
            // user's most recently used documents. A typical path is 
            // C:\Documents and Settings\username\My Recent Documents. 
            // To create a shortcut in this folder, use SHAddToRecentDocs.
            // In addition to creating the shortcut, this function updates
            // the Shell's list of recent documents and adds the shortcut 
            // to the My Recent Documents submenu of the Start menu.
            CSIDL_SENDTO = (0x0009), // The file system directory that contains Send To menu items.
            // A typical path is C:\Documents and Settings\username\SendTo.
            CSIDL_STARTMENU = (0x000b), // The file system directory containing Start menu items. A 
            // typical path is C:\Documents and Settings\username\Start Menu.
            CSIDL_STARTUP = (0x0007), // The file system directory that corresponds to the user's 
            // Startup program group. The system starts these programs 
            // whenever any user logs onto Windows NT or starts Windows 95.
            // A typical path is C:\Documents and Settings\username\
            // Start Menu\Programs\Startup.
            CSIDL_SYSTEM = (0x0025), // Version 5.0. The Windows System folder. A typical path is 
            // C:\Windows\System32.
            CSIDL_TEMPLATES = (0x0015), // The file system directory that serves as a common repository
            // for document templates. A typical path is C:\Documents 
            // and Settings\username\Templates.
            CSIDL_WINDOWS = (0x0024), // Version 5.0. The Windows directory or SYSROOT. This 
            // corresponds to the %windir% or %SYSTEMROOT% environment 
            // variables. A typical path is C:\Windows.
        }

        public enum SHGFP_TYPE
        {
            SHGFP_TYPE_CURRENT = 0,        // current value for user, verify it exists
            SHGFP_TYPE_DEFAULT = 1        // default value, may not exist
        }

        public enum SFGAO : uint
        {
            SFGAO_CANCOPY = 0x00000001,    // Objects can be copied    
            SFGAO_CANMOVE = 0x00000002,    // Objects can be moved     
            SFGAO_CANLINK = 0x00000004,    // Objects can be linked    
            SFGAO_STORAGE = 0x00000008,   // supports BindToObject(IID_IStorage)
            SFGAO_CANRENAME = 0x00000010,   // Objects can be renamed
            SFGAO_CANDELETE = 0x00000020,   // Objects can be deleted
            SFGAO_HASPROPSHEET = 0x00000040,   // Objects have property sheets
            SFGAO_DROPTARGET = 0x00000100,   // Objects are drop target
            SFGAO_CAPABILITYMASK = 0x00000177,    // This flag is a mask for the capability flags.
            SFGAO_ENCRYPTED = 0x00002000,   // object is encrypted (use alt color)
            SFGAO_ISSLOW = 0x00004000,   // 'slow' object
            SFGAO_GHOSTED = 0x00008000,   // ghosted icon
            SFGAO_LINK = 0x00010000,   // Shortcut (link)
            SFGAO_SHARE = 0x00020000,   // shared
            SFGAO_READONLY = 0x00040000,   // read-only
            SFGAO_HIDDEN = 0x00080000,   // hidden object
            SFGAO_DISPLAYATTRMASK = 0x000FC000,    // This flag is a mask for the display attributes.
            SFGAO_FILESYSANCESTOR = 0x10000000,   // may contain children with SFGAO_FILESYSTEM
            SFGAO_FOLDER = 0x20000000,   // support BindToObject(IID_IShellFolder)
            SFGAO_FILESYSTEM = 0x40000000,   // is a win32 file system object (file/folder/root)
            SFGAO_HASSUBFOLDER = 0x80000000,   // may contain children with SFGAO_FOLDER
            SFGAO_CONTENTSMASK = 0x80000000,    // This flag is a mask for the contents attributes.
            SFGAO_VALIDATE = 0x01000000,   // invalidate cached information
            SFGAO_REMOVABLE = 0x02000000,   // is this removeable media?
            SFGAO_COMPRESSED = 0x04000000,   // Object is compressed (use alt color)
            SFGAO_BROWSABLE = 0x08000000,   // supports IShellFolder, but only implements CreateViewObject() (non-folder view)
            SFGAO_NONENUMERATED = 0x00100000,   // is a non-enumerated object
            SFGAO_NEWCONTENT = 0x00200000,   // should show bold in explorer tree
            SFGAO_CANMONIKER = 0x00400000,   // defunct
            SFGAO_HASSTORAGE = 0x00400000,   // defunct
            SFGAO_STREAM = 0x00400000,   // supports BindToObject(IID_IStream)
            SFGAO_STORAGEANCESTOR = 0x00800000,   // may contain children with SFGAO_STORAGE or SFGAO_STREAM
            SFGAO_STORAGECAPMASK = 0x70C50008    // for determining storage capabilities, ie for open/save semantics

        }

        public enum SHCONTF
        {
            SHCONTF_FOLDERS = 0x0020,   // only want folders enumerated (SFGAO_FOLDER)
            SHCONTF_NONFOLDERS = 0x0040,   // include non folders
            SHCONTF_INCLUDEHIDDEN = 0x0080,   // show items normally hidden
            SHCONTF_INIT_ON_FIRST_NEXT = 0x0100,   // allow EnumObject() to return before validating enum
            SHCONTF_NETPRINTERSRCH = 0x0200,   // hint that client is looking for printers
            SHCONTF_SHAREABLE = 0x0400,   // hint that client is looking sharable resources (remote shares)
            SHCONTF_STORAGE = 0x0800,   // include all items with accessible storage and their ancestors
        }

        public enum SHCIDS : uint
        {
            SHCIDS_ALLFIELDS = 0x80000000,    // Compare all the information contained in the ITEMIDLIST 
            // structure, not just the display names
            SHCIDS_CANONICALONLY = 0x10000000,    // When comparing by name, compare the system names but not the 
            // display names. 
            SHCIDS_BITMASK = 0xFFFF0000,
            SHCIDS_COLUMNMASK = 0x0000FFFF
        }

        public enum SHGNO
        {
            SHGDN_NORMAL = 0x0000,        // default (display purpose)
            SHGDN_INFOLDER = 0x0001,        // displayed under a folder (relative)
            SHGDN_FOREDITING = 0x1000,        // for in-place editing
            SHGDN_FORADDRESSBAR = 0x4000,        // UI friendly parsing name (remove ugly stuff)
            SHGDN_FORPARSING = 0x8000        // parsing name for ParseDisplayName()
        }

        public enum STRRET_TYPE
        {
            STRRET_WSTR = 0x0000,            // Use STRRET.pOleStr
            STRRET_OFFSET = 0x0001,            // Use STRRET.uOffset to Ansi
            STRRET_CSTR = 0x0002            // Use STRRET.cStr
        }


        public enum PrinterActions
        {
            PRINTACTION_OPEN = 0,    // The printer specified by the name in lpBuf1 will be opened. 
            // lpBuf2 is ignored. 
            PRINTACTION_PROPERTIES = 1,    // The properties for the printer specified by the name in lpBuf1
            // will be displayed. lpBuf2 can either be NULL or specify.
            PRINTACTION_NETINSTALL = 2,    // The network printer specified by the name in lpBuf1 will be 
            // installed. lpBuf2 is ignored. 
            PRINTACTION_NETINSTALLLINK = 3,    // A shortcut to the network printer specified by the name in lpBuf1
            // will be created. lpBuf2 specifies the drive and path of the folder 
            // in which the shortcut will be created. The network printer must 
            // have already been installed on the local computer. 
            PRINTACTION_TESTPAGE = 4,    // A test page will be printed on the printer specified by the name
            // in lpBuf1. lpBuf2 is ignored. 
            PRINTACTION_OPENNETPRN = 5,    // The network printer specified by the name in lpBuf1 will be
            // opened. lpBuf2 is ignored. 
            PRINTACTION_DOCUMENTDEFAULTS = 6,    // Microsoft?Windows NT?only. The default document properties for
            // the printer specified by the name in lpBuf1 will be displayed. 
            // lpBuf2 is ignored. 
            PRINTACTION_SERVERPROPERTIES = 7        // Windows NT only. The properties for the server of the printer 
            // specified by the name in lpBuf1 will be displayed. lpBuf2 
            // is ignored.
        }


    }
    public class ShellBrowseForFolderDialog
    {
        // The SendMessage function sends the specified message to a window or windows. It calls the window procedure for 
        // the specified window and does not return until the window procedure has processed the message. 
        [DllImport("User32.dll")]
        public static extern Int32 SendMessage(
            IntPtr hWnd,            // handle to destination window
            UInt32 Msg,                // message
            UInt32 wParam,            // first message parameter
            Int32 lParam            // second message parameter
            );

        [DllImport("User32.dll")]
        public static extern Int32 SendMessage(
            IntPtr hWnd,            // handle to destination window
            UInt32 Msg,                // message
            UInt32 wParam,            // first message parameter
            [MarshalAs(UnmanagedType.LPWStr)]
            String lParam            // second message parameter
            );

        public enum RootTypeOptions
        {
            BySpecialFolder,
            ByPath
        }

        [Flags]
        public enum BrowseInfoFlag // BIF
        {
            BIF_RETURNONLYFSDIRS = 0x0001,    // For finding a folder to start document searching
            BIF_DONTGOBELOWDOMAIN = 0x0002,    // For starting the Find Computer
            BIF_STATUSTEXT = 0x0004,    // Top of the dialog has 2 lines of text for BROWSEINFO.lpszTitle and 
            // one line if this flag is set.  Passing the message 
            // BFFM_SETSTATUSTEXTA to the hwnd can set the rest of the text.  
            // This is not used with BIF_USENEWUI and BROWSEINFO.lpszTitle gets
            // all three lines of text.
            BIF_RETURNFSANCESTORS = 0x0008,
            BIF_EDITBOX = 0x0010,    // Add an editbox to the dialog
            BIF_VALIDATE = 0x0020,    // insist on valid result (or CANCEL)
            BIF_NEWDIALOGSTYLE = 0x0040,    // Use the new dialog layout with the ability to resize
            // Caller needs to call OleInitialize() before using this API
            BIF_USENEWUI = (BIF_NEWDIALOGSTYLE | BIF_EDITBOX),
            BIF_BROWSEINCLUDEURLS = 0x0080,    // Allow URLs to be displayed or entered. (Requires BIF_USENEWUI)
            BIF_UAHINT = 0x0100,    // Add a UA hint to the dialog, in place of the edit box. May not be 
            // combined with BIF_EDITBOX
            BIF_NONEWFOLDERBUTTON = 0x0200,    // Do not add the "New Folder" button to the dialog.  Only applicable 
            // with BIF_NEWDIALOGSTYLE.
            BIF_NOTRANSLATETARGETS = 0x0400,    // don't traverse target as shortcut
            BIF_BROWSEFORCOMPUTER = 0x1000,    // Browsing for Computers.
            BIF_BROWSEFORPRINTER = 0x2000,    // Browsing for Printers
            BIF_BROWSEINCLUDEFILES = 0x4000,    // Browsing for Everything
            BIF_SHAREABLE = 0x8000        // sharable resources displayed (remote shares, requires BIF_USENEWUI)
        }

        public enum BrowseForFolderMessages    // BFFM
        {
            // message from browser
            BFFM_INITIALIZED = 1,
            BFFM_SELCHANGED = 2,
            BFFM_VALIDATEFAILEDA = 3,                // lParam:szPath ret:1(cont),0(EndDialog)
            BFFM_VALIDATEFAILEDW = 4,                // lParam:wzPath ret:1(cont),0(EndDialog)
            BFFM_IUNKNOWN = 5,                 // provides IUnknown to client. lParam: IUnknown*

            // messages to browser
            // 0x400 = WM_USER
            BFFM_SETSTATUSTEXTA = (0x0400 + 100),
            BFFM_ENABLEOK = (0x0400 + 101),
            BFFM_SETSELECTIONA = (0x0400 + 102),
            BFFM_SETSELECTIONW = (0x0400 + 103),
            BFFM_SETSTATUSTEXTW = (0x0400 + 104),
            BFFM_SETOKTEXT = (0x0400 + 105),    // Unicode only
            BFFM_SETEXPANDED = (0x0400 + 106)    // Unicode only
        }

        public class InitializedEventArgs : EventArgs
        {
            public InitializedEventArgs(IntPtr hwnd)
            {
                this.hwnd = hwnd;
            }
            public readonly IntPtr hwnd;
        }

        public class IUnknownEventArgs : EventArgs
        {
            public IUnknownEventArgs(IntPtr hwnd, IntPtr iunknown)
            {
                this.hwnd = hwnd;
                this.iunknown = iunknown;
            }
            public readonly IntPtr hwnd;
            public readonly IntPtr iunknown;
        }

        public class SelChangedEventArgs : EventArgs
        {
            public SelChangedEventArgs(IntPtr hwnd, IntPtr pidl)
            {
                this.hwnd = hwnd;
                this.pidl = pidl;
            }
            public readonly IntPtr hwnd;
            public readonly IntPtr pidl;
        }

        public class ValidateFailedEventArgs : EventArgs
        {
            public ValidateFailedEventArgs(IntPtr hwnd, string invalidSel)
            {
                this.hwnd = hwnd;
                this.invalidSel = invalidSel;
            }
            public readonly IntPtr hwnd;
            public readonly string invalidSel;
        }


        public delegate void InitializedHandler(ShellBrowseForFolderDialog sender, InitializedEventArgs args);
        public delegate void IUnknownHandler(ShellBrowseForFolderDialog sender, IUnknownEventArgs args);
        public delegate void SelChangedHandler(ShellBrowseForFolderDialog sender, SelChangedEventArgs args);
        public delegate int ValidateFailedHandler(ShellBrowseForFolderDialog sender, ValidateFailedEventArgs args);

        public event InitializedHandler OnInitialized;
        public event IUnknownHandler OnIUnknown;
        public event SelChangedHandler OnSelChanged;
        public event ValidateFailedHandler OnValidateFailed;

        public void EnableOk(IntPtr hwnd, bool Enabled)
        {
            SendMessage(hwnd, (uint)BrowseForFolderMessages.BFFM_ENABLEOK, 0, Enabled ? 1 : 0);
        }

        public void SetExpanded(IntPtr hwnd, string path)
        {
            SendMessage(hwnd, (uint)BrowseForFolderMessages.BFFM_SETEXPANDED, 1, path);
        }

        public void SetOkText(IntPtr hwnd, string text)
        {
            SendMessage(hwnd, (uint)BrowseForFolderMessages.BFFM_SETOKTEXT, 0, text);
        }

        public void SetSelection(IntPtr hwnd, string path)
        {
            SendMessage(hwnd, (uint)BrowseForFolderMessages.BFFM_SETSELECTIONW, 1, path);
        }

        public void SetStatusText(IntPtr hwnd, string text)
        {
            SendMessage(hwnd, (uint)BrowseForFolderMessages.BFFM_SETSTATUSTEXTW, 1, text);
        }


        private Int32 myBrowseCallbackProc(IntPtr hwnd, UInt32 uMsg, Int32 lParam, Int32 lpData)
        {
            switch ((BrowseForFolderMessages)uMsg)
            {
                case BrowseForFolderMessages.BFFM_INITIALIZED:
                    System.Diagnostics.Debug.WriteLine("BFFM_INITIALIZED");

                    if (OnInitialized != null)
                    {
                        InitializedEventArgs args = new InitializedEventArgs(hwnd);
                        OnInitialized(this, args);
                    }

                    break;

                case BrowseForFolderMessages.BFFM_IUNKNOWN:
                    System.Diagnostics.Debug.WriteLine("BFFM_IUNKNOWN");

                    if (OnIUnknown != null)
                    {
                        IUnknownEventArgs args = new IUnknownEventArgs(hwnd, (IntPtr)lParam);
                        OnIUnknown(this, args);
                    }

                    break;

                case BrowseForFolderMessages.BFFM_SELCHANGED:
                    System.Diagnostics.Debug.WriteLine("BFFM_SELCHANGED");

                    if (OnSelChanged != null)
                    {
                        SelChangedEventArgs args = new SelChangedEventArgs(hwnd, (IntPtr)lParam);
                        OnSelChanged(this, args);
                    }

                    break;

                case BrowseForFolderMessages.BFFM_VALIDATEFAILEDA:
                    System.Diagnostics.Debug.WriteLine("BFFM_VALIDATEFAILEDA");

                    if (OnValidateFailed != null)
                    {
                        string failedSel = Marshal.PtrToStringAnsi((IntPtr)lParam);
                        ValidateFailedEventArgs args = new ValidateFailedEventArgs(hwnd, failedSel);
                        return OnValidateFailed(this, args);
                    }
                    break;

                case BrowseForFolderMessages.BFFM_VALIDATEFAILEDW:
                    System.Diagnostics.Debug.WriteLine("BFFM_VALIDATEFAILEDW");

                    if (OnValidateFailed != null)
                    {
                        string failedSel = Marshal.PtrToStringUni((IntPtr)lParam);
                        ValidateFailedEventArgs args = new ValidateFailedEventArgs(hwnd, failedSel);
                        return OnValidateFailed(this, args);
                    }

                    break;
            }

            return 0;
        }


        public ShellBrowseForFolderDialog()
        {
            hwndOwner = IntPtr.Zero;
            RootType = RootTypeOptions.BySpecialFolder;
            RootSpecialFolder = ShellApi.CSIDL.CSIDL_DESKTOP;
            RootPath = "";
            m_DisplayName = "";
            Title = "";
            UserToken = IntPtr.Zero;
            m_FullName = "";

            // Default flags values
            DetailsFlags = BrowseInfoFlag.BIF_BROWSEINCLUDEFILES
                | BrowseInfoFlag.BIF_EDITBOX
                | BrowseInfoFlag.BIF_NEWDIALOGSTYLE
                | BrowseInfoFlag.BIF_SHAREABLE
                | BrowseInfoFlag.BIF_STATUSTEXT
                | BrowseInfoFlag.BIF_USENEWUI
                | BrowseInfoFlag.BIF_VALIDATE;

        }

        public void ShowDialog()
        {
            m_FullName = "";
            m_DisplayName = "";

            // Get shell's memory allocator, it is needed to free some memory later
            IMalloc pMalloc;
            pMalloc = ShellFunctions.GetMalloc();

            IntPtr pidlRoot;

            if (RootType == RootTypeOptions.BySpecialFolder)
            {
                ShellApi.SHGetFolderLocation(hwndOwner, (int)RootSpecialFolder, UserToken, 0, out pidlRoot);
            }
            else    // m_RootType = RootTypeOptions.ByPath
            {
                uint iAttribute;
                ShellApi.SHParseDisplayName(RootPath, IntPtr.Zero, out pidlRoot, 0, out iAttribute);
            }

            ShellApi.BROWSEINFO bi = new ShellApi.BROWSEINFO();

            bi.hwndOwner = hwndOwner;
            bi.pidlRoot = pidlRoot;
            bi.pszDisplayName = new String(' ', 256);
            bi.lpszTitle = Title;
            bi.ulFlags = (uint)DetailsFlags;
            bi.lParam = 0;
            bi.lpfn = new ShellApi.BrowseCallbackProc(this.myBrowseCallbackProc);

            // Show dialog
            IntPtr pidlSelected;
            pidlSelected = JHDock.ShellApi.SHBrowseForFolder(ref bi);

            // Save the display name
            m_DisplayName = bi.pszDisplayName.ToString();

            IShellFolder isf = ShellFunctions.GetDesktopFolder();

            ShellApi.STRRET ptrDisplayName;
            isf.GetDisplayNameOf(pidlSelected, (uint)ShellApi.SHGNO.SHGDN_NORMAL | (uint)ShellApi.SHGNO.SHGDN_FORPARSING, out ptrDisplayName);

            String sDisplay;
            JHDock.ShellApi.StrRetToBSTR(ref ptrDisplayName, pidlRoot, out sDisplay);
            m_FullName = sDisplay;

            if (pidlRoot != IntPtr.Zero)
                pMalloc.Free(pidlRoot);

            if (pidlSelected != IntPtr.Zero)
                pMalloc.Free(pidlSelected);

            Marshal.ReleaseComObject(isf);
            Marshal.ReleaseComObject(pMalloc);
        }


        ///  <summary> Handle to the owner window for the dialog box.  </summary>
        public IntPtr hwndOwner;

        /// <summary> Select the root type </summary>
        public RootTypeOptions RootType;

        /// <summary> valid only if RootType is RootTypeOptions.ByPath </summary>
        public string RootPath;

        /// <summary> valid only if RootType is RootTypeOptions.BySpecialFolder </summary>
        public ShellApi.CSIDL RootSpecialFolder;

        /// <summary> Address of a buffer to receive the display name of the folder selected by the user. </summary>
        public string DisplayName
        {
            get
            {
                return m_DisplayName;
            }
        }
        private string m_DisplayName;

        /// <summary> 
        /// Address of a null-terminated string that is displayed above the tree view control in the dialog box. 
        /// </summary>
        public string Title;

        /// <summary> Token that can be used to represent a particular user. </summary>
        public IntPtr UserToken;

        /// <summary> Return the result of the dialog </summary>
        public string FullName
        {
            get
            {
                return m_FullName;
            }
        }
        private string m_FullName;

        public BrowseInfoFlag DetailsFlags;

    }
    public class ShellGUIDs
    {
        public static Guid IID_IMalloc =
            new Guid("{00000002-0000-0000-C000-000000000046}");
        public static Guid IID_IShellFolder =
            new Guid("{000214E6-0000-0000-C000-000000000046}");
        public static Guid IID_IFolderFilterSite =
            new Guid("{C0A651F5-B48B-11d2-B5ED-006097C686F6}");
        public static Guid IID_IFolderFilter =
            new Guid("{9CC22886-DC8E-11d2-B1D0-00C04F8EEB3E}");
    }
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("000214E6-0000-0000-C000-000000000046")]
    public interface IShellFolder
    {
        // Translates a file object's or folder's display name into an item identifier list.
        // Return value: error code, if any
        [PreserveSig]
        Int32 ParseDisplayName(
            IntPtr hwnd,                // Optional window handle
            IntPtr pbc,                    // Optional bind context that controls the parsing operation. This parameter is 
            // normally set to NULL. 
            [MarshalAs(UnmanagedType.LPWStr)] 
            String pszDisplayName,        // Null-terminated UNICODE string with the display name.
            ref UInt32 pchEaten,        // Pointer to a ULONG value that receives the number of characters of the 
            // display name that was parsed.
            out IntPtr ppidl,            // Pointer to an ITEMIDLIST pointer that receives the item identifier list for 
            // the object.
            ref UInt32 pdwAttributes);    // Optional parameter that can be used to query for file attributes.
        // this can be values from the SFGAO enum

        // Allows a client to determine the contents of a folder by creating an item identifier enumeration object 
        // and returning its IEnumIDList interface.
        // Return value: error code, if any
        [PreserveSig]
        Int32 EnumObjects(
            IntPtr hwnd,                // If user input is required to perform the enumeration, this window handle 
            // should be used by the enumeration object as the parent window to take 
            // user input.
            Int32 grfFlags,                // Flags indicating which items to include in the enumeration. For a list 
            // of possible values, see the SHCONTF enum. 
            out IntPtr ppenumIDList);    // Address that receives a pointer to the IEnumIDList interface of the 
        // enumeration object created by this method. 

        // Retrieves an IShellFolder object for a subfolder.
        // Return value: error code, if any
        [PreserveSig]
        Int32 BindToObject(
            IntPtr pidl,                // Address of an ITEMIDLIST structure (PIDL) that identifies the subfolder.
            IntPtr pbc,                    // Optional address of an IBindCtx interface on a bind context object to be 
            // used during this operation.
            Guid riid,                    // Identifier of the interface to return. 
            out IntPtr ppv);            // Address that receives the interface pointer.

        // Requests a pointer to an object's storage interface. 
        // Return value: error code, if any
        [PreserveSig]
        Int32 BindToStorage(
            IntPtr pidl,                // Address of an ITEMIDLIST structure that identifies the subfolder relative 
            // to its parent folder. 
            IntPtr pbc,                    // Optional address of an IBindCtx interface on a bind context object to be 
            // used during this operation.
            Guid riid,                    // Interface identifier (IID) of the requested storage interface.
            out IntPtr ppv);            // Address that receives the interface pointer specified by riid.

        // Determines the relative order of two file objects or folders, given their item identifier lists.
        // Return value: If this method is successful, the CODE field of the HRESULT contains one of the following 
        // values (the code can be retrived using the helper function GetHResultCode):
        // Negative A negative return value indicates that the first item should precede the second (pidl1 < pidl2). 
        // Positive A positive return value indicates that the first item should follow the second (pidl1 > pidl2). 
        // Zero A return value of zero indicates that the two items are the same (pidl1 = pidl2). 
        [PreserveSig]
        Int32 CompareIDs(
            Int32 lParam,                // Value that specifies how the comparison should be performed. The lower 
            // sixteen bits of lParam define the sorting rule. The upper sixteen bits of 
            // lParam are used for flags that modify the sorting rule. values can be from 
            // the SHCIDS enum
            IntPtr pidl1,                // Pointer to the first item's ITEMIDLIST structure.
            IntPtr pidl2);                // Pointer to the second item's ITEMIDLIST structure.

        // Requests an object that can be used to obtain information from or interact with a folder object.
        // Return value: error code, if any
        [PreserveSig]
        Int32 CreateViewObject(
            IntPtr hwndOwner,            // Handle to the owner window.
            Guid riid,                    // Identifier of the requested interface. 
            out IntPtr ppv);            // Address of a pointer to the requested interface. 

        // Retrieves the attributes of one or more file objects or subfolders. 
        // Return value: error code, if any
        [PreserveSig]
        Int32 GetAttributesOf(
            UInt32 cidl,                // Number of file objects from which to retrieve attributes. 

            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)]
            IntPtr[] apidl,                // Address of an array of pointers to ITEMIDLIST structures, each of which 
            // uniquely identifies a file object relative to the parent folder.
            ref UInt32 rgfInOut);        // Address of a single ULONG value that, on entry, contains the attributes that 
        // the caller is requesting. On exit, this value contains the requested 
        // attributes that are common to all of the specified objects. this value can
        // be from the SFGAO enum

        // Retrieves an OLE interface that can be used to carry out actions on the specified file objects or folders.
        // Return value: error code, if any
        [PreserveSig]
        Int32 GetUIObjectOf(
            IntPtr hwndOwner,            // Handle to the owner window that the client should specify if it displays 
            // a dialog box or message box.
            UInt32 cidl,                // Number of file objects or subfolders specified in the apidl parameter. 
            IntPtr[] apidl,                // Address of an array of pointers to ITEMIDLIST structures, each of which 
            // uniquely identifies a file object or subfolder relative to the parent folder.
            Guid riid,                    // Identifier of the COM interface object to return.
            ref UInt32 rgfReserved,        // Reserved. 
            out IntPtr ppv);            // Pointer to the requested interface.

        // Retrieves the display name for the specified file object or subfolder. 
        // Return value: error code, if any
        [PreserveSig]
        Int32 GetDisplayNameOf(
            IntPtr pidl,                // Address of an ITEMIDLIST structure (PIDL) that uniquely identifies the file 
            // object or subfolder relative to the parent folder. 
            UInt32 uFlags,                // Flags used to request the type of display name to return. For a list of 
            // possible values, see the SHGNO enum. 
            out ShellApi.STRRET pName);            // Address of a STRRET structure in which to return the display name.

        // Sets the display name of a file object or subfolder, changing the item identifier in the process.
        // Return value: error code, if any
        [PreserveSig]
        Int32 SetNameOf(
            IntPtr hwnd,                // Handle to the owner window of any dialog or message boxes that the client 
            // displays.
            IntPtr pidl,                // Pointer to an ITEMIDLIST structure that uniquely identifies the file object
            // or subfolder relative to the parent folder. 
            [MarshalAs(UnmanagedType.LPWStr)] 
            String pszName,                // Pointer to a null-terminated string that specifies the new display name. 
            UInt32 uFlags,                // Flags indicating the type of name specified by the lpszName parameter. For 
            // a list of possible values, see the description of the SHGNO enum. 
            out IntPtr ppidlOut);        // Address of a pointer to an ITEMIDLIST structure which receives the new ITEMIDLIST. 
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("00000002-0000-0000-C000-000000000046")]
    public interface IMalloc
    {
        // Allocates a block of memory.
        // Return value: a pointer to the allocated memory block.
        [PreserveSig]
        IntPtr Alloc(
            UInt32 cb);        // Size, in bytes, of the memory block to be allocated. 

        // Changes the size of a previously allocated memory block.
        // Return value:  Reallocated memory block 
        [PreserveSig]
        IntPtr Realloc(
            IntPtr pv,        // Pointer to the memory block to be reallocated.
            UInt32 cb);        // Size of the memory block (in bytes) to be reallocated.

        // Frees a previously allocated block of memory.
        [PreserveSig]
        void Free(
            IntPtr pv);        // Pointer to the memory block to be freed.

        // This method returns the size (in bytes) of a memory block previously allocated with 
        // IMalloc::Alloc or IMalloc::Realloc.
        // Return value: The size of the allocated memory block in bytes 
        [PreserveSig]
        UInt32 GetSize(
            IntPtr pv);        // Pointer to the memory block for which the size is requested.

        // This method determines whether this allocator was used to allocate the specified block of memory.
        // Return value: 1 - allocated 0 - not allocated by this IMalloc instance. 
        [PreserveSig]
        Int16 DidAlloc(
            IntPtr pv);        // Pointer to the memory block

        // This method minimizes the heap as much as possible by releasing unused memory to the operating system, 
        // coalescing adjacent free blocks and committing free pages.
        [PreserveSig]
        void HeapMinimize();
    }
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("9CC22886-DC8E-11d2-B1D0-00C04F8EEB3E")]
    public interface IFolderFilter
    {
        // Allows a client to specify which individual items should be enumerated.
        // Note: The host calls this method for each item in the folder. Return S_OK, to have the item enumerated. 
        // Return S_FALSE to prevent the item from being enumerated.
        [PreserveSig]
        Int32 ShouldShow(
            [MarshalAs(UnmanagedType.Interface)]Object psf,                // A pointer to the folder's IShellFolder interface.
            IntPtr pidlFolder,        // The folder's PIDL.
            IntPtr pidlItem);        // The item's PIDL.

        // Allows a client to specify which classes of objects in a Shell folder should be enumerated.
        [PreserveSig]
        Int32 GetEnumFlags(
            [MarshalAs(UnmanagedType.Interface)]Object psf,                // A pointer to the folder's IShellFolder interface.
            IntPtr pidlFolder,        // The folder's PIDL.
            IntPtr phwnd,            // A pointer to the host's window handle.
            out UInt32 pgrfFlags);    // One or more SHCONTF values that specify which classes of objects to enumerate.

    };
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("C0A651F5-B48B-11d2-B5ED-006097C686F6")]
    public interface IFolderFilterSite
    {
        // Exposed by a host to allow clients to pass the host their IUnknown interface pointers.
        [PreserveSig]
        Int32 SetFilter(
            [MarshalAs(UnmanagedType.Interface)]Object punk);        // A pointer to the client's IUnknown interface. To notify the host to terminate 
        // filtering and stop calling your IFolderFilter interface, set this parameter to NULL. 
    }
    [ComVisible(true)]
    [Guid("3766C955-DA6F-4fbc-AD36-311E342EF180")]
    public class FilterByExtension : IFolderFilter
    {
        // Allows a client to specify which individual items should be enumerated.
        // Note: The host calls this method for each item in the folder. Return S_OK, to have the item enumerated. 
        // Return S_FALSE to prevent the item from being enumerated.
        public Int32 ShouldShow(
            Object psf,                // A pointer to the folder's IShellFolder interface.
            IntPtr pidlFolder,        // The folder's PIDL.
            IntPtr pidlItem)        // The item's PIDL.
        {
            // check extension, and if not ok return 1 (S_FALSE)

            // get display name of item
            JHDock.IShellFolder isf = (JHDock.IShellFolder)psf;

            JHDock.ShellApi.STRRET ptrDisplayName;
            isf.GetDisplayNameOf(pidlItem, (uint)JHDock.ShellApi.SHGNO.SHGDN_NORMAL | (uint)JHDock.ShellApi.SHGNO.SHGDN_FORPARSING, out ptrDisplayName);

            String sDisplay;
            JHDock.ShellApi.StrRetToBSTR(ref ptrDisplayName, (IntPtr)0, out sDisplay);

            // check if item is file or folder
            IntPtr[] aPidl = new IntPtr[1];
            aPidl[0] = pidlItem;
            uint Attrib;
            Attrib = (uint)JHDock.ShellApi.SFGAO.SFGAO_FOLDER;

            int temp;
            temp = isf.GetAttributesOf(1, aPidl, ref Attrib);

            // if item is a folder, accept
            if ((Attrib & (uint)JHDock.ShellApi.SFGAO.SFGAO_FOLDER) == (uint)JHDock.ShellApi.SFGAO.SFGAO_FOLDER)
                return 0;

            // if item is file, check if it has a valid extension
            for (int i = 0; i < ValidExtension.Length; i++)
            {
                if (sDisplay.ToUpper().EndsWith("." + ValidExtension[i].ToUpper()))
                    return 0;
            }

            return 1;
        }

        // Allows a client to specify which classes of objects in a Shell folder should be enumerated.
        public Int32 GetEnumFlags(
            Object psf,                // A pointer to the folder's IShellFolder interface.
            IntPtr pidlFolder,        // The folder's PIDL.
            IntPtr phwnd,            // A pointer to the host's window handle.
            out UInt32 pgrfFlags)    // One or more SHCONTF values that specify which classes of objects to enumerate.
        {
            pgrfFlags = (uint)ShellApi.SHCONTF.SHCONTF_FOLDERS | (uint)ShellApi.SHCONTF.SHCONTF_NONFOLDERS;
            return 0;
        }

        public string[] ValidExtension;
    }
    #endregion

}