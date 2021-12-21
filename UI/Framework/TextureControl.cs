using System.Numerics;
using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
	public enum TextureScaleMode
	{
		None,
		FitWidth,
		FitHeight,
	}

	public class TextureControl : Control
	{
		public override bool PerferExpandToParentWidth => ScaleMode == TextureScaleMode.FitWidth;
		public override bool PerferExpandToParentHeight => ScaleMode == TextureScaleMode.FitHeight;

		public Texture2D Texture { get; set; }
		public TextureScaleMode ScaleMode { get; set; } = TextureScaleMode.None;
		public Color Color { get; set; } = Color.WHITE;

		public TextureControl() { }

		public TextureControl(Texture2D texture)
		{
			Texture = texture;
		}

		public override void Draw()
		{
			Raylib.DrawTexturePro(
				Texture,
				new Rectangle(0, 0, Texture.width, Texture.height),
				Transform.DrawBounds,
				Vector2.Zero,
				0,
				Color
			);
		}

		public override Vector2 GetSize(Vector2 maxSize)
		{
			float scale = 1;

			switch (ScaleMode)
			{
				case TextureScaleMode.FitWidth:
					scale = maxSize.X / Texture.width;
					var height = Texture.height * scale;
					return new Vector2(maxSize.X, height);

				case TextureScaleMode.FitHeight:
					scale = maxSize.Y / Texture.height;
					var width = Texture.width * scale;
					return new Vector2(width, maxSize.Y);

				case TextureScaleMode.None:
				default:
					return new Vector2(Texture.width, Texture.height);
			}
		}

		// private Rectangle GetScaledBounds()
		// {
		// 	return ScaleMode switch
		// 	{
		// 		TextureScaleMode.FitHeight => new Rectangle(Transform.Bounds.x, Transform.Bounds.y, Texture.width * (Parent.Transform.Bounds.height / Texture.height), Parent.Transform.Bounds.height),
		// 		TextureScaleMode.None or _ => Transform.Bounds
		// 	};
		// }

		// public override Vector2 GetMinPreferredSize()
		// {
		// 	float scale = ScaleMode switch
		// 	{
		// 		TextureScaleMode.FitWidth => Transform.Bounds.width != 0 ? Parent.Transform.Bounds.width / Texture.width : 1,
		// 		TextureScaleMode.FitHeight => Parent.Transform.Bounds.height / Texture.height,
		// 		TextureScaleMode.None or _ => 1
		// 	};

		// 	System.Console.WriteLine("Scale: Width: {0}, Height: {1}", Texture.width * scale, Texture.height * scale);

		// 	return new Vector2(Texture.width, Texture.height);
		// }
	}
}