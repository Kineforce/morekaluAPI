using System.Collections.Generic;
using System;

namespace morekaluAPI.Models
{
    public class Review {
        DateTime localDate = DateTime.Now;

        public int id {get; set;}
        public string movie_name {get; set;}
        public List<string> movie_genres {get; set;} = new List<string>();
        public int movie_release_year {get; set;}
        public string review_text {get; set;}
        public int review_score {get; set;}
        public string review_date {get; set;}

        public Review(){

            this.review_date = localDate.ToString();
        }

    }
}