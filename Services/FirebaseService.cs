using Firebase.Database;
using Interfaces;

namespace Services
{
    public class FirebaseService : IFirebaseService
    {
        private IConfiguration _config;

        public FirebaseService(IConfiguration config)
        {
            _config = config;
        }

        public FirebaseClient GetInstance()
        {
          // TODO: Define secret pela CLI
            string auth = _config["firebase_auth"];
            string baseUrl = _config["firebase_url"];
            //Console.WriteLine("auth"+auth);
            //Console.WriteLine("baseUrl"+baseUrl);

            FirebaseClient firebaseClient = new(
              baseUrl,
              new FirebaseOptions
              {
                  AuthTokenAsyncFactory = () => Task.FromResult(auth)
              });

            return firebaseClient;
        }
    }

}
