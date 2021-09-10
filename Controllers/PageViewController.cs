using Microsoft.AspNetCore.Mvc;
using morekaluAPI.Models;

namespace morekaluAPI.Controllers 
{
    [Route("api/[controller]")]
    [ApiController] 
    public class pageview: ControllerBase {


        [HttpGet("pageview")]
        public ViewCount returnTotalViews(){

            PageViewReader reader = new PageViewReader("Database/db_view_count.json");
            
            return reader.getRecords();

        }
    }

}
