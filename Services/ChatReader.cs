using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using morekaluAPI.Models;
using System;

namespace morekaluAPI.Controllers
{

    public class ChatReader {
        public StreamReader reader;
        public string file_path;

        public ChatReader(string filename){
            this.reader = new StreamReader(filename);
            this.file_path = filename;
        }

        public int returnMaxId(List<Message> curr_list_of_messages){

            int max_id = -1;
            foreach (Message message in curr_list_of_messages){
                if (message.id > max_id){
                    max_id = message.id;
                }
            }

            return max_id + 1;

        }

        public int returnTotalRecords(){

            int cnt = 0;
            foreach(Message message in this.returnAllRecords()){  
                cnt++;
            }

            return cnt;

        }

        public bool updateRecord(Message updated_message){

            int message_exists = 0;
            List<Message> list_of_messages = this.returnAllRecords();

            foreach(Message message in list_of_messages){
                if (message.id == updated_message.id){
                    message.text_message = updated_message.text_message;
                                        
                    message_exists += 1;
                }
            }
            
            if (message_exists > 0){
                this.writeToJson(this.file_path, list_of_messages);
            }
            
            return message_exists == 0 ? false : true;

        }

        public int addRecord(Message message){

            List<Message> list_of_message = this.returnAllRecords();

            int curr_max_id = 0;

            curr_max_id = this.returnMaxId(list_of_message);
            message.id = curr_max_id;
            
            list_of_message.Add(message);        
         
            this.writeToJson(this.file_path, list_of_message);

            return curr_max_id;

        }

        public bool deleteRecord(int id_review){

            List<Message> list_of_messages = this.returnAllRecords();
            List<Message> new_list_of_messages = new List<Message>();

            int review_exists = 0;

            foreach (Message review in list_of_messages){
                if (review.id != id_review){
                    new_list_of_messages.Add(review);
                    continue;
                }

                review_exists += 1;
            }

            if (review_exists > 0){
                this.writeToJson(this.file_path, new_list_of_messages);
            }

            return review_exists == 0 ? false : true;

        }

        public List<Message> returnAllRecords(){

            string json_string = reader.ReadToEnd();
            List<Message> json_data = new List<Message>();

            if (json_string != ""){
                json_data = JsonSerializer.Deserialize<List<Message>>(json_string);
                json_data.Reverse();
            } 

            return json_data;

        }

        public void writeToJson(string file_path, List<Message> serializable){

            File.WriteAllText(file_path, JsonSerializer.Serialize(serializable));

        }

    }


}