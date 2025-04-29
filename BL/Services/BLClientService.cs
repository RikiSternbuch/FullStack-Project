using BL.Api;
using BL.Models;
using Dal.Api;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services;

public class BLClientService : IBLClient
{
    IClient _clients;
    IBusyAppointment _busyAppointment;
    ITherapist _therapist;
    public BLClientService(IDal dal)
    {
        _clients = dal.Clients;
        _busyAppointment = dal.BusyAppointments;
        _therapist = dal.Therapists;
    }

    public async Task<bool> CreateNewClient(Client client)
    {
        if (client == null)
            throw new ArgumentNullException(nameof(client), "Client cannot be null");

        if (string.IsNullOrWhiteSpace(client.FirstName) || string.IsNullOrWhiteSpace(client.LastName))
            throw new ArgumentException("Client name cannot be empty", nameof(client.FirstName));

        if (string.IsNullOrWhiteSpace(client.PhoneNumber))
            throw new ArgumentException("Phone number cannot be empty", nameof(client.PhoneNumber));

        if (client.YearOfBirth < 1900 || client.YearOfBirth > DateTime.Now.Year)
            throw new ArgumentException("Year of birth is invalid", nameof(client.YearOfBirth));


        try
        {
            var mailAddress = new System.Net.Mail.MailAddress(client.Email);
        }
        catch
        {
            throw new ArgumentException("Invalid email", nameof(client.Email));
        }
        return await _clients.CreateAsync(client);

    }

    public async Task<List<BusyAppointmentForUser>> GetBusyAppointmentsForClient(string id, string name)
    {
        var busyAppointments = await _busyAppointment.ReadAllAsync();
        var therapists = await _therapist.ReadAllAsync();
        if (busyAppointments == null)
        {
            return new List<BusyAppointmentForUser>();
        }

        return busyAppointments
       .Where(app => app.ClientId.Equals(id))
       .Select(appointment =>
       {
           var therapist = therapists.FirstOrDefault(t => t.Id.Equals(appointment.TherapistId));
           DateTime appointmentDateTime = appointment.Date.ToDateTime(appointment.Time);

           return new BusyAppointmentForUser
           {
               Date = appointmentDateTime,
               Name = therapist != null
                   ? therapist.FirstName + " " + therapist.LastName
                   : "Unknown Therapist"
           };
       })
       .ToList();

    }
}


