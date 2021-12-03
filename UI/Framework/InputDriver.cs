using System.Collections.Generic;
using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
	public interface IInputDriver
	{
		IEnumerable<ControllerButton> GetButtonsDown();
	}

	public class KeyboardInputDriver : IInputDriver
	{
		public IEnumerable<ControllerButton> GetButtonsDown()
		{
			if (Raylib.IsKeyPressed(KeyboardKey.KEY_UP)) yield return ControllerButton.DPAD_Up;
			if (Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN)) yield return ControllerButton.DPAD_Down;
			if (Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT)) yield return ControllerButton.DPAD_Left;
			if (Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT)) yield return ControllerButton.DPAD_Right;

			if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE)) yield return ControllerButton.Face_Down; // A
			if (Raylib.IsKeyPressed(KeyboardKey.KEY_ESCAPE)) yield return ControllerButton.Face_Right; // B
			if (Raylib.IsKeyPressed(KeyboardKey.KEY_X)) yield return ControllerButton.DPAD_Left; // X
			if (Raylib.IsKeyPressed(KeyboardKey.KEY_Y)) yield return ControllerButton.DPAD_Right; // Y
		}
	}
}