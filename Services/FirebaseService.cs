using Firebase.Database;
using Firebase.Database.Streaming;
using Interfaces;
using Model;

namespace Services
{
  public class FirebaseService : IFirebaseService
  {
    private IConfiguration _config;

    public FirebaseService(IConfiguration config)
    {
      _config = config;
      /*
         GetInstance(config).Child("dinosaurs").AsObservable<Dinosaur>()
         .Subscribe(d =>
         {
         if (d.EventType == FirebaseEventType.InsertOrUpdate)
         {
         Console.WriteLine("Evento de Insert");
         }
         else if (d.EventType == FirebaseEventType.Delete)
         {
         Console.WriteLine("Evento de Delete");
         }
         });
         */
    }

    public static void MonitoraFirebase(IConfiguration config)
    {
      //TODO: por que a requisição em essa barra entre `Dinosaur` e `.json`
      /*TODO: Mensagem de erro:
        Url: https://fir-874a5-default-rtdb.firebaseio.com/dinosaurs/.json?auth=[firebase_auth]
        Request Data:
        Response:
        ---> System.Net.Http.HttpRequestException: Response status code does not indicate success: 401 (Unauthorized).
      */
      GetInstance(config).Child("dinosaurs").AsObservable<Dinosaur>()
        .Subscribe(d =>
            {
            if (d.EventType == FirebaseEventType.InsertOrUpdate)
            {
            Console.WriteLine("Evento de Insert");
            }
            else if (d.EventType == FirebaseEventType.Delete)
            {
            Console.WriteLine("Evento de Delete");
            }
            });
    }

    public FirebaseClient GetInstance()
    {
      string auth = _config["firebase_auth"];
      string baseUrl = _config["firebase_url"];

      FirebaseClient firebaseClient = new(
          baseUrl,
          new FirebaseOptions
          {
          AuthTokenAsyncFactory = () => Task.FromResult(auth)
          });

      return firebaseClient;
    }

    private static FirebaseClient GetInstance(IConfiguration config)
    {
      string auth = config["firebase_auth"];
      string baseUrl = config["firebase_url"];

      if ((auth is null || baseUrl is null) || (auth == string.Empty || baseUrl == string.Empty))
      {
        throw new Exception("firebase authentication id or url are missing");
      }
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
