using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF_FirebaseCrud.Services;

namespace XF_FirebaseCrud.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContatosView : ContentPage
    {
        ContatoService contatoService = new ContatoService();

        public ContatosView()
        {
            InitializeComponent();
        }

        //OnAppearing é disparado quando a tela é chamada. Se navegou para essa ou está voltando de outra na pilha de telas.
        protected async override void OnAppearing()
        {
            await ExibeContatos();
        }

        private async void btnIncluir_Clicked(object sender, EventArgs e)
        {
            await contatoService.AddContato(Convert.ToInt32(txtId.Text), txtNome.Text, txtEmail.Text);

            txtId.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtNome.Text = string.Empty;

            await DisplayAlert("Sucesso", "Contato Incluído Com Sucesso", "OK");

            await ExibeContatos();
        }

        //Método para exibir o contato

        private async Task ExibeContatos()
        {
            var contatos = await contatoService.GetContatos();

            listaContatos.ItemsSource = contatos;
        }

        private void btnExibir_Clicked(object sender, EventArgs e)
        {

        }

        private void btnAtualizar_Clicked(object sender, EventArgs e)
        {

        }

        private void btnDeletar_Clicked(object sender, EventArgs e)
        {

        }
    }
}