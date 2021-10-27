using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RandomQuiz.Dto;
using RandomQuiz.Dto.Question;
using RandomQuiz.Dto.Tag;
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

        /// <summary>
        /// Gets all tags in the database.
        /// </summary>
        /// <param name="sortBy">Sort by ascending or descending? Default value is ascending.</param>
        /// <param name="pageSize">The number of tags to be sent in a request.</param>
        /// <param name="pageNumber">The number of the current page.</param>
        /// <returns>A collection of tags.</returns>
        [HttpGet("Tags")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTags(SortByEnum? sortBy, int? pageSize, int? pageNumber)
        {
            var result = await service.GetTagsAsync(sortBy, pageSize, pageNumber);
            return Ok(result);
        }

        /// <summary>
        /// Gets the details of a tag.
        /// </summary>
        /// <param name="id">The id of the tag. It is the name of a tag. For example, to get the details of a tag called <c>Math</c> 
        /// send in math as the id.</param>
        /// <returns>A <see cref="TagDetailResponse"/></returns>
        [HttpGet("tags/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTagDetail(string id)
        {
            var tagDetail = await service.GetTagDetailAsync(id);
            if (tagDetail == null)
                return NotFound(id);
            return Ok(tagDetail);
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
