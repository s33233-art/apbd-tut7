using Microsoft.Data.SqlClient;
using Tutorial7.DTOs;

namespace Tutorial7.Services;

public class AppointmentService : IAppointmentService
{
    private readonly string _connectionString;

    public AppointmentService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<IEnumerable<AppointmentListDto>> GetAllAppointmentsAsync() 
    {
        var query = "SELECT a.IdAppointment, a.AppointmentDate, a.Status, a.Reason, p.FirstName + N' ' + p.LastName AS PatientFullName, p.Email AS PatientEmail" +
            "FROM dbo.Appointments a" +
            "JOIN dbo.Patients p ON p.IdPatient = a.IdPatient" +
            "WHERE (@Status IS NULL OR a.Status = @Status)" +
            "  AND (@PatientLastName IS NULL OR p.LastName = @PatientLastName)" +
            "ORDER BY a.AppointmentDate;";

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@Status", Status);
        command.Parameters.AddWithValue("@PatientLastName", PatientLastName);

        var reader = await command.ExecuteReaderAsync();
        var appointments = new List<AppointmentListDto>();
        while (await reader.ReadAsync())
        {
            var appointment = new AppointmentListDto();
            appointments.Add(appointment);
        }
    }
}