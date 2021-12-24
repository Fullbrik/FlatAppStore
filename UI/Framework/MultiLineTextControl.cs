using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
	public class MultiLineTextControl : TextControl
	{
		private List<string> lines;
		private float width = 0;

		public override bool PerferExpandToParentWidth => true;
		public override bool PerferExpandToParentHeight => false;

		public MultiLineTextControl() { }

		public MultiLineTextControl(string text)
		{
			Text = text;
		}

		public MultiLineTextControl(string text, int fontSize)
		{
			Text = text;
			FontSize = fontSize;
		}

		public MultiLineTextControl(string text, Color color)
		{
			Text = text;
			Color = color;
		}

		public MultiLineTextControl(string text, int fontSize, Color color)
		{
			Text = text;
			FontSize = fontSize;
			Color = color;
		}

		public override void Draw()
		{
			// Check if we need to recalculate the line width
			if (Transform.DrawBounds.width != width)
			{
				width = Transform.DrawBounds.width;
				ResplitLines();
			}

			// Draw the lines
			float hight = 0; // Keep track of the hight so far so we know where to position lines
			foreach (var line in lines)
			{
				Raylib.DrawTextEx(Font, line, new Vector2(Transform.DrawBounds.x, Transform.DrawBounds.y + hight), FontSize, FontSize / 10, Color);

				hight += Raylib.MeasureTextEx(Font, line, FontSize, FontSize / 10).Y;
			}
		}

		private float lastMaxWidth = -1;
		public override Vector2 GetSize(Vector2 maxSize)
		{
			if (maxSize.X != lastMaxWidth)
			{
				width = maxSize.X;
				ResplitLines();
			}

			return new Vector2(maxSize.X, CalculatePreferredHight());
		}

		private float CalculatePreferredHight()
		{
			float hight = 0;

			foreach (var line in lines)
			{
				hight += Raylib.MeasureTextEx(Font, line, FontSize, FontSize / 10).Y;
			}

			return hight;
		}

		private void ResplitLines()
		{
			lines = SplitTextIntoLines(Font, LocalizedText, width, FontSize, FontSize / 10);
		}

		private List<string> SplitTextIntoLines(Font font, string text, float maxWidth, float fontSize, float spacing)
		{
			var lines = new List<string>();

			string currentLine = "";
			string currentLineSinceLastWhitespace = "";
			int lastWhitespaceIndex = 0;

			for (int i = 0; i < text.Length; i++)
			{
				if (text[i] == '\n') // Force new line
				{
					// Add line
					lines.Add(currentLine);

					// Reset current line
					currentLine = "";
					currentLineSinceLastWhitespace = "";
				}
				else if (char.IsWhiteSpace(text[i]))
				{
					if (Raylib.MeasureTextEx(Font, currentLine, fontSize, spacing).X > maxWidth) // If we are too big for the line, roll back to the last whitespace
					{
						lines.Add(currentLineSinceLastWhitespace);
						i = lastWhitespaceIndex;

						// Reset the current line
						currentLine = "";
						currentLineSinceLastWhitespace = "";
					}
					// Record where the whitespace was and what the current line looked like beforehand so we can roll back there when we run out of horizontal space.
					else
					{
						currentLineSinceLastWhitespace = currentLine;
						lastWhitespaceIndex = i;

						currentLine += text[i];
					}
				}
				else
				{
					currentLine += text[i];

					// var textSize = ;

					// if (textSize.X > maxWidth) // If we have gone past the max width
					// {
					// 	if (string.IsNullOrEmpty(currentLineSinceLastWhitespace)) // If we haven't had a whitepace yet, just move down a line
					// 	{
					// 		lines.Add(currentLine);
					// 	}
					// 	else // Otherwise go back to the last whitespace
					// 	{

					// 	}


					// }
				}
			}

			return lines;
		}
	}
}