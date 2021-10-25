using Microsoft.AspNetCore.Mvc;
using Random_Quiz.Interfaces;
using System;
using System.Threading.Tasks;

namespace Random_Quiz.Controllers
{
    [Route("/v1/quiz")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService service;
        //private static bool hasBeenSeeded = false;

        public QuizController(IQuizService service)
        {
            this.service = service;

            // ======  UNCOMMENT AT YOUR OWN RISK!!!  ======
            //  I used the following code to seed the database at first, if you uncomment it, you'd be duplicating the data.
            //
            //
            //if (!hasBeenSeeded)
            //    hasBeenSeeded = service.SetupSeedData().Result;
        }

        [HttpGet("random")]
        public async Task<IActionResult> GetRandomQuestion()
        {
            var question = await service.GetRandomQuestionAsync();

            if (question == null)
                return NotFound();
            return Ok(question);
        }

        [HttpGet("{id:guid}", Name ="GetQuestionById")]
        public async Task<IActionResult> GetQuestionById(Guid id)
        {
            var question = await service.GetQuestionByIdAsync(id);

            if (question == null)
                return NotFound();
            return Ok(question);
        }

        [HttpPost]
        public void Post()
        {

        }
    }
}
