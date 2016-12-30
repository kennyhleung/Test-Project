using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Hl7.Fhir.Model;
using FHIR = Hl7.Fhir.Model;

namespace HealthpeuticsAPI.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }
        
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public string Street1 { get; set; } 

        public string Street2 { get; set; }
        
        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string Phone { get; set; }

        internal FHIR.Patient ToFHIR()
        {
            FHIR.Patient fhirModel = new FHIR.Patient();
            fhirModel.Id = PatientId.ToString();
            fhirModel.Name = new List<HumanName>() { new HumanName() { Given = new List<string>() { FirstName }, Family = new List<string>() { LastName } } };
            fhirModel.Gender = (Gender == "Male" ? AdministrativeGender.Male : AdministrativeGender.Female);
            fhirModel.BirthDate = BirthDate.ToString("yyyy-MM-dd");
            
            List<string> addressLine = new List<string>() { Street1};
            if (Street2 != null) addressLine.Add(Street2);
            fhirModel.Address = new List<Address>() { new Address() { Line = addressLine, City = City, State = State, PostalCode = ZipCode } };
            fhirModel.Telecom = new List<ContactPoint>() { new ContactPoint() { Value = Phone, Use = ContactPoint.ContactPointUse.Home } };

            return fhirModel;

        }
    }

     
}