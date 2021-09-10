using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using morekaluAPI.Models;
using System;

namespace morekaluAPI.Controllers
{

    public class MoviesReader {
        public StreamReader reader;
        public string file_path;

        public MoviesReader(string filename){
            this.reader = new StreamReader(filename);
            this.file_path = filename;
        }

        public int returnMaxId(List<Review> curr_list_of_reviews){

            int max_id = -1;
            foreach (Review review in curr_list_of_reviews){
                if (review.id > max_id){
                    max_id = review.id;
                }
            }

            return max_id + 1;

        }

        public int returnTotalRecords(){

            int cnt = 0;
            foreach(Review review in this.returnAllRecords()){  
                cnt++;
            }

            return cnt;

        }

        public bool updateRecord(Review updated_review){

            int review_exists = 0;
            List<Review> list_of_reviews = this.returnAllRecords();

            foreach(Review review in list_of_reviews){
                if (review.id == updated_review.id){
                    review.movie_name = updated_review.movie_name;
                    review.movie_genres = updated_review.movie_genres;
                    review.movie_release_year = updated_review.movie_release_year;
                    review.review_text = updated_review.review_text;
                    review.review_score = updated_review.review_score;
                    review.review_date = updated_review.review_date;

                    review_exists += 1;
                }
            }
            
            if (review_exists > 0){
                this.writeToJson(this.file_path, list_of_reviews);
            }
            
            return review_exists == 0 ? false : true;

        }

        public int addRecord(Review review){

            List<Review> list_of_reviews = this.returnAllRecords();

            int curr_max_id = 0;

            curr_max_id = this.returnMaxId(list_of_reviews);
            review.id = curr_max_id;
            
            list_of_reviews.Add(review);        
         
            this.writeToJson(this.file_path, list_of_reviews);

            return curr_max_id;

        }

        public bool deleteRecord(int id_review){

            List<Review> list_of_reviews = this.returnAllRecords();
            List<Review> new_list_of_reviews = new List<Review>();

            int review_exists = 0;

            foreach (Review review in list_of_reviews){
                if (review.id != id_review){
                    new_list_of_reviews.Add(review);
                    continue;
                }

                review_exists += 1;
            }

            if (review_exists > 0){
                this.writeToJson(this.file_path, new_list_of_reviews);
            }

            return review_exists == 0 ? false : true;

        }

        public List<Review> returnAllRecords(){

            string json_string = reader.ReadToEnd();
            List<Review> json_data = new List<Review>();

            if (json_string != ""){
                json_data = JsonSerializer.Deserialize<List<Review>>(json_string);
                json_data.Reverse();
            } 

            return json_data;

        }

        public void writeToJson(string file_path, List<Review> serializable){

            File.WriteAllText(file_path, JsonSerializer.Serialize(serializable));

        }

    }


}