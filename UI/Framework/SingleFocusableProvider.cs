using System.Numerics;

namespace FlatAppStore.UI.Framework
{
    public class SingleFocusableProvider<T> : IFocusLayoutProvider
        where T : FocusableUserControl
    {
        public T Control { get; set; }

        public IFocusLayoutProvider FocusProviderUp { get; set; }
        public IFocusLayoutProvider FocusProviderDown { get; set; }
        public IFocusLayoutProvider FocusProviderLeft { get; set; }
        public IFocusLayoutProvider FocusProviderRight { get; set; }

        public SingleFocusableProvider(T control)
        {
            Control = control;
            Control.OverideFocusLayoutProvider(this);
        }

        public FocusableUserControl FocusGetFirst()
        {
            return Control;
        }

        public FocusableUserControl FocusGetDown(FocusableUserControl focusableUserControl)
        {
            return FocusProviderDown?.FocusGetFirst();
        }

        public FocusableUserControl FocusGetLeft(FocusableUserControl focusableUserControl)
        {
            return FocusProviderLeft?.FocusGetFirst();
        }

        public FocusableUserControl FocusGetRight(FocusableUserControl focusableUserControl)
        {
            return FocusProviderRight?.FocusGetFirst();
        }

        public FocusableUserControl FocusGetUp(FocusableUserControl focusableUserControl)
        {
            return FocusProviderUp?.FocusGetFirst();
        }

        public void OnChildGetFocus(FocusableUserControl control)
        {

        }
    }
}