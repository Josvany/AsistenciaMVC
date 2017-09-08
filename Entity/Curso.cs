namespace Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Curso")]
    public partial class Curso
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Curso()
        {
            AlumnoCurso = new HashSet<AlumnoCurso>();
        }

        public int id { get; set; }

        [Required (ErrorMessage ="Introduzca el Nombre dle Curso")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlumnoCurso> AlumnoCurso { get; set; }

        public List<Curso> Listar()
            {
                var cursos = new List<Curso>();

            try
            {
                using (var cn = new BDCursos())
                {
                    var lista = cn.Curso.ToList();
                    cursos = lista;
                }
            }
            catch (Exception)  {
                throw;
            }

            return cursos;
            }

        public List<Curso> Todos(int IdAlumno=0)
        {
            var cursos = new List<Curso>();

            try
            {
                using (var cn = new BDCursos())
                {
                    if (IdAlumno > 0)
                    {
                        var cusos_actuales =
                            cn.AlumnoCurso.Where(x => x.Alumno_id == IdAlumno)
                            .Select(x => x.Curso_id).ToList();

                        cursos = cn.Curso.Where(x => !cusos_actuales.Contains(x.id)).ToList();

                    }
                    else {
                        cursos = cn.Curso.ToList();
                    }


                    var lista = cn.Curso.ToList();
                    cursos = lista;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return cursos;
        }
    }
}
