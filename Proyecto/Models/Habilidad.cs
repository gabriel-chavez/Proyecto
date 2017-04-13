namespace Proyecto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Habilidad")]
    public partial class Habilidad
    {
        public int id { get; set; }

        public int Usuario_id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        public int Dominio { get; set; }

        public virtual Usuario Usuario { get; set; }
        public Habilidad Obtener(int id)
        {
            var habilidad = new Habilidad();
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    habilidad = ctx.Habilidad.Where(x => x.id == id)
                                .SingleOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return habilidad;
        }
        public List<Habilidad> Listar()
        {
            List<Habilidad> habilidad = new List<Habilidad>();
            try
            {
                using (var ctx = new ProyectoContext())
                {
                    habilidad = ctx.Habilidad.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return habilidad;
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

