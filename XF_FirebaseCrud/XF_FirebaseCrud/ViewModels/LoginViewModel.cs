using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF_FirebaseCrud.Services;
using XF_FirebaseCrud.Views;

namespace XF_FirebaseCrud.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _Username;
        private string _Password;
        private bool _Result;
        private bool _IsBusy;

        public string Username
        {
            get
            {
                return this._Username;
            }
            set
            {
                this._Username = value;
                OnPropertyChanged();
            }
        }

        //Password
        public string Password
        {
            get
            {
                return this._Password;
            }
            set
            {
                this._Password = value;
                OnPropertyChanged();
            }
        }

        //Método para verificar se o login foi realizado com sucesso
        public bool Result
        {
            get
            {
                return this._IsBusy;
            }
            set
            {
                this._IsBusy = value;
                OnPropertyChanged();
            }
        }

        //Método para verificar se o login está sendo realizado para evitar concorrência
        public bool IsBusy
        {
            get
            {
                return this._Result;
            }
            set
            {
                this._Result = value;
                OnPropertyChanged();
            }
        }

        //Comandos referentes aos botões da tela de Login
        public Command LoginCommand { get; set; }

        public Command RegisterCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await LoginCommandAsync()); //Nome do comando, qualquer um
            RegisterCommand = new Command(async () => await RegisterCommandAsync()); //Nome do comando, qualquer um
        }

        private async Task RegisterCommandAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var userService = new UserServices();
                Result = await userService.RegisterUser(Username, Password);

                if (Result)
                {
                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Usuário Registrado", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Usuário Já Cadastrado", "OK");
                }

            }catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        //Login
        private async Task LoginCommandAsync()
        {
            if (IsBusy)
                return;

            try
            {

                IsBusy = true;
                var userService = new UserServices();
                Result = await userService.LoginUser(Username, Password);

                if (Result)
                {
                    Preferences.Set("Username", Username);
                    await Application.Current.MainPage.Navigation.PushAsync(new ContatosView());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Usuário/Senha Inválidos", "OK");
                }

            }catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
