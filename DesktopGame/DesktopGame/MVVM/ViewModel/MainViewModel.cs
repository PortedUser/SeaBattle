using DesktopGame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopGame.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        private object _curVM;

        public object CurrentVM
        {
            get { return _curVM; }
            set { _curVM = value; OnPropertyChanged(); }
        }
        public MainViewModel() 
        {
            CurrentVM = new BattleViewModel();
        }
    }
}
