using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Payroll.Controllers
{
    public class SendEmpDataController : ApiController
    {

        // POST: api/SendEmpData
        [HttpPost]
        public void Post([FromBody] empleados any)
        {

            /////////////////////////////////////////
            //No logre leer el XML, solamente Jsons//
            /////////////////////////////////////////

            //var result = new object();
            //var body = await Request.Content.ReadAsHttpRequestMessageAsync();

            //var doc = new System.Xml.XmlDocument();
            //doc.Load(request.Content.ReadAsStreamAsync().Result);
            //doc.Load(body);

            //System.Xml.Linq.XDocument doc = System.Xml.Linq.XDocument.Load(body);



            //var result = doc.ToString();



        }

    }


    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class empleados
    {

        private empleadosEmpleado[] empleadoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("empleado")]
        public empleadosEmpleado[] empleado
        {
            get
            {
                return this.empleadoField;
            }
            set
            {
                this.empleadoField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class empleadosEmpleado
    {

        private string nombresField;

        private string apellidosField;

        private decimal horasField;

        private decimal importeField;

        private string tipoField;

        private byte seccionField;

        /// <remarks/>
        public string nombres
        {
            get
            {
                return this.nombresField;
            }
            set
            {
                this.nombresField = value;
            }
        }

        /// <remarks/>
        public string apellidos
        {
            get
            {
                return this.apellidosField;
            }
            set
            {
                this.apellidosField = value;
            }
        }

        /// <remarks/>
        public decimal horas
        {
            get
            {
                return this.horasField;
            }
            set
            {
                this.horasField = value;
            }
        }

        /// <remarks/>
        public decimal importe
        {
            get
            {
                return this.importeField;
            }
            set
            {
                this.importeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string tipo
        {
            get
            {
                return this.tipoField;
            }
            set
            {
                this.tipoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte seccion
        {
            get
            {
                return this.seccionField;
            }
            set
            {
                this.seccionField = value;
            }
        }
    }


}
