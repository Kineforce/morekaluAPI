using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using morekaluAPI.Models;

namespace morekaluAPI.Controllers
{

    public class PageViewReader {
        public StreamReader reader;
        public string file_path;

        public PageViewReader(string filename){
            this.reader = new StreamReader(filename);
            this.file_path = filename;
        }

        public ViewCount getRecords(){
            string json_string = reader.ReadToEnd();
            ViewCount view = new ViewCount();

            if (json_string != ""){
                view = JsonSerializer.Deserialize<ViewCount>(json_string);
            } 

            view.viewsOnPage ++;
            
            this.incrementView(view);

            return view;

        }

        public bool incrementView(ViewCount _view_count){

            this.writeToJson(this.file_path, _view_count);

            return true;

        }

        public void writeToJson(string file_path, ViewCount serializable){

            File.WriteAllText(file_path, JsonSerializer.Serialize(serializable));

        }

    }


}