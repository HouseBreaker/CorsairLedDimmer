namespace CueController.App
{
	using System.Drawing;
	using System.Threading;
	using Sdk;

	internal class Startup
	{
		public static void Main(string[] args)
		{
			DeviceController.InitializeSdk();

			DeviceController.PaintDevices(Color.Black);

			Thread.Sleep(3000);

			DeviceController.UninitializeSdk();

			while (true)
			{
				Thread.Sleep(1000 * 60);
			}
		}
	}
}