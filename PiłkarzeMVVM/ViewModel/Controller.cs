using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PiłkarzeMVVM.ViewModel
{
    using Base;
    using PiłkarzeMVVM.Model;
    using System.Windows;
    using R = Properties.Resources;

    internal class Controller : ViewModelBase
    {
        //call model
        Players players = new Players();

        //form vars
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; } = 20;
        public ObservableCollection<Player> PlayersList { get => new ObservableCollection<Player>(players.PlayersList); }
        public int SelectedIdx { get; set; } = -1;

        //commands
        #region commands
        private ICommand _addPlayer = null;
        public ICommand AddPlayer
        {
            get
            {
                if (_addPlayer == null)
                {
                    _addPlayer = new RelayCommand(
                        arg =>
                        {
                            players.AddPlayer(Name, LastName, Age);
                            onPropertyChanged(nameof(PlayersList));
                            Clear();
                        },
                        arg => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(LastName) && !players.Exists(Name, LastName, Age)
                    );
                }
                return _addPlayer;
            }
        }

        private ICommand _editPlayer = null;
        public ICommand EditPlayer
        {
            get
            {
                if (_editPlayer == null)
                {
                    _editPlayer = new RelayCommand(
                        arg =>
                        {
                            players.EditPlayer(SelectedIdx, Name, LastName, Age);
                            onPropertyChanged(nameof(PlayersList));
                        },
                        arg => SelectedIdx != -1
                    );
                }
                return _editPlayer;
            }
        }

        private ICommand _delPlayer = null;
        public ICommand DelPlayer
        {
            get
            {
                if (_delPlayer == null)
                {
                    _delPlayer = new RelayCommand(
                        arg =>
                        {
                            var alert = MessageBox.Show(R.ConfirmDel, R.Attention, MessageBoxButton.YesNo);
                            if(alert == MessageBoxResult.Yes)
                            {
                                players.Remove(SelectedIdx);
                                onPropertyChanged(nameof(PlayersList));
                                Clear();
                            }
                        },
                        arg => SelectedIdx != -1
                    );
                }
                return _delPlayer;
            }
        }

        private ICommand _loadPlayerData = null;
        public ICommand LoadPlayerData
        {
            get
            {
                if (_loadPlayerData == null)
                {
                    _loadPlayerData = new RelayCommand(
                        arg =>
                        {
                            Player selected = PlayersList[SelectedIdx];
                            Name = selected.Name;
                            LastName = selected.Lastname;
                            Age = selected.Age;

                            onPropertyChanged(nameof(Name), nameof(LastName), nameof(Age));
                        },
                        arg => SelectedIdx != -1
                    );
                }
                return _loadPlayerData;
            }
        }

        private ICommand _clear = null;
        public ICommand ComClear
        {
            get
            {
                if (_clear == null)
                {
                    _clear = new RelayCommand(
                        arg => Clear(),
                        arg => !string.IsNullOrEmpty(Name) || !string.IsNullOrEmpty(LastName)
                    );
                }
                return _clear;
            }
        }

        private ICommand _close = null;
        public ICommand Close
        {
            get
            {
                if (_close == null)
                {
                    _close = new RelayCommand(
                        arg => players.Save(),
                        arg => true
                    );
                }
                return _close;
            }
        }
        #endregion

        private void Clear()
        {
            Name = "";
            LastName = "";
            Age = 20;
            SelectedIdx = -1;
            onPropertyChanged(nameof(Name), nameof(LastName), nameof(Age));
        }

        #region Resources
        public string RName { get => R.Name; }
        public string RLastname { get => R.Lastname; }
        public string RAge { get => R.Age; }
        public string RAdd { get => R.Add; }
        public string REdit { get => R.Edit; }
        public string RDel { get => R.Delete; }
        public string RClear { get => R.Clear; }
        public string RTooltip { get => R.EmptyError; }
        #endregion
    }
}
