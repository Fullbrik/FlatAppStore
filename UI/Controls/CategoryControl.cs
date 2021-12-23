using FlatAppStore.UI.Framework;
using Raylib_cs;

namespace FlatAppStore.UI.Controls
{
	public enum ApplicationCategory
	{
		MusicAndVideo,
		Communication,
		Productivity
	}

	public class CategoryControl : FocusableUserControl
	{
		public Color BackgroundColor { get; set; }

		public ApplicationCategory Category { get; set; }
		public string CategoryName => Category switch
		{
			ApplicationCategory.MusicAndVideo => "#category_music_and_video",
			ApplicationCategory.Communication => "#category_communication",
			ApplicationCategory.Productivity => "#category_productivity",
			_ => ""
		};

		public CategoryControl() { }

		public CategoryControl(ApplicationCategory category)
		{
			Category = category;
		}

		protected override Control Build()
		{
			var control = new SimpleDirectionLayoutControl(LayoutDirection.Vertical);

			control.AddChild(new LabelControl(CategoryName));

			return new PaddingControl(control, 10, 10, 10, 10);
		}

		public override void Draw()
		{
			Raylib.DrawRectangleRec(Transform.DrawBounds, BackgroundColor);

			base.Draw();
		}
	}
}