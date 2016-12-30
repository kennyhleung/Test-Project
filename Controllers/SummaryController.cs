using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using HealthpeuticsAPI.Models;
using FHIR = Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;

namespace HealthpeuticsAPI.Controllers
{
    public class SummaryController : Controller
    {
        public async Task<ActionResult> Render()
        {
            ViewBag.Title = "Healthpeutics - Summary";

            string patientURL = @"http://localhost:57802/api/patient/1";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(patientURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(patientURL);
            if (response.IsSuccessStatusCode)
            {
                string fhirPatientString = await response.Content.ReadAsStringAsync();
                FhirJsonParser parser = new FhirJsonParser();
                FHIR.Patient fhirPatient = parser.Parse<FHIR.Patient>(fhirPatientString);
                SummaryViewModel viewmodel = new SummaryViewModel();

                viewmodel.Name = string.Format("{0} {1}", fhirPatient.Name[0].Given.ToList<string>()[0], fhirPatient.Name[0].Family.ToList<string>()[0]);
                    viewmodel.Gender = fhirPatient.Gender.ToString();
                viewmodel.BirthDate = fhirPatient.BirthDate;
                viewmodel.Address = (fhirPatient.Address[0].Line.ToList<string>().Count != 2) ? string.Format("{0} {1}, {2} {3}", fhirPatient.Address[0].Line.ToList<string>()[0], fhirPatient.Address[0].City, fhirPatient.Address[0].State, fhirPatient.Address[0].PostalCode) : string.Format("{0} {1} {2}, {3} {4}", fhirPatient.Address[0].Line.ToList<string>()[0], fhirPatient.Address[0].Line.ToList<string>()[1], fhirPatient.Address[0].City, fhirPatient.Address[0].State, fhirPatient.Address[0].PostalCode);
                viewmodel.Phone = fhirPatient.Telecom[0].Value;
                
                return View("~/Views/Summary.cshtml",viewmodel);
            }
            else
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }
    }
}
