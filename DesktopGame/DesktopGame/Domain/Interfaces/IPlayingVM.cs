using BattlefieldComponents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopGame.Domain.Interfaces
{
    interface IPlayingVM
    {
        public bool Filled { get; }
        public ShotResult Shot(int x, int y);
        public void StartGame();
        public void StopGame();

        public void UpdateVM();
    }
}
