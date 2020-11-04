using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Octokit;

namespace GithubUpdate
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Récupération de la version actuelle de l'application
            string currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            // Utilisation de la librairie Octokit
            var client = new GitHubClient(new ProductHeaderValue("GithubUpdaterRepo"));
            // Récupération de toutes les releases du dépôt "GithubUpdaterRepo" créé par "EvannG1"
            var releases = await client.Repository.Release.GetAll("EvannG1", "GithubUpdaterRepo");
            // Récupération de la dernière release
            var latest = releases[0];

            Console.WriteLine("Version actuelle ! [{0}] : récupéré depuis la version d'assembly de l'application\n", currentVersion);

            if (currentVersion != latest.TagName)
            {
                Console.WriteLine("Nouvelle version disponible ! : récupéré depuis les releases GitHub [{0}]\n\nDisponible à cette adresse : {1}", latest.TagName, latest.HtmlUrl);
            }
            else
            {
                Console.WriteLine("L'application est à jour.");
            }

            Console.Read();
        }
    }
}
