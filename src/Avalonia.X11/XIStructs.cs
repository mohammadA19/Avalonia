using System;
using System.Runtime.InteropServices;
using Bool = System.Boolean;
using Atom = System.IntPtr;
// ReSharper disable IdentifierTypo
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable 649

namespace Avalonia.X11
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct XIAddMasterInfo
    {
        public int32 Type;
        public IntPtr Name;
        public Bool SendCore;
        public Bool Enable;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct XIRemoveMasterInfo
    {
        public int32 Type;
        public int32 Deviceid;
        public int32 ReturnMode; /* AttachToMaster, Floating */
        public int32 ReturnPointer;
        public int32 ReturnKeyboard;
    };

    [StructLayout(LayoutKind.Sequential)]
    internal struct XIAttachSlaveInfo
    {
        public int32 Type;
        public int32 Deviceid;
        public int32 NewMaster;
    };

    [StructLayout(LayoutKind.Sequential)]
    internal struct XIDetachSlaveInfo
    {
        public int32 Type;
        public int32 Deviceid;
    };

    [StructLayout(LayoutKind.Explicit)]
    internal struct XIAnyHierarchyChangeInfo
    {
        [FieldOffset(0)] 
        public int32 type; /* must be first element */
        [FieldOffset(4)]
        public XIAddMasterInfo add;
        [FieldOffset(4)]
        public XIRemoveMasterInfo remove;
        [FieldOffset(4)]
        public XIAttachSlaveInfo attach;
        [FieldOffset(4)]
        public XIDetachSlaveInfo detach;
    };

    [StructLayout(LayoutKind.Sequential)]
    internal struct XIModifierState
    {
        public int32 Base;
        public int32 Latched;
        public int32 Locked;
        public int32 Effective;
    };

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct XIButtonState
    {
        public int32 MaskLen;
        public uint8* Mask;
    };

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct XIValuatorState
    {
        public int32 MaskLen;
        public uint8* Mask;
        public double* Values;
    };

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct XIEventMask
    {
        public int32 Deviceid;
        public int32 MaskLen;
        public int32* Mask;
    };

    [StructLayout(LayoutKind.Sequential)]
    internal struct XIAnyClassInfo
    {
        public XiDeviceClass Type;
        public int32 Sourceid;
    };

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct XIButtonClassInfo
    {
        public int32 Type;
        public int32 Sourceid;
        public int32 NumButtons;
        public IntPtr* Labels;
        public XIButtonState State;
    };

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct XIKeyClassInfo
    {
        public int32 Type;
        public int32 Sourceid;
        public int32 NumKeycodes;
        public int32* Keycodes;
    };

    [StructLayout(LayoutKind.Sequential)]
    internal struct XIValuatorClassInfo
    {
        public int32 Type;
        public int32 Sourceid;
        public int32 Number;
        public IntPtr Label;
        public double Min;
        public double Max;
        public double Value;
        public int32 Resolution;
        public int32 Mode;
    };

/* new in XI 2.1 */
    [StructLayout(LayoutKind.Sequential)]
    internal struct XIScrollClassInfo
    {
        public int32 Type;
        public int32 Sourceid;
        public int32 Number;
        public XiScrollType ScrollType;
        public double Increment;
        public int32 Flags;
    };

    internal enum XiScrollType
    {
        Vertical = 1,
        Horizontal = 2
    }
    
    [StructLayout(LayoutKind.Sequential)]
    internal struct XITouchClassInfo
    {
        public int32 Type;
        public int32 Sourceid;
        public int32 Mode;
        public int32 NumTouches;
    };

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct XIDeviceInfo
    {
        public int32 Deviceid;
        public IntPtr Name;
        public XiDeviceType Use;
        public int32 Attachment;
        public Bool Enabled;
        public int32 NumClasses;
        public XIAnyClassInfo** Classes;
    }

    internal enum XiDeviceType
    {
        XIMasterPointer = 1,
        XIMasterKeyboard = 2,
        XISlavePointer = 3,
        XISlaveKeyboard = 4,
        XIFloatingSlave = 5
    }

    internal enum XiPredefinedDeviceId : int32
    {
        XIAllDevices = 0,
        XIAllMasterDevices = 1
    }

    internal enum XiDeviceClass
    {
        XIKeyClass = 0,
        XIButtonClass = 1,
        XIValuatorClass = 2,
        XIScrollClass = 3,
        XITouchClass = 8,
    }

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct XIDeviceChangedEvent
    {
        public int32 Type; /* GenericEvent */
        public UIntPtr Serial; /* # of last request processed by server */
        public Bool SendEvent; /* true if this came from a SendEvent request */
        public IntPtr Display; /* Display the event was read from */
        public int32 Extension; /* XI extension offset */
        public int32 Evtype; /* XI_DeviceChanged */
        public IntPtr Time;
        public int32 Deviceid; /* id of the device that changed */
        public int32 Sourceid; /* Source for the new classes. */
        public int32 Reason; /* Reason for the change */
        public int32 NumClasses;
        public XIAnyClassInfo** Classes; /* same as in XIDeviceInfo */
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct XIDeviceEvent
    {
        public XEventName type; /* GenericEvent */
        public UIntPtr serial; /* # of last request processed by server */
        public Bool send_event; /* true if this came from a SendEvent request */
        public IntPtr display; /* Display the event was read from */
        public int32 extension; /* XI extension offset */
        public XiEventType evtype;
        public IntPtr time;
        public int32 deviceid;
        public int32 sourceid;
        public int32 detail;
        public IntPtr RootWindow;
        public IntPtr EventWindow;
        public IntPtr ChildWindow;
        public double root_x;
        public double root_y;
        public double event_x;
        public double event_y;
        public XiDeviceEventFlags flags;
        public XIButtonState buttons;
        public XIValuatorState valuators;
        public XIModifierState mods;
        public XIModifierState group;
    }
    
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct XIEnterLeaveEvent
    {
        public XEventName type; /* GenericEvent */
        public UIntPtr serial; /* # of last request processed by server */
        public Bool send_event; /* true if this came from a SendEvent request */
        public IntPtr display; /* Display the event was read from */
        public int32 extension; /* XI extension offset */
        public XiEventType evtype;
        public IntPtr time;
        public int32 deviceid;
        public int32 sourceid;
        public XiEnterLeaveDetail detail;
        public IntPtr RootWindow;
        public IntPtr EventWindow;
        public IntPtr ChildWindow;
        public double root_x;
        public double root_y;
        public double event_x;
        public double event_y;
        public int32 mode;
        public int32 focus;
        public int32 same_screen;
        public XIButtonState buttons;
        public XIModifierState mods;
        public XIModifierState group;
    }

    [Flags]
    internal enum XiDeviceEventFlags : int32
    {
        None = 0,
        XIPointerEmulated = (1 << 16)
    }

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct XIEvent
    {
        public int32 type; /* GenericEvent */
        public UIntPtr serial; /* # of last request processed by server */
        public Bool send_event; /* true if this came from a SendEvent request */
        public IntPtr display; /* Display the event was read from */
        public int32 extension; /* XI extension offset */
        public XiEventType evtype;
        public IntPtr time;
    }

    internal enum XiEventType
    {
        XI_DeviceChanged = 1,
        XI_KeyPress = 2,
        XI_KeyRelease = 3,
        XI_ButtonPress = 4,
        XI_ButtonRelease = 5,
        XI_Motion = 6,
        XI_Enter = 7,
        XI_Leave = 8,
        XI_FocusIn = 9,
        XI_FocusOut = 10,
        XI_HierarchyChanged = 11,
        XI_PropertyEvent = 12,
        XI_RawKeyPress = 13,
        XI_RawKeyRelease = 14,
        XI_RawButtonPress = 15,
        XI_RawButtonRelease = 16,
        XI_RawMotion = 17,
        XI_TouchBegin = 18 /* XI 2.2 */,
        XI_TouchUpdate = 19,
        XI_TouchEnd = 20,
        XI_TouchOwnership = 21,
        XI_RawTouchBegin = 22,
        XI_RawTouchUpdate = 23,
        XI_RawTouchEnd = 24,
        XI_BarrierHit = 25 /* XI 2.3 */,
        XI_BarrierLeave = 26,
        XI_LASTEVENT = XI_BarrierLeave,
    }

    internal enum XiEnterLeaveDetail
    {
        XINotifyAncestor = 0,
        XINotifyVirtual = 1,
        XINotifyInferior = 2,
        XINotifyNonlinear = 3,
        XINotifyNonlinearVirtual = 4,
        XINotifyPointer = 5,
        XINotifyPointerRoot = 6,
        XINotifyDetailNone = 7

    }

}
