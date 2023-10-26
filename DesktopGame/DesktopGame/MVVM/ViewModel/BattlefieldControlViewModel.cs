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
        private BattlefieldCell _rotate;
        private BattlefieldCell _delete;

        public RelayCommand SetBowShip { get; private set; }
        public RelayCommand SetDoubleShip { get; private set; }
        public RelayCommand SetThreeShip { get; private set; }
        public RelayCommand SetFourShip { get; private set; }
        public RelayCommand SetRotate { get; private set; }
        public RelayCommand SetDelete { get; private set; }


        public BattlefieldCell Delete
        {
            get { return _delete; }
            set { _delete = value; OnPropertyChanged(); }
        }


        public BattlefieldCell Rotate
        {
            get { return _rotate; }
            set { _rotate = value;  OnPropertyChanged(); }
        }

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
            Rotate = new BattlefieldCell(0, 0);
            Delete = new BattlefieldCell(0, 0);
            Delete.SetFullState(StateCell.Delete);
            Rotate.SetFullState(StateCell.Rotate);

            SetBowShip = new RelayCommand(o => 
            { 
                LastStateShip.CurrentType = TypeShip.BowShip;
            });

            SetDoubleShip = new RelayCommand(o =>
            {
                LastStateShip.CurrentType = TypeShip.DoubleDeckShip;
            });

            SetThreeShip = new RelayCommand(o =>
            {
                LastStateShip.CurrentType = TypeShip.ThreeDeckShip;
            });

            SetFourShip = new RelayCommand(o =>
            {
                LastStateShip.CurrentType = TypeShip.FourDeckShip;
            });

            SetDelete = new RelayCommand(o =>
            {
                LastStateShip.CurrentType = TypeShip.Delete;
            });



            SetRotate = new RelayCommand(o =>
            {
                LastStateShip.AngleRotation = LastStateShip.AngleRotation == 0 ? 90 : 0;
                if (Rotate.CurrentState == StateCell.Rotate)
                    Rotate.SetFullState(StateCell.Rotate_90);
                else
                    Rotate.SetFullState(StateCell.Rotate);
            });
        }

        public StateShip GetLastState()
        {
            return _lastStateShip;
        }

		private Uri GetUri(TypeShip state)
		{
			var configManager = new ConfigManager();
			return new Uri(configManager.GetConfig(state.ToString()),UriKind.Relative);
		}
	}
}
