using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace campusCare.vistasModelos
{
    class HomePacientsViewModel : ObservableObject
    {
        public ICommand historyViewCommand { get; }

        public HomePacientsViewModel()
        {
            historyViewCommand = new AsyncRelayCommand(ExecuteHistoryViewCommandAsync);
        }
        private async Task ExecuteHistoryViewCommandAsync()
        {
            await Shell.Current.GoToAsync("///HistoryPacient");
        }
    }
}
