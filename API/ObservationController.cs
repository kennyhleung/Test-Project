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
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using System.Configuration;

namespace HealthpeuticsAPI.API
{
    public class ObservationController : ApiController
    {
        private DB db = new DB();

    
        // GET: api/Observation/<PatientObservationId>
        public async Task<HttpResponseMessage> GetPatientObservation(int id)
        {
            PatientObservation patientObservation = await db.PatientObservations.FindAsync(id);
            if (patientObservation == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return new HttpResponseMessage() { Content = new StringContent(FhirSerializer.SerializeResourceToJson(patientObservation.ToFHIR()),System.Text.Encoding.UTF8, "application/json") };
        }

        // GET: api/Observation?subject=Patient/<patientId>
        public async Task<HttpResponseMessage> GetPatientObservationBySubject(string subject)
        {
            string[] inputparameters = subject.Split('/');
            int searchPatientId;
            if (inputparameters.Length != 2 || inputparameters[0].ToLowerInvariant()!="patient" || !Int32.TryParse(inputparameters[1],out searchPatientId))
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            Models.Patient searchPatient = await db.Patients.FirstAsync(p => p.PatientId == searchPatientId);
            if (searchPatient == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            List<PatientObservation> patientObservations = await db.PatientObservations.Where(po => po.PatientId == searchPatient.PatientId).ToListAsync();

            Bundle searchSet = new Bundle() { Type = Bundle.BundleType.Searchset, Total = patientObservations.Count };
            foreach (PatientObservation patientObservation in patientObservations)
            {
                searchSet.AddResourceEntry(patientObservation.ToFHIR(), ConfigurationManager.AppSettings["BaseURL"] + "api/MedicationStatement/" + patientObservation.PatientObservationId);
            }

            return new HttpResponseMessage() { Content = new StringContent(FhirSerializer.SerializeResourceToJson(searchSet),System.Text.Encoding.UTF8, "application/json") };
        }
      
    }
}