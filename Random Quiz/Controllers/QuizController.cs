using Microsoft.AspNetCore.Mvc;
using RandomQuiz.Interfaces;
using System;
using System.Threading.Tasks;

namespace RandomQuiz.Controllers
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

        /// <summary>
        /// Gets a random question.
        /// (/v1/quiz/random)
        /// </summary>
        /// <returns></returns>
        [HttpGet("random")]
        public async Task<IActionResult> GetRandomQuestion()
        {
            var question = await service.GetRandomQuestionAsync();

            if (question == null)
                return NotFound();
            return Ok(question);
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestions([FromQuery] string? tag, [FromQuery] int? pageSize, [FromQuery] int? pageNo)
        {
            var questions = await service.GetQuestions(tag, pageSize, pageNo);

            if (questions == null)
                return NotFound();
            return Ok(questions);
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
