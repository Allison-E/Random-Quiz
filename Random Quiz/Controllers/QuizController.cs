using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RandomQuiz.Dto;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetQuestionById(Guid id)
        {
            var question = await service.GetQuestionByIdAsync(id);

            if (question == null)
                return NotFound();
            return Ok(question);
        }

        [HttpGet("Tags")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTags(SortByEnum? sortBy, int? pageSize, int? pageNumber)
        {
            var result = await service.GetTagsAsync(sortBy, pageSize, pageNumber);
            return Ok(result);
        }
        
        /// <summary>
        /// Creates a new question.
        /// </summary>
        /// <param name="request">The question fields corresponding to the <see cref="CreatedAtActionResult"/> properties.</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateQuestion(CreateQuestionRequest request)
        {
            string createdQuestionId;
            try
            {
                createdQuestionId = await service.CreateQuestionAsync(request);
            }
            catch (OperationCanceledException e)
            {
                return StatusCode(StatusCodes.Status409Conflict, new 
                {
                    Status = 409,
                    Message = e.Message
                });
            }
            var actionName = nameof(GetQuestionById);
            var routeValues = new { id = createdQuestionId };
            return CreatedAtAction(actionName, routeValues, createdQuestionId);
        }
    }
}
