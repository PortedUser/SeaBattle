using BattlefieldComponents.Core;

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
