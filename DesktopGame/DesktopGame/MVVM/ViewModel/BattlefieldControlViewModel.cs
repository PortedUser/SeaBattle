using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using DesktopGame.Core;
using DesktopGame.Domain.Enum;
using DesktopGame.MVVM.Model;

namespace DesktopGame.MVVM.ViewModel
{
    class BattlefieldControlViewModel : ObservableObject
    {
		private BitmapImage _bowShipSetter;
        private BitmapImage _doubleShipSetter;
        private BitmapImage _threeShipSetter;
        private BitmapImage _fourShipSetter;
        private StateShip _lastStateShip;

        public RelayCommand SetBowShip { get; private set; }
        public RelayCommand SetDoubleShip { get; private set; }
        public RelayCommand SetThreeShip { get; private set; }
        public RelayCommand SetFourShip { get; private set; }

        public StateShip LastStateShip
        {
            get { return _lastStateShip; }
            set { _lastStateShip = value; OnPropertyChanged(); }
        }

        public BitmapImage BowShipSetter
        {
			get { return _bowShipSetter; }
			set { _bowShipSetter = value; }
		}

        public BitmapImage DoubleShipSetter
        {
            get { return _doubleShipSetter; }
            set { _doubleShipSetter = value; }
        }
        public BitmapImage ThreeShipSetter
        {
            get { return _threeShipSetter; }
            set { _threeShipSetter = value;}
        }

        public BitmapImage FourShipSetter
		{
			get { return _fourShipSetter; }
            set { _fourShipSetter = value;}
		}

        public BattlefieldControlViewModel()
		{
			_bowShipSetter = new BitmapImage(GetUri(TypeShip.BowShip));
			_doubleShipSetter = new BitmapImage(GetUri(TypeShip.DoubleDeckShip));
            _threeShipSetter = new BitmapImage(GetUri(TypeShip.ThreeDeckShip));
            _fourShipSetter = new BitmapImage(GetUri(TypeShip.FourDeckShip));
            LastStateShip = new StateShip(TypeShip.BowShip);

            SetBowShip = new RelayCommand(o => 
            { 
                LastStateShip = new StateShip(TypeShip.BowShip);
            });

            SetDoubleShip = new RelayCommand(o =>
            {
                LastStateShip = new StateShip(TypeShip.DoubleDeckShip);
            });

            SetThreeShip = new RelayCommand(o =>
            {
                LastStateShip = new StateShip(TypeShip.ThreeDeckShip);
            });

            SetFourShip = new RelayCommand(o =>
            {
                LastStateShip = new StateShip(TypeShip.FourDeckShip);
            });
        }

		private Uri GetUri(TypeShip state)
		{
			var configManager = new ConfigManager();
			return new Uri(configManager.GetConfig(state.ToString()),UriKind.Relative);
		}
	}
}
