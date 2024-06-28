using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalculationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MathOperationsController : ControllerBase
    {
        /// <summary>
        /// Calculation request view model
        /// </summary>
        public class Request
        {
            /// <summary>
            /// Operand
            /// </summary>
            public string Operand { get; set; }

            /// <summary>
            /// Numbers
            /// </summary>
            public List<double> Numbers { get; set; }
        }

        /// <summary>
        /// Response view model
        /// </summary>
        public class Response
        {
            public double Result { get; set; }
        }

        /// <summary>
        /// Method to calculte based on the given operand
        /// </summary>
        /// <param name="request">request containing operand and numbers</param>
        /// <returns>Result of the calculation</returns>
        [HttpPost]
        public IActionResult Calculate([FromBody] Request request)
        {
            if (request.Numbers == null || request.Numbers.Count == 0)
            {
                return BadRequest("Numbers cannot be null or empty.");
            }

            double result;

            switch (request.Operand)
            {
                case "+":
                    result = 0;
                    foreach (var number in request.Numbers)
                    {
                        result += number;
                    }
                    break;
                case "-":
                    result = request.Numbers[0];
                    for (int i = 1; i < request.Numbers.Count; i++)
                    {
                        result -= request.Numbers[i];
                    }
                    break;
                case "*":
                    result = 1;
                    foreach (var number in request.Numbers)
                    {
                        result *= number;
                    }
                    break;
                case "/":
                    result = request.Numbers[0];
                    for (int i = 1; i < request.Numbers.Count; i++)
                    {
                        if (request.Numbers[i] == 0)
                        {
                            return BadRequest("Cannot divide by zero.");
                        }
                        result /= request.Numbers[i];
                    }
                    break;
                default:
                    return BadRequest("Invalid operand.");
            }

            return Ok(new Response { Result = result });
        }
    }
}
