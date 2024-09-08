using System;
using System.Collections;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using UnityEngine;


namespace gunity{
    public interface IResponse {}

    public class BaseModel {
        private protected static readonly HttpClient client = new HttpClient();
        public string url;

        public BaseModel(string url) {
            this.url = url;
        }

        public virtual async Task<T> Post<T>(string req) {
            var content = new StringContent(req, Encoding.UTF8, "application/json");
            Debug.Log("Sending request at endpoint: " + url);
            try {
                var response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<T>(responseContent);
                if(responseObject == null) {throw new Exception("The deserialized response is null."); }
                Debug.Log("Deserialized Response: " + responseContent.ToString());
                return responseObject;
            }
            catch (Exception e) {
                Debug.Log($"HTTP Request Error: {e.Message}");
                return default;
            }
        }
    }

    public class Gemma : BaseModel {
        public Gemma(string url) : base(url) {}
    }

    public class JsonRequest {
        [JsonProperty(Order = 1)] public string model;
        [JsonProperty(Order = 2)] public string prompt;
        [JsonProperty(Order = 3)] public string suffix;
        [JsonProperty(Order = 4)] private string format = "json";
        [JsonProperty(Order = 5)] private bool stream = false;

        public string jsonify() {return JsonConvert.SerializeObject(this); }
    }

    public class JsonResponse : IResponse {
        [JsonProperty(Order = 1)] public string model;
        [JsonProperty(Order = 2)] public string created_at;
        [JsonProperty(Order = 3)] public string response;
        [JsonProperty(Order = 4)] public bool done;
        [JsonProperty(Order = 5)] public int[] context;
        [JsonProperty(Order = 6)] public string total_duration;
        [JsonProperty(Order = 7)] public string load_duration;
        [JsonProperty(Order = 8)] public int prompt_eval_count;
        [JsonProperty(Order = 9)] public string prompt_eval_duration;
        [JsonProperty(Order = 10)] public int eval_count;
        [JsonProperty(Order = 11)] public string eval_duration;
    
        public string jsonify() {return JsonConvert.SerializeObject(this); }
    }

    public class ModelOptions {

    }
}