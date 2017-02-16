using System;
using System.Windows.Controls;

namespace KinesiologiaMiramar.Helpers
{
    public class SetMainContentEventArgs : EventArgs
    {
        public UserControl UserControl { get; set; }

        public SetMainContentEventArgs(UserControl uc)
        {
            this.UserControl = uc;
        }
    }
}
