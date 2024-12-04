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
        public ICommand logOutCommand { get; }

        public ICommand references { get; }

        public HomePacientsViewModel()
        {
            historyViewCommand = new AsyncRelayCommand(ExecuteHistoryViewCommandAsync);
            logOutCommand = new AsyncRelayCommand(ExecuteLogOutCommandAsync);
            references = new AsyncRelayCommand(ExecuteReferencesCommandAsync);

        }
        private async Task ExecuteHistoryViewCommandAsync()
        {
            await Shell.Current.GoToAsync("///AgendarCitas");
        }
        private async Task ExecuteLogOutCommandAsync()
        {
            Preferences.Remove("IdUsuario");
            await Shell.Current.GoToAsync("///Login");
        }
        private async Task ExecuteReferencesCommandAsync()
        {
            await Shell.Current.GoToAsync("///ReferenceView");
        }
    }
}
