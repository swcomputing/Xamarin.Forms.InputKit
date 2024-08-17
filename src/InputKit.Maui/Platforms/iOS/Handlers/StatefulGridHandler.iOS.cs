#if IOS
using Foundation;
using InputKit.Shared.Layouts;
using Microsoft.Maui.Platform;
using System.Diagnostics;
using UIKit;

namespace InputKit.Handlers
{
    public partial class StatefulGridHandler
    {
        protected override void ConnectHandler(LayoutView nativeView)
        {
            nativeView.AddGestureRecognizer(new UIContinousGestureRecognizer(Tapped));

            base.ConnectHandler(nativeView);
        }

        private void Tapped(UIGestureRecognizer recognizer)
        {
            if (VirtualView is StatefulGrid stateful)
            {
                switch (recognizer.State)
                {
                    case UIGestureRecognizerState.Began:

                        VisualStateManager.GoToState(stateful, "Pressed");
                        stateful.ApplyIsPressedAction?.Invoke(stateful, true);

                        break;
                    case UIGestureRecognizerState.Ended:

                        stateful.GoDefaultVisualState();
                        stateful.ApplyIsPressedAction?.Invoke(stateful, false);

                        //// TODO: Fix working of native gesture recognizers of MAUI
                        foreach (var item in stateful.GestureRecognizers)
                        {
                            Debug.WriteLine(item.GetType().Name);
                            if (item is TapGestureRecognizer tgr)
                            {
                                tgr.Command.Execute(stateful);
                            }
                        }

                        break;
                }
            }
        }
    }
}
#endif