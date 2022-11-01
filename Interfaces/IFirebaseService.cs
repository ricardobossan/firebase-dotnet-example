using Firebase.Database;

namespace Interfaces
{
    public interface IFirebaseService
    {
        FirebaseClient GetInstance();
    }
}
