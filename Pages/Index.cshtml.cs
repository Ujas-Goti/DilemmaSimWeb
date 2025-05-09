using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DilemmaSimWeb.Models;
using System.Collections.Generic;

namespace DilemmaSimWeb.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public int SelectedChoice { get; set; }

        public EthicalScenario? CurrentScenario { get; set; }

        public static Player Player { get; set; } = new Player();
        public static string Feedback { get; set; } = string.Empty;

        public static int ScenarioIndex = 0;

        [TempData]
        public bool ShowConsequence { get; set; }



        public static List<EthicalScenario> Scenarios = new()
        {
            new EthicalScenario
            {
                Description = "You find a wallet on the street with cash and an ID inside.",
                Choices = new List<Choice>
                {
                    new() { Text = "Return it to the owner.", HonestyScore = 2, EmpathyScore = 1, Consequence = "Owner is grateful." },
                    new() { Text = "Keep the cash.", HonestyScore = -2, EmpathyScore = -1, Consequence = "You feel guilty." },
                    new() { Text = "Give it to the police.", HonestyScore = 1, EmpathyScore = 0, Consequence = "You stay neutral." }
                }
            },
            new EthicalScenario
            {
                Description = "Your friend asks you to lie for them to cover up a mistake.",
                Choices = new List<Choice>
                {
                    new() { Text = "Refuse to lie.", HonestyScore = 2, EmpathyScore = 0, Consequence = "They’re disappointed but you kept your integrity." },
                    new() { Text = "Agree to lie.", HonestyScore = -2, EmpathyScore = 1, Consequence = "They appreciate it, but you feel wrong." },
                    new() { Text = "Avoid answering.", HonestyScore = 0, EmpathyScore = 0, Consequence = "You're not involved, but not helpful either." }
                }
            },
            new EthicalScenario
            {
                Description = "You witness a colleague stealing supplies at work.",
                Choices = new List<Choice>
                {
                    new() { Text = "Report them.", HonestyScore = 2, EmpathyScore = 0, Consequence = "Justice is served." },
                    new() { Text = "Talk to them privately.", HonestyScore = 1, EmpathyScore = 1, Consequence = "They return the item." },
                    new() { Text = "Ignore it.", HonestyScore = -1, EmpathyScore = 0, Consequence = "Theft continues." }
                }
            },
            new EthicalScenario
            {
                Description = "You see someone being bullied online.",
                Choices = new List<Choice>
                {
                    new() { Text = "Stand up for them.", HonestyScore = 1, EmpathyScore = 2, Consequence = "They thank you." },
                    new() { Text = "Report the bully.", HonestyScore = 2, EmpathyScore = 1, Consequence = "The bully gets banned." },
                    new() { Text = "Scroll past.", HonestyScore = -1, EmpathyScore = -1, Consequence = "The victim feels alone." }
                }
            },
            new EthicalScenario
            {
                Description = "You're given credit for a group project you didn’t do much on.",
                Choices = new List<Choice>
                {
                    new() { Text = "Speak up about your role.", HonestyScore = 2, EmpathyScore = 1, Consequence = "The team appreciates your honesty." },
                    new() { Text = "Say nothing.", HonestyScore = -1, EmpathyScore = 0, Consequence = "You keep the credit." },
                    new() { Text = "Privately thank the group.", HonestyScore = 0, EmpathyScore = 1, Consequence = "They feel acknowledged." }
                }
            },
            new EthicalScenario
            {
                Description = "You find test answers before an exam.",
                Choices = new List<Choice>
                {
                    new() { Text = "Ignore them and report it.", HonestyScore = 2, EmpathyScore = 0, Consequence = "Fair play is restored." },
                    new() { Text = "Use them to help you study.", HonestyScore = -1, EmpathyScore = 0, Consequence = "You pass but feel uncertain." },
                    new() { Text = "Share them with friends.", HonestyScore = -2, EmpathyScore = -1, Consequence = "You’re all at risk." }
                }
            },
            new EthicalScenario
            {
                Description = "A stranger drops their groceries.",
                Choices = new List<Choice>
                {
                    new() { Text = "Help them pick up.", HonestyScore = 0, EmpathyScore = 2, Consequence = "They’re very thankful." },
                    new() { Text = "Walk past.", HonestyScore = 0, EmpathyScore = -1, Consequence = "They struggle alone." },
                    new() { Text = "Laugh and move on.", HonestyScore = -1, EmpathyScore = -2, Consequence = "You made it worse." }
                }
            },
            new EthicalScenario
            {
                Description = "You can volunteer for a charity event.",
                Choices = new List<Choice>
                {
                    new() { Text = "Sign up to help.", HonestyScore = 1, EmpathyScore = 2, Consequence = "You made a difference." },
                    new() { Text = "Ignore the request.", HonestyScore = 0, EmpathyScore = 0, Consequence = "Nothing happens." },
                    new() { Text = "Discourage others from going.", HonestyScore = -1, EmpathyScore = -1, Consequence = "They lose volunteers." }
                }
            },
            new EthicalScenario
            {
                Description = "You’re offered a bribe to change a grade.",
                Choices = new List<Choice>
                {
                    new() { Text = "Refuse and report it.", HonestyScore = 2, EmpathyScore = 0, Consequence = "You protect fairness." },
                    new() { Text = "Accept it quietly.", HonestyScore = -2, EmpathyScore = -1, Consequence = "Your integrity is compromised." },
                    new() { Text = "Decline but don’t report.", HonestyScore = 1, EmpathyScore = 0, Consequence = "No harm done, but risk persists." }
                }
            },
            new EthicalScenario
            {
                Description = "A friend wants to cheat in a game.",
                Choices = new List<Choice>
                {
                    new() { Text = "Convince them to play fair.", HonestyScore = 1, EmpathyScore = 1, Consequence = "They agree." },
                    new() { Text = "Help them cheat.", HonestyScore = -2, EmpathyScore = -1, Consequence = "You both win unfairly." },
                    new() { Text = "Don’t play at all.", HonestyScore = 0, EmpathyScore = 0, Consequence = "You avoid the issue." }
                }
            }
        };

        public void OnGet()
        {
            if (ScenarioIndex < Scenarios.Count)
                CurrentScenario = Scenarios[ScenarioIndex];
        }

        public void OnPost()
        {
            if (ScenarioIndex >= Scenarios.Count)
                return;

            var current = Scenarios[ScenarioIndex];

            if (SelectedChoice >= 0 && SelectedChoice < current.Choices.Count)
            {
                var choice = current.Choices[SelectedChoice];
                Player.Honesty += choice.HonestyScore;
                Player.Empathy += choice.EmpathyScore;
                Feedback = choice.Consequence;
            }

            ShowConsequence = true;
            CurrentScenario = current;
        }

       
        public IActionResult OnPostNext()
        {
            ScenarioIndex++;
            ShowConsequence = false;

            if (ScenarioIndex < Scenarios.Count)
                CurrentScenario = Scenarios[ScenarioIndex];
            else
                CurrentScenario = null;

            return RedirectToPage(); // Ensures page reloads properly
        }

        public int GetScenarioIndex() => ScenarioIndex;
        public Player GetPlayer() => Player;
        public string GetFeedback() => Feedback;
    }
}
