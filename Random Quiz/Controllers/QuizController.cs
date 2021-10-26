using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RandomQuiz.Dto.Question;
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

        /// <summary>
        /// Gets a collection of questions (either randomly chosen or filtered by tag).
        /// </summary>
        /// <param name="tag">A tag filter.</param>
        /// <param name="pageSize">The required page size of the paginated response.</param>
        /// <param name="pageNo">The page number of the paginated response.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetQuestions([FromQuery] string? tag, [FromQuery] int? pageSize, [FromQuery] int? pageNo)
        {
            var questions = await service.GetQuestionsAsync(tag, pageSize, pageNo);

            if (questions == null)
                return NotFound();
            return Ok(questions);
        }

        /// <summary>
        /// Gets a question by its ID.
        /// </summary>
        /// <param name="id">The question's ID.</param>
        /// <returns></returns>
        [HttpGet("{id:guid}", Name ="GetQuestionById")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetQuestionById(Guid id)
        {
            var question = await service.GetQuestionByIdAsync(id);

            if (question == null)
                return NotFound();
            return Ok(question);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateQuestion(CreateQuestionRequest request)
        {
            var createdQuestionId = await service.CreateQuestionAsync(request);
            var actionName = nameof(GetQuestionById);
            var routeValues = new { id = createdQuestionId };
            return CreatedAtAction(actionName, routeValues, createdQuestionId);
        }
    }
}
