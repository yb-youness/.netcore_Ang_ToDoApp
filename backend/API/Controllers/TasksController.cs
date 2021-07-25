using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Errors;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
     //To Consume this In Application Add The Cors To Satrtup Class  

    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
         //! This dependecy Injection To Inject The Context 
            // The Context Must Be Declared And Configure Withe The Entites(Tables) 
            // It Must Be Configured (Added As Service) In The StarterUp.cs
        private readonly Context _ctx;
        public TasksController(Context ctx)
        {
            _ctx = ctx;
        }

        //! This Methode To Get All Tasks In The Db 
              
        [HttpGet]   // http://localhost:5000/api/tasks   
        public async Task<ActionResult<List<Item>>> GetTask()
        {
            return await _ctx.Items.ToListAsync();
        }
        //! This To Get One Task In The Db

        [HttpGet("{id}")]  // http://localhost:5000/api/tasks/{id}   
        public async Task<ActionResult<Item>> GetOneTask(int id)
        {   
            var Item = await _ctx.Items.FindAsync(id);
            if(Item == null) return NotFound(new ApiResponse(404));
            
            return Ok(Item) ;
        }
      
      //! This Methode Is To Add A TasK
       [HttpPost("AddTask")]  // http://localhost:5000/api/tasks/AddTask   
        public async Task<ActionResult<Item>> AddTask(Item item){
            await _ctx.Items.AddAsync(item);
            await _ctx.SaveChangesAsync();
          return Ok(item);
        }

     [HttpPut("{id}")]
     public async Task<ActionResult<Item>> editTask (int id,Item item){
                     item.Id = id;
                 _ctx.Update(item);
           await _ctx.SaveChangesAsync();
          return Ok(item);
     }


     //! This Methode Is To Delete A Task 
      [HttpDelete("{id}")]   // http://localhost:5000/api/tasks/{id}   
       public async Task<ActionResult<Item>> DeleteTask(int id){
              
              var Item = await _ctx.Items.FindAsync(id);
               if(Item == null) return NotFound(new ApiResponse(404));

                       _ctx.Items.Remove(Item);
                      await  _ctx.SaveChangesAsync(); 
              return Ok(Item);
       }
    }
}