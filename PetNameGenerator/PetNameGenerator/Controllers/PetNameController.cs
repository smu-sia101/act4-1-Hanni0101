using Microsoft.AspNetCore.Mvc;
using System;

namespace PetNameGenerator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetNameController : ControllerBase
    {
        private readonly string[] dogName = { "Darius", "Yasuo", "Zed", "Naix", "Huskar" };
        private readonly string[] catName = { "Ahri", "Tristana", "Lux", "Crystal", "Mirana" };
        private readonly string[] birdName = { "Kaisel", "Nami", "Kalea", "Fanny", "Nana" };
        private readonly string[] lastName = { "Corpuz","Reyes","Castro","Andres","Salvador","Antonio", "Mendoza", "Santos", "Smith", "Swift"};

        [HttpPost("generate")]
        public IActionResult Generate([FromBody] PetNameRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.AnimalType))
                return BadRequest(new { message = "animalType is required." });

            string[] names;

            switch (request.AnimalType.ToLower())
            {
                case "dog":
                    names = dogName;
                    break;
                case "cat":
                    names = catName;
                    break;
                case "bird":
                    names = birdName;
                    break;
                default:
                    return BadRequest(new { message = "Not the required animalType. [Must be: dog | cat | bird]" });
            }

            var random = new Random();
            string petName = names[random.Next(names.Length)];
            string lName = lastName[random.Next(lastName.Length)];

            if (request.TwoPart == true)
            {
                petName += " " + lName;
            }
            return Ok(new { petName });
        }
    }

    public class PetNameRequest
    {
        public string AnimalType { get; set; }
        public bool TwoPart { get; set; }
    }
}
