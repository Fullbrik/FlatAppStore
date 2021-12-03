using System.Collections.Generic;

namespace FlatAppStore.UI.Framework
{
	public interface IUpdateable
	{
		void Update(float deltaTime);
	}

	public static class Updateables
	{
		private static readonly List<IUpdateable> updateables = new List<IUpdateable>();

		public static void RegisterGlobalUpdateable(IUpdateable updateable)
		{
			if (!updateables.Contains(updateable)) updateables.Add(updateable);
		}

		public static void UnregisterGlobalUpdateable(IUpdateable updateable)
		{
			if (updateables.Contains(updateable)) updateables.Remove(updateable);
		}

		public static bool ContainsGlobalUpdateable(IUpdateable updateable)
		{
			return updateables.Contains(updateable);
		}

		public static void UpdateAll(float deltaTime)
		{
			var updateablesArray = updateables.ToArray();
			foreach (var updateable in updateablesArray)
			{
				updateable.Update(deltaTime);
			}
		}
	}
}