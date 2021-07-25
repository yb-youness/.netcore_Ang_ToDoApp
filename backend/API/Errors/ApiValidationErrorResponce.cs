using System.Collections.Generic;
namespace API.Errors
{
    //! this Class Is To Handel A Invalid Request Sent By the Client A Inavlid post request 
    // it must Derive From ApiResponce Class the First class Crated For Validation 
    // Pass the Satuts Code  of 400 To The base Class 
    // Must Have IEnumerable<String> to Add the Errors 
    // Overide the Normal Flow In The Startup Class in The Configure Methode Note The Order is Important 
    //! the Ovride Must be After the Contorller  
    public class ApiValidationErrorResponce : ApiResponse
    {
        
        public ApiValidationErrorResponce() : base(400)
        {
        }
        public IEnumerable<string> Errors { get; set; }
    }
}