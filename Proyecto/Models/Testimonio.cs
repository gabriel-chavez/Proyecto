namespace Proyecto.Models
{
    using Helper;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Testimonio")]
    public partial class Testimonio
    {
        public int id { get; set; }

        public int? Usuario_id { get; set; }

        [Required]
        [StringLength(50)]
        public string IP { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Comentario { get; set; }

        public int Estado_id { get; set; }

        [Required]
        [StringLength(10)]
        public string Fecha { get; set; }

        public virtual Usuario Usuario { get; set; }
        public Testimonio Obtener(int id)
        {
            var testimonio = new Testimonio();
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    testimonio = ctx.Testimonio.Where(x => x.id == id)
                                .SingleOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return testimonio;
        }
        public List<Testimonio> Listar()
        {
            List<Testimonio> testimonio = new List<Testimonio>();
            try
            {
                using (var ctx = new ProyectoContext())
                {
                
                    var id = SessionHelper.GetUser();
                    testimonio = ctx.Testimonio.Where(x => x.Usuario_id == id)
                                            .ToList();
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return testimonio;
        }
        public ResponseModel Guardar()
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    if (this.id > 0) ctx.Entry(this).State = EntityState.Modified;
                    else ctx.Entry(this).State = EntityState.Added;
                    ctx.SaveChanges();
                    rm.SetResponse(true);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return rm;
        }
        public ResponseModel Eliminar(int id)
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    this.id = id;
                    ctx.Entry(this).State = EntityState.Deleted;
                    ctx.SaveChanges();
                    rm.SetResponse(true);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return rm;
        }
    }
}
