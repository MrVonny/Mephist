using Mephist.Data;
using Mephist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mephist.Services
{
    public class UniversityCRUD<T> where T : IDataModel
    {

        private UniversityContext _context;

        public UniversityCRUD(UniversityContext context)
        {
            _context = context;
        }

        //Create
        void CreateDataModel(T model)
        {
            
        }
        //Read

        //Update

        //Delete

    }
}
