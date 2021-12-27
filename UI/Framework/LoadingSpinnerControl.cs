using System;
using System.Numerics;
using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
	public class LoadingSpinnerControl : Control, IUpdateable
	{
		public override bool PerferExpandToParentWidth => false;
		public override bool PerferExpandToParentHeight => false;

		public float InnerRadius { get; set; } = 90;
		public float MinSpinnerGap { get; set; } = 10;
		public float MaxSpinnerGap { get; set; } = 350;
		public float SpinSpeed { get; set; } = 90f;
		public float GapChangeSpeed { get; set; } = 180;
		public Color Color { get; set; } = Color.WHITE;
		public Vector2 Size { get; set; } = new Vector2(200, 200);

		private float rotation = 0f;
		private float spinnerGap = 0;
		private bool reverseSpinnerGapDirection = false;

		public LoadingSpinnerControl() { }

		public LoadingSpinnerControl(Color color)
		{
			Color = color;
		}

		protected override void Initialized()
		{
			spinnerGap = MinSpinnerGap;

			base.Initialized();

			Updateables.RegisterGlobalUpdateable(this);
		}

		public override void Removed()
		{
			base.Removed();

			Updateables.UnregisterGlobalUpdateable(this);
		}

		public override void Draw()
		{
			Raylib.DrawRing(
				new Vector2(Transform.DrawBounds.x + Transform.DrawBounds.width / 2f, Transform.DrawBounds.y + Transform.DrawBounds.height / 2f),
				InnerRadius,
				(Transform.DrawBounds.width + Transform.DrawBounds.height) / 4f, // Get half of average
				rotation,
				(360 + rotation - spinnerGap),
				50,
				Color);
		}

		public override Vector2 GetSize(Vector2 maxSize)
		{
			return Size;
		}

		public void Update(float deltaTime)
		{
			rotation -= SpinSpeed * deltaTime;

			if (rotation > 360) rotation = 0;
			else if (rotation < 0) rotation = 360;

			if (reverseSpinnerGapDirection) spinnerGap -= SpinSpeed * deltaTime;
			else spinnerGap += GapChangeSpeed * deltaTime;

			if (spinnerGap > MaxSpinnerGap) { spinnerGap = MaxSpinnerGap; reverseSpinnerGapDirection = !reverseSpinnerGapDirection; }
			if (spinnerGap < MinSpinnerGap) { spinnerGap = MinSpinnerGap; reverseSpinnerGapDirection = !reverseSpinnerGapDirection; }
		}
	}
}