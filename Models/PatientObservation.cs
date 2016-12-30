using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hl7.Fhir.Model;

namespace HealthpeuticsAPI.Models
{
    public class PatientObservation
    {
        
        [Key]
        public int PatientObservationId { get; set; }

        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }

        [Required]
        public int Value { get; set; }

        [Required]
        public string ObservationCode { get; set; }

        [Required]
        public string ObservationCodeText { get; set; }

        [Required]
        public string ObservationCodeSystem { get; set; }

        [Required]
        public DateTime ObservationDateTime { get; set; }

        internal Observation ToFHIR()
        {
            Observation fhirModel = new Observation();
            fhirModel.Id = PatientObservationId.ToString();
            fhirModel.Code = new CodeableConcept(ObservationCodeSystem, ObservationCode, ObservationCodeText);
            fhirModel.Value = new Integer(Value);
            fhirModel.Effective = Instant.FromLocalDateTime(ObservationDateTime.Year, ObservationDateTime.Month, ObservationDateTime.Day, ObservationDateTime.Hour, ObservationDateTime.Minute, ObservationDateTime.Second);
        
            ResourceReference patientReference = new ResourceReference();
            patientReference.Reference = @"Patient/" + PatientId;
            fhirModel.Subject = patientReference;

            return fhirModel;

        }
    }

     
}