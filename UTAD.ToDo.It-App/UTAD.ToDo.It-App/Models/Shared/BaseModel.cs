using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTAD.ToDo.It_App.Models.Shared
{
    public class BaseModel
    {
        public BaseModel()
        {
            if (string.IsNullOrEmpty(Id))
            {
                Id = Guid.NewGuid().ToString();
            }
        }

        public string Id { get; set; }
    }
}