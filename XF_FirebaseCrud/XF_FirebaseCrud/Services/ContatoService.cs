using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XF_FirebaseCrud.Models;

namespace XF_FirebaseCrud.Services
{
    public class ContatoService
    {
        FirebaseClient firebase;

        public ContatoService()
        {
            firebase = new FirebaseClient("https://demoxamarinrealtime-b3e1d-default-rtdb.firebaseio.com/");
        }

        public async Task AddContato(int contatoId,string nome,string email)
        {
            await firebase.Child("Contatos")
                .PostAsync(
                new Contato()
                {
                    ContatoId = contatoId,
                    Nome = nome,
                    Email = email
                });
        }

        public async Task<List<Contato>> GetContatos()
        {
            return (await firebase
                .Child("Contatos")
                .OnceAsync<Contato>()).Select(item => new Contato
                {
                    Nome = item.Object.Nome,
                    Email = item.Object.Email,
                    ContatoId = item.Object.ContatoId
                }).ToList();
        }
    }
}
