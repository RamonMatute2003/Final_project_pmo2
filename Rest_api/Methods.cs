using Newtonsoft.Json;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Final_project.Rest_api {
    public class Methods {
        public async Task<string> insert_async(object data) {
            string response_insert = null;

            using(HttpClient client = new HttpClient()) {
                var json = "";

                if(data is Table_users user) {
                    json=JsonConvert.SerializeObject(user);
                } else {
                    if(data is Table_records record) {
                        json=JsonConvert.SerializeObject(record);
                    } else {
                        if(data is Table_services service) {
                            json=JsonConvert.SerializeObject(service);
                        }
                    }
                }

                if(json!="") {
                    try {
                        var content = new StringContent(json,Encoding.UTF8,"application/json");

                        var response = await client.PostAsync(Connection_bd.insert_user,content);

                        if(response.IsSuccessStatusCode) {
                            response_insert="exitoso";
                        } else {
                            response_insert="error";
                        }
                    } catch(Exception ex) {
                        response_insert=ex+"";
                    }

                } else {
                    response_insert="JSON null";
                }
            }

            return response_insert;
        }

        public async Task<string> select_async(object data, string url) {
            string json_response = "";
            string json = "";

            using(HttpClient client = new HttpClient()) {
                try {
                    if(data is Table_users user) {
                        json=JsonConvert.SerializeObject(user);
                    } else {
                        if(data is Table_records record) {
                            json=JsonConvert.SerializeObject(record);
                        } else {
                            if(data is Table_services service) {
                                json=JsonConvert.SerializeObject(service);
                            }
                        }
                    }

                    var json_content = new StringContent(json,Encoding.UTF8,"application/json");
                    var response = await client.PostAsync(url,json_content);

                    if(response.IsSuccessStatusCode) {
                        json_response=await response.Content.ReadAsStringAsync();
                    } else {
                        json_response="error";
                    }
                } catch(Exception ex) {
                    json_response=ex.ToString();
                }
            }

            return json_response;
        }

    }
}
