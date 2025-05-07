using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class DianaApi
{
    
    [StructLayout(LayoutKind.Sequential)]
    public struct StrTrajectoryState
    {
        public int taskId;
        public int segCount;
        public int segIndex;
        public int errorCode;

        public int isControllerPaused;
        public int isSafetyDriving;
        public int isFreeDriving;
        public int isZeroSpaceFreeDriving;
        public int isRobotHoldBrake;
        public int isProgramRunningOrPause;
        public int isTeachPendantPaused;
        public int isControllerTerminated;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct StrErrorInfo
    {
        public int errorId;
        public int errorType;
        public int errorCode;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string errorMsg;
    }
    
    [StructLayout(LayoutKind.Sequential)]
    public struct StrRobotStateInfo
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        public double[] jointPos;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        public double[] jointAngularVel;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        public double[] jointCurrent;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        public double[] jointTorque;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public double[] tcpPos;

        public double tcpExternalForce;

        [MarshalAs(UnmanagedType.I1)]
        public bool bCollision;

        [MarshalAs(UnmanagedType.I1)]
        public bool bTcpForceValid;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public double[] tcpForce;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        public double[] jointForce;

        public StrTrajectoryState trajState;

        public StrErrorInfo errorInfo;
    }
    
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FNCERRORCALLBACK(int info, [MarshalAs(UnmanagedType.LPStr)] string ip);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FNCSTATECALLBACK(ref StrRobotStateInfo info, [MarshalAs(UnmanagedType.LPStr)] string ip);
    
    [StructLayout(LayoutKind.Sequential)]
    public struct StrCustomStateInfo
    {
        // double[40]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        public double[] dblField;

        // int8_t[160] —— C 中 int8_t 是 signed char，对应 sbyte
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 160)]
        public sbyte[] int8Field;
    }
    
    public enum CustomRobotStateAction
    {
        API_CUSTOM_ADD = 0,    // 定制
        API_CUSTOM_DEL = 1,    // 取消
        API_CUSTOM_RESET = 2   // 重置
    }
    
    
    
    // 对应 C 的 struct _SrvNetSt
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct srv_net_st
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string SrvIp;

        public ushort LocHeartbeatPort;
        public ushort LocRobotStatePort;
        public ushort LocSrvPort;
        public ushort LocRealtimeSrvPort;
        public ushort LocPassThroughSrvPort;
    }
    
    
    // 调用 DLL 中的函数
    [DllImport("DianaApi", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public static extern void initSrvNetInfo(ref srv_net_st info);
    
    
    [DllImport("DianaApi", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public static extern int initSrv(FNCERRORCALLBACK fnError, FNCSTATECALLBACK fnState, ref srv_net_st pinfo);

    [DllImport("DianaApi", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public static extern int destroySrv([MarshalAs(UnmanagedType.LPStr)] string strIpAddress);

    [DllImport("DianaApi", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public static extern int setPushPeriod(int intPeriod, [MarshalAs(UnmanagedType.LPStr)] string strIpAddress);
    
    public enum tcp_direction_e
    {
        T_MOVE_X_UP = 0,
        T_MOVE_X_DOWN = 1,
        T_MOVE_Y_UP = 2,
        T_MOVE_Y_DOWN = 3,
        T_MOVE_Z_UP = 4,
        T_MOVE_Z_DOWN = 5
    }
    public enum coordinate_e
    {
        E_BASE_COORDINATE = 0,
        E_TOOL_COORDINATE = 1,
        E_WORK_PIECE_COORDINATE = 2,
        E_VIEW_COORDINATE = 3
    }
    
    [DllImport("DianaApi", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public static extern int moveTCP(
        tcp_direction_e d,
        double v,
        double a,
        [In, MarshalAs(UnmanagedType.LPArray, SizeConst = 6)] double[] active_tcp,
        [MarshalAs(UnmanagedType.LPStr)] string strIpAddress);

    [DllImport("DianaApi", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public static extern int rotationTCP(
        tcp_direction_e d,
        double v,
        double a,
        [In, MarshalAs(UnmanagedType.LPArray, SizeConst = 6)] double[] active_tcp,
        [MarshalAs(UnmanagedType.LPStr)] string strIpAddress);
    
    public enum joint_direction_e
    {
        T_MOVE_UP = 0,
        T_MOVE_DOWN = 1
    }
    
    [DllImport("DianaApi", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public static extern int moveJoint(
        joint_direction_e d,
        int jointIndex,
        double velocity,
        double acceleration,
        [MarshalAs(UnmanagedType.LPStr)] string ip);

    [DllImport("DianaApi", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public static extern int moveJToTarget(
        [In, MarshalAs(UnmanagedType.LPArray, SizeConst = 7)] double[] joints,
        double velocity,
        double acceleration,
        int zv_shaper_order,
        double zv_shaper_frequency,
        double zv_shaper_damping_ratio,
        [MarshalAs(UnmanagedType.LPStr)] string ip);

    [DllImport("DianaApi", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public static extern int moveJToPose(
        [In, MarshalAs(UnmanagedType.LPArray, SizeConst = 6)] double[] pose,
        double velocity,
        double acceleration,
        [In, MarshalAs(UnmanagedType.LPArray, SizeConst = 6)] double[] active_tcp,
        int zv_shaper_order,
        double zv_shaper_frequency,
        double zv_shaper_damping_ratio,
        [MarshalAs(UnmanagedType.LPStr)] string ip);

    // 宏映射（可封装为别名方法）
    public static int moveJ(double[] joints, double v, double a, int order = 0, double freq = 0, double damping = 0, string ip = "")
        => moveJToTarget(joints, v, a, order, freq, damping, ip);

    public static int moveL(double[] pose, double v, double a, double[] tcp = null, int order = 0, double freq = 0, double damping = 0, string ip = "")
        => moveJToPose(pose, v, a, tcp, order, freq, damping, ip);
    
    
    
    
    
    [DllImport("DianaApi", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public static extern int getJointPos(
        [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 7)] double[] joints,
        [MarshalAs(UnmanagedType.LPStr)] string ip);
    
    
    
}
