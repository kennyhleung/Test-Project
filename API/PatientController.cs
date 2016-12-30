using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HealthpeuticsAPI.Models;
using FHIR=Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;

namespace HealthpeuticsAPI.API
{
    public class PatientController : ApiController
    {
        private DB db = new DB();

        // GET: api/Patient/<PatientId>
        public async Task<HttpResponseMessage> GetPatient(int id)
        {
            Patient Patient = await db.Patients.FindAsync(id);
            if (Patient == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            return new HttpResponseMessage() { Content = new StringContent(FhirSerializer.SerializeResourceToJson(Patient.ToFHIR()), System.Text.Encoding.UTF8, "application/json") };
        }
      
    }
}