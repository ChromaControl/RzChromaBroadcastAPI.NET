using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Razer.Chroma.Broadcast
{

	#region Implementation

	/// <summary>
	/// Base Interface for both 32 bit and 64 bit
	/// </summary>
	internal interface IRzChromaBroadcastAPINative
	{
		RzResult Init(Guid guid);

		RzResult UnInit();
		RzResult RegisterEventNotification(RzChromaBroadcastAPINative.RegisterEventNotificationCallback callback);
		RzResult UnRegisterEventNotification();
	}

	/// <summary>
	/// 32 Bit Implementation
	/// </summary>
	internal class RzChromaBroadcastAPINative32 : IRzChromaBroadcastAPINative
	{
		RzResult IRzChromaBroadcastAPINative.Init(Guid guid) => Init(guid);

		RzResult IRzChromaBroadcastAPINative.RegisterEventNotification(RzChromaBroadcastAPINative.RegisterEventNotificationCallback callback) => RegisterEventNotification(callback);

		RzResult IRzChromaBroadcastAPINative.UnInit() => UnInit();

		RzResult IRzChromaBroadcastAPINative.UnRegisterEventNotification() => UnRegisterEventNotification();


		[DllImport("RzChromaBroadcastAPI.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern RzResult Init(Guid guid);

		[DllImport("RzChromaBroadcastAPI.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern RzResult UnInit();

		[DllImport("RzChromaBroadcastAPI.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern RzResult RegisterEventNotification(RzChromaBroadcastAPINative.RegisterEventNotificationCallback callback);

		[DllImport("RzChromaBroadcastAPI.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern RzResult UnRegisterEventNotification();
	}

	/// <summary>
	/// 64 Bit Implementation
	/// </summary>
	internal class RzChromaBroadcastAPINative64 : IRzChromaBroadcastAPINative
	{
		RzResult IRzChromaBroadcastAPINative.Init(Guid guid) => Init(guid);

		RzResult IRzChromaBroadcastAPINative.RegisterEventNotification(RzChromaBroadcastAPINative.RegisterEventNotificationCallback callback) => RegisterEventNotification(callback);

		RzResult IRzChromaBroadcastAPINative.UnInit() => UnInit();

		RzResult IRzChromaBroadcastAPINative.UnRegisterEventNotification() => UnRegisterEventNotification();

		// No need to specify native calling conventions on x86_64 as it is always fastcall even if otherwise specified

		[DllImport("RzChromaBroadcastAPI64.dll")]
		public static extern RzResult Init(in Guid guid); // 64-bit version expects a Guid* not a Guid 

		[DllImport("RzChromaBroadcastAPI64.dll")]
		public static extern RzResult UnInit();

		[DllImport("RzChromaBroadcastAPI64.dll")]
		public static extern RzResult RegisterEventNotification(RzChromaBroadcastAPINative.RegisterEventNotificationCallback callback);

		[DllImport("RzChromaBroadcastAPI64.dll")]
		public static extern RzResult UnRegisterEventNotification();
	}
	#endregion

	internal class RzChromaBroadcastAPINative
	{
		// On x86_64 the calling convention is always fastcall even if otherwise specified, so it's fine to use cdecl here.
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int RegisterEventNotificationCallback(int message, IntPtr data);

		// Get the native api for the right architecture.
		private static readonly IRzChromaBroadcastAPINative _native = IntPtr.Size == 8 ? (IRzChromaBroadcastAPINative)new RzChromaBroadcastAPINative64() : new RzChromaBroadcastAPINative32();

		// Pass all calls to the appropriate Native class for this architecture.

		public static RzResult Init(Guid guid) => _native.Init(guid);
		public static RzResult UnInit() => _native.UnInit();
		public static RzResult RegisterEventNotification(RegisterEventNotificationCallback callback) => _native.RegisterEventNotification(callback);
		public static RzResult UnRegisterEventNotification() => _native.UnRegisterEventNotification();
	}
}
