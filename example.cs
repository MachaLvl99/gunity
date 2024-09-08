using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using gunity;
using System.Threading.Tasks;

public class example : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        _ = RunRequest();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private async Task RunRequest() {
        string APIurl = "http://127.0.0.1:11434/api/generate";
        Gemma gemma = new Gemma(APIurl);

        JsonRequest request = new JsonRequest {
            model = "gemma2:2b",
            prompt = "Tell me about the mating rituals of african ants."
        };

        JsonResponse response = await gemma.Post<JsonResponse>(request.jsonify());
        if(response != null) {
            Debug.Log($"Response: {response.response}");
        }
        else {
            Debug.Log("Failed to retrieve a valid response from the API.");
        }
    }
}
