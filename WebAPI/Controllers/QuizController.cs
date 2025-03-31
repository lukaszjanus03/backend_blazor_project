using BackendLab01;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTO;

namespace WebAPI.Controllers
{
    [Route("api/v1/quizzes")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizUserService _service;
        
        public QuizController(IQuizUserService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public IEnumerable<QuizDto> FindAll()
        {
            return  _service.FindAllQuizzes().Select(u=>QuizDto.of(u));
        }
        
        [HttpGet]
        [Route("{id}")]
        public ActionResult<QuizDto> FindById(int id)
        {

            if (_service.FindQuizById(id) is null)
                return NotFound();
            return Ok(QuizDto.of(_service.FindQuizById(id)));
        } 
        
        [HttpPost]
        [Route("{quizId}/items/{itemId}")]
        public IActionResult SaveAnswer(int quizId, int itemId, [FromBody] QuizItemAnswerDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid request body");
            }

            _service.SaveUserAnswerForQuiz(quizId, itemId, dto.UserId, dto.Answer);
    
            return Ok("Answer saved successfully");
        }
        
        
        [HttpGet]
        [Route("{quizId}/answers/{userId}")]
        public ActionResult<object> GetQuizFeedback(int quizId, int userId)
        {
            var feedback = _service.GetUserAnswersForQuiz(quizId, userId);
            return new
            {
                quizId = quizId,
                userId = userId,
                totalQuestions = _service.FindQuizById(quizId)?.Items.Count??0,
                answers = feedback.Select(a =>
                    new
                    {
                        question = a.QuizItem.Question,
                        answer = a.Answer,
                        isCorrect = a.IsCorrect()
                    }
                ).AsEnumerable()
            };
        }
    }
}

