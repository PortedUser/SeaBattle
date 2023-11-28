using BattlefieldComponents.Core;
using DesktopGame.MVVM.Model;
using System.Windows;

namespace DesktopGame.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        private object _curVM;
        public ButtonState CloseApp { get; set; }
        public ButtonState HideApp { get; set; }
        public ButtonState MaxApp { get; set; }

        public object CurrentVM
        {
            get { return _curVM; }
            set { _curVM = value; OnPropertyChanged(); }
        }
        public MainViewModel() 
        {
            CloseApp = new ButtonState();
            HideApp = new ButtonState();
            MaxApp = new ButtonState();

            CloseApp.Command = new RelayCommand(o => { Application.Current.Shutdown(); });
            HideApp.Command = new RelayCommand(o => { Application.Current.MainWindow.WindowState = WindowState.Minimized; });
            MaxApp.Command = new RelayCommand(o => { Application.Current.MainWindow.WindowState = WindowState.Maximized; });

            _curVM = new BattleViewModel();
        }
    }
}
