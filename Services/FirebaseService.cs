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
