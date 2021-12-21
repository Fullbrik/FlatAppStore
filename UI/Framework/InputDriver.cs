using System.Collections.Generic;
using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
	public interface IInputDriver
	{
		void LoadIcons();
		void UnloadIcons();
		IEnumerable<ControllerButton> GetButtonsDown();

		Texture2D GetButtonIcon(ControllerButton button);
	}

	public class KeyboardInputDriver : IInputDriver
	{
		private Dictionary<ControllerButton, Texture2D> icons;

		public void LoadIcons()
		{
			icons = new Dictionary<ControllerButton, Texture2D>
			{
				[ControllerButton.DPAD_Up] = Assets.AssetLoader.LoadTexture2D("/Assets/ControllerIcons/Keyboard/Arrow_Up_Key_Dark.png"),
				[ControllerButton.DPAD_Down] = Assets.AssetLoader.LoadTexture2D("/Assets/ControllerIcons/Keyboard/Arrow_Down_Key_Dark.png"),
				[ControllerButton.DPAD_Right] = Assets.AssetLoader.LoadTexture2D("/Assets/ControllerIcons/Keyboard/Arrow_Right_Key_Dark.png"),
				[ControllerButton.DPAD_Left] = Assets.AssetLoader.LoadTexture2D("/Assets/ControllerIcons/Keyboard/Arrow_Left_Key_Dark.png"),

				[ControllerButton.Face_Down] = Assets.AssetLoader.LoadTexture2D("/Assets/ControllerIcons/Keyboard/Space_Key_Dark.png"),
				[ControllerButton.Face_Right] = Assets.AssetLoader.LoadTexture2D("/Assets/ControllerIcons/Keyboard/Esc_Key_Dark.png")
			};
		}

		public void UnloadIcons()
		{
			foreach (var texture in icons.Values)
				Assets.AssetLoader.UnloadTexture2D(texture);
		}

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

		public Texture2D GetButtonIcon(ControllerButton button)
		{
			if (icons.ContainsKey(button))
				return icons[button];
			else
				throw new System.Exception("Could not find icon for button " + button.ToString());
		}
	}
}