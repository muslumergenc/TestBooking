using System;
using System.Collections.Generic;
using System.Text;

namespace SanProject.Domain.SetReservation
{
    public class Address
    {
        public ContactPhone contactPhone { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string zipCode { get; set; }
        public City city { get; set; }
        public Country country { get; set; }
        public string phone { get; set; }
    }

    public class City
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class ContactPhone
    {
        public string countryCode { get; set; }
        public string areaCode { get; set; }
        public string phoneNumber { get; set; }
    }

    public class Country
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class DestinationAddress
    {
    }

    public class Nationality
    {
        public string twoLetterCode { get; set; }
    }

    public class PassportInfo
    {
        public string serial { get; set; }
        public string number { get; set; }
        public DateTime expireDate { get; set; }
        public DateTime issueDate { get; set; }
        public string citizenshipCountryCode { get; set; }
    }

    public class RootReservation
    {
        public string transactionId { get; set; }
        public List<Traveller> travellers { get; set; }
        public string reservationNote { get; set; }
        public string agencyReservationNumber { get; set; }
    }

    public class Traveller
    {
        public string travellerId { get; set; }
        public int type { get; set; }
        public int title { get; set; }
        public int passengerType { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public bool isLeader { get; set; }
        public DateTime birthDate { get; set; }
        public Nationality nationality { get; set; }
        public string identityNumber { get; set; }
        public PassportInfo passportInfo { get; set; }
        public Address address { get; set; }
        public DestinationAddress destinationAddress { get; set; }
        public int orderNumber { get; set; }
        public List<object> documents { get; set; }
        public List<object> insertFields { get; set; }
        public int status { get; set; }
    }




}
