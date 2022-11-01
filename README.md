# Firebase .NET Example

## Guide

### Set up Firebase

- Create a Firebase account
  - Create a Firebase project
    - Define autentication
    - Create a Realtime Database
      - [Set Realtime Database's Rules][db-rules]:

```json
{
  "rules": {
    ".read": true,
    ".write": true
  }
}
```

### Set up the code

- [Guide][firebase-dotnet]

- Program.cs

```csharp
builder.Services.AddSingleton<IFirebaseService, FirebaseService>();
```

- FirebaseAuthService.cs

```csharp
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

```

## Reference

[db-rules]: https://stackoverflow.com/a/37404116
[firebase-dotnet]: https://github.com/step-up-labs/firebase-database-dotnet/tree/e7e628af78fd9f1655762911890766d048a3bf46
