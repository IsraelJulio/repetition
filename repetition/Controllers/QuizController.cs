using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace repetition.Controllers
{
    [ApiController]
    [EnableCors("MyCorsImplementation")]
    [Route("[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly IQuizRepository _quizRepository;

        public QuizController(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            try
            {
                var results = _quizRepository.Get().ToList();
                return Ok(results);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            
        }
        [HttpGet("Top/{top}/category/{categoryId}")]
        public IActionResult GetByCategory(int top, int categoryId)
        {
            try
            {

                var questions = _quizRepository.Get().Where(quiz=> quiz?.Category?.Id == categoryId).Select(q=> q.Questions).SelectMany(list => list).OrderBy(x=> x.Rate).ToList();
                var result = new Quiz
                {
                    Id = 0,
                    Title = $"Top {top} spaced repetition",
                    CategoryId = categoryId,
                    Questions = questions,
                };
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
        [HttpGet("Available")]
        public IActionResult GetAvailableCategory()
        {
            try
            {
                var results = _quizRepository.Get().Select(quiz => quiz?.Category).DistinctBy(x=> x.Id).ToList();
                return Ok(results);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var results = _quizRepository.Get().Where(x=> x.Id == id).FirstOrDefault();
                return Ok(results);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            
        }
        [HttpPost()]
        public IActionResult Post([FromBody] Quiz quiz)
        {
            try
            {
                _quizRepository.Create(quiz);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Quiz quiz)
        {
            try
            {
                _quizRepository.Update(quiz);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
             {
                var entity = _quizRepository.GetById(id,CancellationToken.None).Result;
                _quizRepository.Delete(entity);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}