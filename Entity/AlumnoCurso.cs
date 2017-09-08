namespace Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    [Table("AlumnoCurso")]
    public partial class AlumnoCurso
    {
        public int id { get; set; }

        public int Alumno_id { get; set; }

        public int Curso_id { get; set; }

        public decimal Nota { get; set; }

        public virtual Alumno Alumno { get; set; }

        public virtual Curso Curso { get; set; }


        public List<AlumnoCurso> Listar(int Alumno_Id)
        {

            var alumnocursos = new List<AlumnoCurso>();

            try
            {
                using (var cn = new BDCursos())
                {
                    alumnocursos = cn.AlumnoCurso.Include("Curso")
                                    .Where(x => x.Alumno_id == Alumno_Id)
                                    .ToList();                                    
                }
            }
            catch {
                throw;
            }
            return alumnocursos;
        }

        public ResponseModel Guardar()
        {
            var rm = new ResponseModel();
            try
            {
                using (var cn = new BDCursos())
                {
                    cn.Entry(this).State = EntityState.Added;
                    rm.SetResoponse(true);
                    cn.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return rm;
        }
    }
}
