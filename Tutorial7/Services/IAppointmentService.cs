using Tutorial7.DTOs;

namespace Tutorial7.Services;

public interface IAppointmentService
{
    Task<IEnumerable<AppointmentListDto>> GetAllAppointmentsAsync();
}