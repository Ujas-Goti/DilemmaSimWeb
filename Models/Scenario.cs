using System.Collections.Generic;
namespace DilemmaSimWeb.Models
{
    public abstract class Scenario
    {
        public string Description { get; set; }
        public List<Choice> Choices { get; set; }
    }

    public class EthicalScenario : Scenario { }
}
