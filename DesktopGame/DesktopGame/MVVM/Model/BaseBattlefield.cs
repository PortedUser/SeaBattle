using DesktopGame.Core;
using DesktopGame.Domain.Interfaces;
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace DesktopGame.MVVM.Model
{
    class BaseBattlefield : ObservableObject, IBattlefield
    {
        private BindingList<BindingList<BattlefieldCell>> _battle;
        private BindingList<BattleCommand> _commands;

        public BindingList<BattleCommand> Commands
        {
            get { return _commands; }
            set { _commands = value;  OnPropertyChanged(); }
        }


        public BaseBattlefield() 
        {
            _battle = new BindingList<BindingList<BattlefieldCell>>();
            _commands = new BindingList<BattleCommand>();

            for (int x = 0; x < 10; x++)
            {
                _battle.Add(new BindingList<BattlefieldCell>());
                for (int y = 0; y < 10; y++)
                {
                    _battle[x].Add(new BattlefieldCell(x, y));
                    _commands.Add(new BattleCommand(x,y));
                }
            }
        }

        public BattlefieldCell this[int index]
        {
            get
            {
                try
                {
                    int x = index / 10;
                    int y = index % 10;
                    return _battle[x][y];
                }
                catch (Exception e)
                {
                    throw GetIndexException(e);
                } 
            }
            set 
            {
                try
                {
                    int x = index / 10;
                    int y = index % 10;
                    _battle[x][y] = value;
                }
                catch (Exception e)
                {
                    throw GetIndexException(e);
                }
            }
        }

        public BattlefieldCell this[int x, int y] 
        {
            get 
            {
                try
                {
                    return _battle[x][y];
                }
                catch (Exception e)
                {
                    throw GetIndexException(e);
                }
            }
            set {
                try
                {
                    _battle[x][y] = value;
                }
                catch (Exception e)
                {
                    throw GetIndexException(e);
                }
            }
        }

        private Exception GetIndexException(Exception e)
        {
            MessageBox.Show($"Не верный индекс при обращении к Battlefield: {e}");
            throw new Exception($"Не верный индекс при обращении к Battlefield: {e}");
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var line in _battle)
            {
                foreach(var cell in line)
                {
                    yield return cell;
                }
            }
        }
    }
}
