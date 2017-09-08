namespace Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Data.Entity;

    [Table("Alumno")]
    public partial class Alumno
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Alumno()
        {
            Adjunto = new HashSet<Adjunto>();
            AlumnoCurso = new HashSet<AlumnoCurso>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }

        public int Sexo { get; set; }

        [Required]
        [StringLength(10)]
        public string FechaNacimiento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Adjunto> Adjunto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlumnoCurso> AlumnoCurso { get; set; }

        public List<Alumno> Listar()
        {
            var alumno = new List<Alumno>();
            try
            {
                using (var cn = new BDCursos())
                {
                    alumno = cn.Alumno.ToList();
                }
            }
            catch(Exception) {
                throw;
            }

            return alumno;
        }

        public Alumno Obtener(int id)
        {
            var alumno = new Alumno();
            try
            {
                using (var cn = new BDCursos())
                {
                    alumno = cn.Alumno.Include("AlumnoCurso").Include("AlumnoCurso.Curso").Where(x => x.id == id).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return alumno;
        }

        public ResponseModel Guardar()
        {
            var rm = new ResponseModel();
            try
            {
                using (var cn = new BDCursos())
                {
                    if (this.id > 0)
                    {
                        cn.Entry(this).State = EntityState.Modified;
                    }
                    else
                    {
                        cn.Entry(this).State = EntityState.Added;
                    }

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

        public ResponseModel Borrar()
        {
            var rm = new ResponseModel();
            try
            {
                using (var cn = new BDCursos())
                {
                    if (this.id > 0)
                    {
                        cn.Entry(this).State = EntityState.Deleted;
                    }
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

        public void Eliminar()
        {
            try
            {
                using (var cn = new BDCursos())
                {
                    cn.Entry(this).State = EntityState.Deleted;
                    cn.SaveChanges();   //sin los save changes los cambios no quedarán confirmados asi que CUIDADITO WASAWSKY
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
