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

		public BitmapImage BowShipSetter
        {
			get { return _bowShipSetter; }
			set { _bowShipSetter = value; OnPropertyChanged(); }
		}


		public BattlefieldControlViewModel()
		{
			BowShipSetter = new BitmapImage(GetUri(StateCell.BowShip));
        }

		private Uri GetUri(StateCell state)
		{
			var configManager = new ConfigManager();
			return new Uri(configManager.GetConfig(state.ToString()),UriKind.Relative);
		}

	}
}
