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
    class BaseBattlefield : IBattlefield
    {
        private BindingList<BindingList<BattlefieldCell>> _battle;

        public BattlefieldCell this[int index]
        {
            get
            {
                try
                {
                    return null;
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
