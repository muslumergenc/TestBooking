using SanProject.Shared.HotelModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SanProject.Application.Services.Interfaces
{
    public interface IHotelService
    {
        string tokne { get; set; }

        Task<HotelDetailDTO> GetDetails(string querys, int adultnum, string checkinstr);
        Task<HotelDetailDTO> GetDetailsTest(string query, int adultnum);
        Task<List<HotelDetailDTO>> GetAllDetails(AllHotelQueryDTO qu);

    }
}