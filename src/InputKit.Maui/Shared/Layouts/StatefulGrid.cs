using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputKit.Shared.Layouts
{
    /// <summary>
    /// This class generated for prevent future errors with VisualStates of Grid. This Grid has VisaulStates like 'Pressed', 'Normal' etc. 
    /// </summary>
    public class StatefulGrid : Grid
    {
        public string DefaultVisualState { get; set; } = VisualStateManager.CommonStates.Normal;

        /// <summary>
        /// Applies pressed effect. You can set another <see cref="void"/> to make custom pressed effects.
        /// </summary>
        public Action<StatefulGrid, bool> ApplyIsPressedAction { get; set; }

        public void GoDefaultVisualState()
        {
            VisualStateManager.GoToState(this, DefaultVisualState);
        }
    }
}
