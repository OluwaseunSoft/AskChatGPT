using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;


if (args.Length > 0)
{
    HttpClient client = new HttpClient();
    client.DefaultRequestHeaders.Add("authorization", "Bearer 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");

    var content = new StringContent("{\"model\": \"text-davinci-001\", \"prompt\": \"" + args[0] + "\",\"temperature\": 1,\"max_tokens\": 100}",
    Encoding.UTF8, "application/json");

    HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/completions", content);

    string responseString = await response.Content.ReadAsStringAsync();

    try
    {
        var dyData = JsonConvert.DeserializeObject<dynamic>(responseString);
        
        string guess = GuessCommand(dyData!.choices[0].text);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"--> My Guess At The Command Prompt Is: {guess}");
        Console.ResetColor();
    }
    catch (Exception ex)
    {
        System.Console.WriteLine($"--> Could not deserialise the JSON: {ex.Message}");
    }
    //System.Console.WriteLine(responseString);
}
else
{
    System.Console.WriteLine("--> You need to provide some input ");
}

static string GuessCommand(string raw)
{
    System.Console.WriteLine("--> GPT-3 API Returned Text!");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(raw);

    var lastIndex = raw.LastIndexOf('\n');
    string guess = raw.Substring(lastIndex + 1);
    Console.ResetColor();
    TextCopy.ClipboardService.SetText(guess);
    return guess;
}