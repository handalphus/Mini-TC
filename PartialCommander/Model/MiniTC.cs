using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartialCommander.Model
{
    class MiniTC
    {
        public List<PanelTC> panels = new List<PanelTC>(2);
       public void InitializePanels(string path1, string path2)
        {
            panels.Add(new PanelTC(path1));
            panels.Add(new PanelTC(path2));
        }
        public void ChangePanel(int numberOfPanel, string newPath)
        {
            panels[numberOfPanel].CurrentPath = newPath;
        }
        
    }
}
