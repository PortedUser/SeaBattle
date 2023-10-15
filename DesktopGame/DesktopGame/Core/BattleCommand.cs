using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopGame.Core
{
    class BattleCommand
    {
		private RelayCommand _battleFieldCommand;
        public int X { get; }
		public int Y { get; }

        public RelayCommand Command
		{
			get { return _battleFieldCommand; }
			set { _battleFieldCommand = value; }
		}

		public BattleCommand(int x, int y)
		{
			Y = y;
			X = x;
		}
	}
}
