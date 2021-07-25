using API.Data;
using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //* This AContoroller for Testing 
    [ApiController]
    [Route("api/[controller]")]
    public class BugyController : ControllerBase
    {
        public readonly Context _ctx; 
        public BugyController(Context ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("notfound")] // 404
        public ActionResult GetNotFoundRequest()
        {
          //! Explication : Trying To Find A object that Dosent Exsite Wille result in the 
          // var == null 
          // Create A Class For Adding Coutume Errors (Errors/ApiResponse) + A Coustme Contoller (ErrorController) 
          // Add This To The Startup Class In The Configure Methode
                 //  app.UseStatusCodePagesWithReExecute("/errors/{0}");
           var thing = _ctx.Items.Find(44);      
              if(thing == null)
                 return NotFound(new ApiResponse(404));

            return Ok();
        }

        [HttpGet("servererror")] // 500
      public ActionResult GetInteranelSeverError()
        {
          //! Explication : Trying To Do A Ops On object that Dosent Exsite Wille result in the the Internale 
          // Server Error + StakTrace
          // To use This Create A Class (Errors/ApiException) : from A A Base calss Error (ErrorApi)
           var thing = _ctx.Items.Find(44);      
            var objectDosentExite = thing.ToString(); // this well result in Error if the Object Is null 

            return Ok();
        }

     
    [HttpGet("badrequest")] // 400  This For validation A user Submited A Invalid Object 
                                  // In This Case A User My Submite A String In Place Of Int  
      public ActionResult GetBadRequest(int id)
        {
          

            return BadRequest(new ApiResponse(400));
        }
    


    [HttpGet("badrequest/{id}")] 
      public ActionResult GetNotFoundRequest(int id)
        {
           // This Required A Config 
              // 1 Create A Coustme Controller To Handel The Error
              // 2 Add As A Midelware In The Startup in the Configure Methode In The Top 

            return Ok();
        }
    

    }
}