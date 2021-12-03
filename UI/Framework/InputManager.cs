using System;
using System.Collections.Generic;
using System.Linq;

namespace FlatAppStore.UI.Framework
{
	public class InputManager
	{
		public IInputDriver CurrentDriver { get; private set; }
		private List<IInputDriver> inputDrivers = new List<IInputDriver>();

		public void LoadInputDriver(IInputDriver inputDriver)
		{
			inputDrivers.Add(inputDriver);
		}

		public void CheckInput(Action<ControllerButton> onKeyDown)
		{

			var inputsAndDrivers = inputDrivers.Select((driver) => (driver, driver.GetButtonsDown()));

			CurrentDriver = inputsAndDrivers.Last().driver; // Extract the last input device to set as current one.

			var inputs = inputsAndDrivers.Select((id) => id.Item2).SelectMany((b) => b).Distinct();

			foreach (var input in inputs)
			{
				onKeyDown(input);
			}
		}
	}
}