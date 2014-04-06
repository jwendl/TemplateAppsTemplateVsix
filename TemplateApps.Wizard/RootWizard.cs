using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TemplateApps.Wizard
{
    public class RootWizard
        : IWizard
    {
        // Use to communicate $saferootprojectname$ to ChildWizard
        public static Dictionary<string, string> GlobalDictionary = new Dictionary<string, string>();

        // Use internally
        private DTE dteObject { get; set; }
        private Dictionary<string, string> replacementsDictionary { get; set; }
        private DirectoryInfo solutionFolder { get; set; }
        private object[] customParams { get; set; }

        // Add global replacement parameters
        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            var dte = automationObject as DTE;
            if (dte != null)
            {
                dteObject = dte;
            }

            solutionFolder = new DirectoryInfo(replacementsDictionary["$solutiondirectory$"]);

            // Place "$saferootprojectname$ in the global dictionary.
            // Copy from $safeprojectname$ passed in my root vstemplate
            GlobalDictionary["$saferootprojectname$"] = replacementsDictionary["$safeprojectname$"];
            this.replacementsDictionary = replacementsDictionary;
            this.customParams = customParams;
        }

        public void BeforeOpeningFile(EnvDTE.ProjectItem projectItem)
        {

        }

        public void ProjectFinishedGenerating(EnvDTE.Project project)
        {

        }

        public void ProjectItemFinishedGenerating(EnvDTE.ProjectItem projectItem)
        {

        }

        public void RunFinished()
        {
            AddSolutionItems();

            CloseSavedFiles();

            UpdateStrongNamedAssemblies();
        }

        private void UpdateStrongNamedAssemblies()
        {
            var templateFileInfo = new FileInfo(customParams.First().ToString());
            var strongNameDirectory = new DirectoryInfo(Path.Combine(templateFileInfo.Directory.Parent.FullName, "TemplateApps.ProjectContent.zip"));

            var ef5RepositoryFileInfo = strongNameDirectory.GetFiles("SharpRepository.Ef5Repository.dll").Single();
            ef5RepositoryFileInfo.CopyTo(Path.Combine(solutionFolder.FullName, "packages", "SharpRepository.Ef5Repository.1.3.6.10", "lib", "net40", ef5RepositoryFileInfo.Name), true);

            var sharpRepositoryFileInfo = strongNameDirectory.GetFiles("SharpRepository.Repository.dll").Single();
            sharpRepositoryFileInfo.CopyTo(Path.Combine(solutionFolder.FullName, "packages", "SharpRepository.Repository.1.3.6.15", "lib", "net40", sharpRepositoryFileInfo.Name), true);
        }

        private void AddSolutionItems()
        {
            var templateFileInfo = new FileInfo(customParams.First().ToString());
            var inputDirectory = new DirectoryInfo(Path.Combine(templateFileInfo.Directory.Parent.FullName, "TemplateApps.ProjectContent.zip"));

            var solution = dteObject.Solution as Solution;
            var projects = solution.Projects;

            // Add custom project manipulations here...
            foreach (Project project in projects)
            {
                // The solution items folder.
                if (project.Name == "Solution Items")
                {
                    AddFile(project, inputDirectory, "CustomDictionary.xml");
                    AddFile(project, inputDirectory, "TemplateApps.Template.ruleset");
                    AddFile(project, inputDirectory, "TemplateApps.Template.snk");
                }
                else
                {
                    // A real project
                    var projectItem = AddFileLink(project, inputDirectory, "CustomDictionary.xml");
                    projectItem.Properties.Item("ItemType").Value = "CodeAnalysisDictionary";

                    AddFileLink(project, inputDirectory, "TemplateApps.Template.ruleset");
                    AddFileLink(project, inputDirectory, "TemplateApps.Template.snk");
                }
            }
        }

        private void CloseSavedFiles()
        {
            // Create a list of all saved documents.
            Documents docs = dteObject.Documents;
            ArrayList savedDocs = new ArrayList();
            for (int i = 1; i <= docs.Count; i++)
            {
                if (docs.Item(i).Saved)
                {
                    savedDocs.Add(docs.Item(i));
                }
            }

            // Close all saved documents.
            foreach (Document doc in savedDocs)
            {
                doc.Close(vsSaveChanges.vsSaveChangesNo);
            }
        }

        private ProjectItem AddFile(Project project, DirectoryInfo directoryInfo, string fileName)
        {
            var inputFiles = directoryInfo.GetFiles();
            var customFile = inputFiles
                .Where(fi => fi.Name == fileName)
                .Single();

            return project.ProjectItems.AddFromFileCopy(customFile.FullName);
        }

        private ProjectItem AddFileLink(Project project, DirectoryInfo directoryInfo, string fileName)
        {
            var inputFiles = directoryInfo.GetFiles();
            var customDictionary = inputFiles
                .Where(fi => fi.Name == fileName)
                .Single();

            return project.ProjectItems.AddFromFile(Path.Combine(solutionFolder.FullName, customDictionary.Name));
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }
    }
}
