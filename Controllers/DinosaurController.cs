using Firebase.Database;
using Firebase.Database.Query;
using Interfaces;
using Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FirebaseAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DinosaurController : ControllerBase
    {
        private IFirebaseService _firebaseService;
        private FirebaseClient _firebaseClient;

        public DinosaurController(IFirebaseService firebaseService)
        {
            _firebaseClient = firebaseService.GetInstance();
       }

        // GET: api/<DinosaurController>
        [HttpGet]
        public async Task<List<Dinosaur>> Get()
        {
            //var dinos = new List<Dinosaur>();
            var dinos = await _firebaseClient
                .Child("dinosaurs")
                .OrderByKey()
                .StartAt("pterodactyl")
                .LimitToFirst(10)
                .OnceAsync<Dinosaur>();

            //return new string[] { "value1", "value2" };
            var retorno = new List<Dinosaur>();
            foreach (var d in dinos)
            {
                retorno.Add(d.Object);
            }
            return retorno;
        }

        // GET api/<DinosaurController>/5
        [HttpGet("{name}")]
        public async Task<Dinosaur> Get(string name)
        {
            Dinosaur dino = new();

            try
            {
                var dinos = await _firebaseClient
                    .Child("dinosaurs")
                    .OrderByKey()
                    .OnceAsync<Dinosaur>();


                dino = dinos.FirstOrDefault(x => x.Key == name).Object;

                Console.WriteLine($"Returned {name}");
            }
            catch
            {
                Console.WriteLine($"Failed to return {name}");
            }

            return dino;
        }

        // POST api/<DinosaurController>
        [HttpPost]
        public async void Post([FromBody] Dinosaur value)
        {
            var dino = await _firebaseClient
                .Child("dinosaurs")
                .Child("randomsaur3")
                .PostAsync(value);

            Console.WriteLine($"{dino.Key} posted");
        }

        // PUT api/<DinosaurController>/5
        [HttpPut("{name}")]
        public async void Put(string name, [FromBody] Dinosaur value)
        {
            try
            {
                await _firebaseClient
                    .Child("dinosaurs")
                    .Child(name)
                    .PutAsync(value);

                Console.WriteLine($"{name} updated");
            }
            catch
            {
                Console.WriteLine($"{name} not updated");
            }
        }

        // DELETE api/<DinosaurController>/5
        [HttpDelete("{name}")]
        public async void Delete(string name)
        {
            try
            {
                await _firebaseClient
                    .Child("dinosaurs")
                    .Child(name)
                    .DeleteAsync();
                Console.WriteLine($"{name} deleted");
            }
            catch
            {
                Console.WriteLine($"{name} not deleted");
            }
        }
    }
}
