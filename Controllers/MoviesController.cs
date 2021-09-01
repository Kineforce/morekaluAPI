using Microsoft.AspNetCore.Mvc;
using morekaluAPI.Models;
using System.Collections.Generic;

namespace morekaluAPI.Controllers 
{
    
    [Route("api/[controller]")]
    [ApiController] 
    public class movies : ControllerBase {

        [HttpGet("reviews")]
        public List<Review> getReviews(){
            
            JsonReader reader = new JsonReader("Database/db_reviews.json");

            return reader.returnAllRecords();

        }

        [HttpPost("reviews")]
        public int postReview(Review _review){

            JsonReader reader = new JsonReader("Database/db_reviews.json");

            return reader.addRecord(_review);
        }

        [HttpDelete("reviews")]
        public bool deleteReview(int _id){
            
            JsonReader reader = new JsonReader("Database/db_reviews.json");

            return reader.deleteRecord(_id);
        }

        [HttpPut("reviews")]
        public bool updateReview(Review _updated_review){
            JsonReader reader = new JsonReader("Database/db_reviews.json");

            return reader.updateRecord(_updated_review);
        }

    }
}