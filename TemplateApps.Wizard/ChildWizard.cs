using Microsoft.VisualStudio.TemplateWizard;
using System.Collections.Generic;

namespace TemplateApps.Wizard
{
    public class ChildWizard
        : IWizard
    {
        // Retrieve global replacement parameters
        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            // Add custom parameters.
            replacementsDictionary.Add("$saferootprojectname$", RootWizard.GlobalDictionary["$saferootprojectname$"]);
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

        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }
    }
}
