using BattlefieldComponents.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopGame.MVVM.Model
{
    class ButtonState : UIElement
    {
        public RelayCommand Command { get; set; }
        public string Text { get; set; }
        public bool State { get; set; }

        public ButtonState() 
        {
            Command = new RelayCommand(o => { });
            Text = string.Empty;
        }
    }
}
