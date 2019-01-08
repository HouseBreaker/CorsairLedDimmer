using System;
using System.Windows.Forms;

namespace CueController
{
	using System.Drawing;
	using System.Linq;
	using System.Resources;
	using CueController.Sdk;
	using CUE.NET.Exceptions;
	using Properties;

	public class TaskTrayApplicationContext : ApplicationContext
	{
		private static readonly NotifyIcon NotifyIcon = new NotifyIcon();

		private static bool isDimmed;

		public TaskTrayApplicationContext()
		{
			var connectedDevices = "None";
			try
			{
				DeviceController.InitializeSdk();
				if (DeviceController.Devices.Any())
				{
					connectedDevices = string.Join(", ", DeviceController.DeviceModels);
				}
			}
			catch (CUEException ex)
			{
				MessageBox.Show($"Error: {ex.Message}");
				Exit(null, null);
			}

			var devicesMenuItem = new MenuItem
			{
				Text = $"Connected Devices: {connectedDevices}",
				Enabled = false
			};

			var menuItems = new[]
			{
				devicesMenuItem,
				new MenuItem("Exit", this.Exit),
			};

			NotifyIcon.Icon = Resources.IconOn;
			NotifyIcon.Click += ToggleDeviceDim;

			NotifyIcon.ContextMenu = new ContextMenu(menuItems);
			NotifyIcon.Visible = true;
		}

		private static void ToggleDeviceDim(object sender, EventArgs e)
		{
			try
			{
				if (!isDimmed)
				{
					DeviceController.PaintDevices(Color.Black);
					NotifyIcon.Icon = Resources.IconOff;
				}
				else
				{
					DeviceController.UninitializeSdk();
					NotifyIcon.Icon = Resources.IconOn;
				}

				isDimmed = !isDimmed;
			}
			catch (CUEException ex)
			{
				MessageBox.Show($"SDK Error: {ex.Message}");
			}
		}

		private void Exit(object sender, EventArgs e)
		{
			NotifyIcon.Visible = false;

			Application.Exit();
		}
	}
}