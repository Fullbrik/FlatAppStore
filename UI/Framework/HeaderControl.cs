using FlatAppStore.UI.Framework.Assets;
using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
	public class HeaderControl : UserControl
	{
		public Color SearchTextColor { get; set; } = Color.GRAY;

		protected override Control Build()
		{
			var control = new SimpleDirectionLayoutControl(LayoutDirection.Horizontal);
			control.DefaultCrossAxisAlignment = CrossAxisAlignment.Center;

			control.AddChild(new PaddingControl(new TextureControl(Icons.Search) { ScaleMode = TextureScaleMode.FitHeight }, 10, 10, 10, 10));
			control.AddChild(new LabelControl("#search", SearchTextColor));

			control.AddChild(new FillerControl());

			return control;
		}
	}
}