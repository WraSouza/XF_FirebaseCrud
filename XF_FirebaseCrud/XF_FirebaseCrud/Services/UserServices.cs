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
    public class UserServices
    {
        FirebaseClient client;

        public UserServices()
        {
            client = new FirebaseClient("https://demoxamarinrealtime-b3e1d-default-rtdb.firebaseio.com/");
        }

        public async Task<bool> IsUSerExists(string name)
        {
            var user = (await client.Child("Users")
                .OnceAsync<User>())
                .Where(u => u.Object.Username == name)
                .FirstOrDefault();

            return (user != null);
        }

        public async Task<bool> RegisterUser(string name, string password)
        {
            if(await IsUSerExists(name) == false)
            {
                await client.Child("Users")
                    .PostAsync(new User()
                    {
                        Username = name,
                        Password = password
                    });
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> LoginUser(string name, string passwd)
        {
            var user = (await client.Child("Users")
                .OnceAsync<User>())
                .Where(u => u.Object.Username == name)
                .Where(u => u.Object.Password == passwd)
                .FirstOrDefault();

            return (user != null);
        }
    }
}
