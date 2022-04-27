using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Personeel.MVCSite.Models.AssessViewModels
{
    public class EditAssess
    {
        public Guid Id { get; set; }
        public string AssessName { get; set; }
        public string AssessType { get; set; }
        public string AssessDep { get; set; }
        public string UserName { get; set; }
        public string Result { get; set; }
    }

}