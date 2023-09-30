using DesktopGame.Core;
using DesktopGame.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DesktopGame.MVVM.Model
{
    class BattlefieldCell : ObservableObject
    {
		private BitmapImage _curImage;
        private StateCell _state;
        private RelayCommand _command;

        public int X { get; private set; }
        public int Y { get; private set; }

        public BitmapImage CurrentImage
		{
			get { return _curImage; }
            private set { _curImage = value; OnPropertyChanged(); }
		}

		public StateCell CurrentState
		{
			get { return _state; }
		    private	set { _state = value; OnPropertyChanged(); }
		}

		public RelayCommand CurrentCommand
		{
			get { return _command; }
			set { _command = value; OnPropertyChanged(); }
		}


		public BattlefieldCell(int x, int y)
		{
			X = x;
			Y = y;
		}

		public void SetState(StateCell newState)
		{
			CurrentState = newState;
			var configManager = new ConfigManager();
			var uriImage = new Uri(configManager.GetConfig(CurrentState.ToString()), UriKind.Relative);
			CurrentImage = new BitmapImage(uriImage);
		}
    }
}
