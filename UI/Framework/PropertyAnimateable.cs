using System;

namespace FlatAppStore.UI.Framework
{
	public class PropertyAnimateable
	{
		public Tween<T> AnimateProperty<T>(string name)
		{
			var prop = GetType().GetProperty(name);

			if (prop.PropertyType != typeof(T)) throw new Exception($"Property {name} is not of type {typeof(T).Name}");

			return AnimateProperty<T>((T)prop.GetValue(this), (value) => prop.SetValue(this, value));
		}

		public Tween<T> AnimateProperty<T>(T value, Action<T> setter)
		{
			return Tween.Property(value, setter).BindUpdated(OnPropertyUpdated<T>);
		}

		protected virtual void OnPropertyUpdated<T>(Tween<T> tween) { }
	}
}