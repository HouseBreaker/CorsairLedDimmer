using System.Linq;

namespace CueController.Sdk
{
	using System.Collections.Generic;
	using System.Drawing;
	using System.Runtime.CompilerServices;
	using CUE.NET;
	using CUE.NET.Brushes;
	using CUE.NET.Devices;
	using CUE.NET.Devices.Generic.Enums;
	using CUE.NET.Exceptions;

	public static class DeviceController
	{
		public static ICueDevice[] Devices => CueSDK.InitializedDevices.ToArray();

		public static string[] DeviceModels =>
			Devices?.Select(d => d.DeviceInfo.Model).ToArray() ?? new string[0];

		public static void InitializeSdk()
		{
			CueSDK.Initialize();

			if (!CueSDK.InitializedDevices.Any())
			{
				throw new WrapperException("No devices detected.");
			}
		}

		public static void PaintDevices(Color color)
		{
			var brush = new SolidColorBrush(color);

			CueSDK.UpdateMode = UpdateMode.Continuous;

			foreach (var device in Devices)
			{
				device.Brush = brush;
			}
		}

		public static void UninitializeSdk()
		{
			CueSDK.UpdateMode = UpdateMode.Manual;
			CueSDK.Reinitialize();
		}
	}
}