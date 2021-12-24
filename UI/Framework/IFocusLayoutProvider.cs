namespace FlatAppStore.UI.Framework
{
	public interface IFocusLayoutProvider
	{
		FocusableUserControl FocusGetFirst();
		FocusableUserControl FocusGetRight(FocusableUserControl focusableUserControl);
		FocusableUserControl FocusGetLeft(FocusableUserControl focusableUserControl);
		FocusableUserControl FocusGetUp(FocusableUserControl focusableUserControl);
		FocusableUserControl FocusGetDown(FocusableUserControl focusableUserControl);

		void OnChildGetFocus(FocusableUserControl control);
	}
}