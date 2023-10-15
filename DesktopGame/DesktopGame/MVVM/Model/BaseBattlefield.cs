using DesktopGame.Core;
using DesktopGame.Domain.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace DesktopGame.MVVM.Model
{
    class BaseBattlefield : ObservableObject, IBattlefield
    {
        private BindingList<BindingList<BattlefieldCell>> _battle;

        public BaseBattlefield() 
        {
            _battle = new BindingList<BindingList<BattlefieldCell>>();
            for (int x = 0; x < 10; x++)
            {
                _battle.Add(new BindingList<BattlefieldCell>());
                for (int y = 0; y < 10; y++)
                {
                    _battle[x].Add(new BattlefieldCell(x, y));
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
            foreach (var item in _battle)
            {
                foreach(var cell in item)
                {
                    yield return cell;
                }
            }
        }
    }
}
