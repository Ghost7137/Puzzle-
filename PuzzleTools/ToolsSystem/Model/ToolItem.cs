using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsSystem
{
    public class ToolItem
    {
        public string Name { get; set; }
        public string ToolTip { get; set; }
        public string Description { get; set; }
        public ToolTypes Type { get; set; }
    }
}
