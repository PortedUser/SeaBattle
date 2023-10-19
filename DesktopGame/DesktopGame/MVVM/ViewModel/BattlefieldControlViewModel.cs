using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using DesktopGame.Core;
using DesktopGame.Domain.Enum;

namespace DesktopGame.MVVM.ViewModel
{
    class BattlefieldControlViewModel : ObservableObject
    {
		private BitmapImage _bowShipSetter;
        private BitmapImage _doubleShipSetter;
        private BitmapImage _threeShipSetter;
        private BitmapImage _fourShipSetter;

        public BitmapImage BowShipSetter
        {
			get { return _bowShipSetter; }
			set { _bowShipSetter = value; OnPropertyChanged(); }
		}

        public BitmapImage DoubleShipSetter
        {
            get { return _doubleShipSetter; }
            set { _doubleShipSetter = value; OnPropertyChanged(); }
        }

        public BitmapImage ThreeShipSetter
        {
            get { return _threeShipSetter; }
            set { _threeShipSetter = value; OnPropertyChanged(); }
        }

        public BitmapImage FourShipSetter
		{
			get { return _fourShipSetter; }
			set { _fourShipSetter = value; OnPropertyChanged(); }
		}

        public BattlefieldControlViewModel()
		{
			BowShipSetter = new BitmapImage(GetUri(TypeShip.BowShip));
			DoubleShipSetter = new BitmapImage(GetUri(TypeShip.DoubleDeckShip));
            ThreeShipSetter = new BitmapImage(GetUri(TypeShip.ThreeDeckShip));
            FourShipSetter = new BitmapImage(GetUri(TypeShip.FourDeckShip));
        }

		private Uri GetUri(TypeShip state)
		{
			var configManager = new ConfigManager();
			return new Uri(configManager.GetConfig(state.ToString()),UriKind.Relative);
		}
	}
}
