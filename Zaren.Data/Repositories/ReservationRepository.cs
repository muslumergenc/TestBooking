using SanProject.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanProject.Data.Repositories
{
    public class ReservationRepository:Repository<ReservationDBDTO>, IReservationRepository
    {
        public ReservationRepository(SanProjectDBContext dBContext) : base(dBContext)
        {

        }


    }
}
