using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{   
    public class ResponseModel
    {

        public dynamic result { get; set; }
        public bool response { get; set; }
        public string message { get; set; }
        public string href { get; set; }
        public string function { get; set; }

        public ResponseModel()
        {
            this.response = false;
            this.message = "Ocurrio un error inesperado.";
        }

        public void SetResoponse(bool r, string m = "")
        {
            this.response = r;
            this.message = m;

            if (!r && message == "") this.message = "Ocurrio un error inesperado.";

        }
        
    }
}
