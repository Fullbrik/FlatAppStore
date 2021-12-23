using System;
using System.Collections.Generic;
using System.Linq;

namespace FlatAppStore.UI.Framework
{
	public class InputManager : IDisposable
	{
		public IInputDriver CurrentDriver { get; private set; }
		private List<IInputDriver> inputDrivers = new List<IInputDriver>();
		private bool disposedValue;

		public void LoadInputDriver(IInputDriver inputDriver)
		{
			inputDrivers.Add(inputDriver);
			inputDriver.LoadIcons();

			if (CurrentDriver == null) CurrentDriver = inputDriver;
		}

		public void CheckInput(Action<ControllerButton> onKeyDown, Action<float> onScroll)
		{
			// Check for controller input
			var inputsAndDrivers = inputDrivers.Select((driver) => (driver, driver.GetButtonsDown()));

			CurrentDriver = inputsAndDrivers.Last().driver; // Extract the last input device to set as current one.

			var inputs = inputsAndDrivers.Select((id) => id.Item2).SelectMany((b) => b).Distinct();

			foreach (var input in inputs)
			{
				onKeyDown(input);
			}

			// Check for scroll wheel
			float scrollAmount = Raylib_cs.Raylib.GetMouseWheelMove();
			if (scrollAmount != 0) onScroll(scrollAmount);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects)
				}

				// Unload all icons
				foreach (var driver in inputDrivers)
					driver.UnloadIcons();

				// TODO: set large fields to null
				disposedValue = true;
			}
		}

		~InputManager()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: false);
		}

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}