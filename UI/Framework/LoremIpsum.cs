namespace FlatAppStore.UI.Framework
{
	public static class LoremIpsum
	{
		private static readonly string[] paragraphs = {
			"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ac ut consequat semper viverra nam libero justo laoreet. Placerat vestibulum lectus mauris ultrices eros in cursus. Sed cras ornare arcu dui vivamus arcu felis bibendum. Aliquam purus sit amet luctus venenatis lectus magna fringilla urna. Risus at ultrices mi tempus imperdiet nulla malesuada pellentesque elit. Vulputate eu scelerisque felis imperdiet proin fermentum. A pellentesque sit amet porttitor eget dolor morbi non. Lorem ipsum dolor sit amet consectetur adipiscing. Maecenas volutpat blandit aliquam etiam erat velit. Elementum curabitur vitae nunc sed velit. Sed lectus vestibulum mattis ullamcorper.",
			"Lectus sit amet est placerat in. Egestas tellus rutrum tellus pellentesque eu. Amet facilisis magna etiam tempor orci eu lobortis elementum nibh. Viverra adipiscing at in tellus integer feugiat scelerisque varius morbi. Neque sodales ut etiam sit amet. Tempus quam pellentesque nec nam aliquam sem et tortor consequat. Sed viverra tellus in hac habitasse. Habitant morbi tristique senectus et netus et malesuada. Volutpat est velit egestas dui id ornare arcu odio ut. Arcu dui vivamus arcu felis bibendum ut tristique et. Ultrices sagittis orci a scelerisque purus semper eget duis. Tincidunt tortor aliquam nulla facilisi cras fermentum odio eu feugiat. Lacus luctus accumsan tortor posuere ac ut. Et egestas quis ipsum suspendisse ultrices gravida dictum fusce. Eu facilisis sed odio morbi quis.",
			"Fringilla urna porttitor rhoncus dolor. Sed arcu non odio euismod lacinia at. Pellentesque elit eget gravida cum sociis natoque penatibus et magnis. Duis at consectetur lorem donec. At imperdiet dui accumsan sit. Purus sit amet luctus venenatis lectus magna. Turpis in eu mi bibendum. Et leo duis ut diam quam nulla porttitor massa. Elit ullamcorper dignissim cras tincidunt lobortis feugiat vivamus at. Quisque non tellus orci ac auctor augue mauris augue neque. Facilisis mauris sit amet massa. Eu non diam phasellus vestibulum. Interdum posuere lorem ipsum dolor sit amet.",
			"Proin fermentum leo vel orci porta non pulvinar. Convallis a cras semper auctor neque vitae tempus quam pellentesque. Orci ac auctor augue mauris augue neque gravida in. Ipsum a arcu cursus vitae. Tortor pretium viverra suspendisse potenti nullam ac tortor vitae. Nibh praesent tristique magna sit amet purus gravida quis blandit. Ornare suspendisse sed nisi lacus sed viverra tellus in hac. Eu lobortis elementum nibh tellus molestie nunc. Egestas congue quisque egestas diam in arcu cursus euismod quis. Nullam ac tortor vitae purus. Malesuada nunc vel risus commodo viverra maecenas accumsan lacus vel. In ornare quam viverra orci. Tellus mauris a diam maecenas sed enim ut sem viverra. Risus ultricies tristique nulla aliquet enim.",
			"Egestas quis ipsum suspendisse ultrices gravida dictum. Quis enim lobortis scelerisque fermentum dui faucibus in ornare quam. Morbi leo urna molestie at elementum eu. Ullamcorper morbi tincidunt ornare massa. Sit amet mattis vulputate enim nulla aliquet porttitor lacus. Nibh venenatis cras sed felis eget. Mauris commodo quis imperdiet massa tincidunt nunc pulvinar sapien et. Auctor augue mauris augue neque gravida in fermentum et sollicitudin. Et odio pellentesque diam volutpat commodo sed egestas egestas fringilla. Posuere lorem ipsum dolor sit amet consectetur adipiscing elit duis. Nunc sed blandit libero volutpat sed cras ornare arcu. Sed euismod nisi porta lorem mollis aliquam ut. Dignissim diam quis enim lobortis scelerisque fermentum. Adipiscing vitae proin sagittis nisl rhoncus mattis rhoncus urna."
		};

		public static string Generate(int numParagraphs)
		{
			if (numParagraphs > paragraphs.Length)
			{
				throw new System.NotImplementedException();
			}
			else
			{
				string retVal = "";

				for (int i = 0; i < numParagraphs; i++)
					retVal += paragraphs[i] + "\n";

				return retVal;
			}
		}
	}
}