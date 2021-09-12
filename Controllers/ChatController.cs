using Microsoft.AspNetCore.Mvc;
using morekaluAPI.Models;
using System.Collections.Generic;

namespace morekaluAPI.Controllers 
{
    
    [Route("api/[controller]")]
    [ApiController] 
    public class chat : ControllerBase {

        [HttpGet("chat")]
        public List<Message> getMessages(){
            
            ChatReader reader = new ChatReader("Database/db_chat.json");

            return reader.returnAllRecords();

        }

        [HttpGet("chat/total")]
        public int getTotalMessages(){

            ChatReader reader = new ChatReader("Database/db_chat.json");

            return reader.returnTotalRecords();

        }

        [HttpDelete("chat")]
        public bool deleteReview(int _id){
            
            ChatReader reader = new ChatReader("Database/db_chat.json");

            return reader.deleteRecord(_id);
        }

        [HttpPut("chat")]
        public bool updateReview(Message _updated_message){
            ChatReader reader = new ChatReader("Database/db_chat.json");

            return reader.updateRecord(_updated_message);
        }

    }
}