namespace HealthpeuticsAPI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using HealthpeuticsAPI.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<HealthpeuticsAPI.Models.DB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HealthpeuticsAPI.Models.DB context)
        {
            Patient patient1 = new Models.Patient()
            {
                FirstName = "Bob",
                LastName = "Cat",
                Gender = "Male",
                BirthDate = new DateTime(1999, 4, 6),
                Phone = "123-456-7890",
                Street1 = "1 Happy Way",
                Street2 = "Apt 110",
                City = "Seattle",
                State = "WA",
                ZipCode = "98120"

            };

            context.Patients.AddOrUpdate(
              p => p.PatientId, patient1
            );

            context.PatientObservations.AddOrUpdate(
                po => po.PatientObservationId,
                new PatientObservation()
                {
                    Patient = patient1,
                    ObservationCodeSystem = "LOINC",
                    ObservationCode = "29463-7",
                    ObservationCodeText = "Body Weight",
                    ObservationDateTime = new DateTime(2016, 12, 23, 8, 5, 4),
                    Value = 124
                },
                new PatientObservation()
                {
                    Patient = patient1,
                    ObservationCodeSystem = "LOINC",
                    ObservationCode = "29463-7",
                    ObservationCodeText = "Body Weight",
                    ObservationDateTime = new DateTime(2016, 12, 25, 12, 45, 30),
                    Value = 126
                },
                new PatientObservation()
                {
                    Patient = patient1,
                    ObservationCodeSystem = "LOINC",
                    ObservationCode = "29463-7",
                    ObservationCodeText = "Body Weight",
                    ObservationDateTime = new DateTime(2016, 12, 29, 18, 20, 5),
                    Value = 129
                }
           );
        }
    }
}
